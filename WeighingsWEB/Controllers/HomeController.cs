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
	public class HomeController : ControllerBase
	{

		private readonly DbContext dbContext;

		public HomeController(DbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		/* 30.10.2019. Контроллер для отображения последних 4 записей для начальной страницы. */
		[HttpGet]
		public object Get()
		{

			var repository = new EntityRepository<WeighingLog>(dbContext);
			var weighingLogWorker = new WeighingLogWorker(repository);

			/* Выбрать первые 4 записи */

			var lastWeighingLogs = weighingLogWorker.GetLogList(0, 4, null);

			foreach (var weighing in lastWeighingLogs)
				for (int i = 0; i < weighing.WeighingImages.Count; i++)
				{
					weighing.WeighingImages[i].ImageData =
						new CompressPictureByte(weighing.WeighingImages[i].ImageData)
						.Resize(1.5f)
						.GetPictureArrayByte(ImageFormat.Jpeg);
					weighing.WeighingImages[i].Format = "jpeg";
				}

			


			return lastWeighingLogs;

		}
	}
}
