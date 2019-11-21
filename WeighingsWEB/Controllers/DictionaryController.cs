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
	public class DictionaryController : ControllerBase
	{

		private readonly DbContext dbContext;

		public DictionaryController(DbContext dbContext)
		{
			this.dbContext = dbContext;
		}

		/* 30.10.2019. Контроллер для получения данных справочника */
		[HttpGet]
		public object Get(int fieldId)
		{
			return (new EntityRepository<RegisterValues>(dbContext))
                .Get(e => e.FieldId==fieldId);
		}
	}
}
