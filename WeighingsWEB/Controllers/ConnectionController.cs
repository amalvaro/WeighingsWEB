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
using WeighingsWEB.Controllers.Response.BasicResponses;
using System.Web;


namespace WeighingsWEB.Controllers
{


	[ApiController]
    
	[Route("[controller]")]
    [Route("[controller]/[action]")]
	public class ConnectionController : ControllerBase
	{
        private static string MSSQL_FILE_PATH = "./mssql-connection.cfg";


		/* Контроллер для проверки подключения к базе данных. */
		[HttpGet]
		public object Get()
		{
            bool bIsConnectionStringExists  = System.IO.File.Exists(MSSQL_FILE_PATH);
            if(bIsConnectionStringExists) {
                bIsConnectionStringExists = 
                    System.IO.File.ReadAllText(MSSQL_FILE_PATH).Trim().Length != 0;
            }
            return new BooleanResponse(bIsConnectionStringExists);
		}

        /* Метод для добавления строки подключения. */
        public object Create(string connectionString) {
            bool bResult = connectionString.Trim().Length != 0;
            if(bResult) {
                string sLastContent = System.IO.File.ReadAllText(MSSQL_FILE_PATH);
                connectionString = System.Web.HttpUtility.UrlDecode(connectionString);
                System.IO.File.WriteAllText(MSSQL_FILE_PATH, connectionString);
                try {
                    Context dbContext = new Context();
                }
                catch(Exception e) {
                    bResult = false;
                    System.IO.File.WriteAllText(MSSQL_FILE_PATH, sLastContent);
                }
            }
            return new BooleanResponse(bResult);
        }
	}
}
