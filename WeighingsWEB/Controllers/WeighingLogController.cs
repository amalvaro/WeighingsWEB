using Entities.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WeighingsWEB.Database.EntityWorker.Entities;
using WeighingsWEB.Database.EntityWorker;
using Database;
using Util.Picture;
using System.Drawing.Imaging;
using Microsoft.EntityFrameworkCore;

namespace WeighingsWEB.Controllers
{


	[ApiController]
	[Route("[controller]")]
	public class WeighingLogController : ControllerBase
	{

		private readonly DbContext dbContext;

		public WeighingLogController(DbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		/* 23.10.2019. Контроллер для отображения журнала взвешиваний. */
		[HttpGet]
		public object Get(int page = 1)
		{
			if(page < 1)
				page = 1;


			var repository = new EntityRepository<WeighingLog>(dbContext);
			var weighingLogWorker = new WeighingLogWorker(repository);
			
			var listOfItems = weighingLogWorker.GetLogList((page - 1) * 5 /* 5 элементов на странице */, 5 /* количество элементов на странице */);
			var count = repository.Count();

			foreach (var log in listOfItems)
			{
				for(int i = 0; i < log.WeighingImages.Count; i++)
				{
					CompressPictureByte compressor = new CompressPictureByte(log.WeighingImages[i].ImageData);
					log.WeighingImages[i].ImageData =
						compressor
						.Resize(1.5f)
						.GetPictureArrayByte(ImageFormat.Jpeg);

				}
			}

			return new { Count = count, Response = listOfItems };

		}
	}
}
