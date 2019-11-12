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
                try {
                    /* Проверка корректности введенных данных */
                    Context context = new Context();
                }
                catch(Exception) {
                    bIsConnectionStringExists = false;
                }
            }
            return new BooleanResponse(bIsConnectionStringExists);
		}


        /* Метод для добавления строки подключения. */
        public object Create(string Server, string Database, string Login = null, string Password = null) {

            Console.WriteLine("------");
            Console.WriteLine(Server);
            Console.WriteLine(Database);
            Console.WriteLine(Login);
            Console.WriteLine(Password);
            Console.WriteLine("------");


            bool bResult = true;
            DatabaseConfiguration configuration = new DatabaseConfiguration("mssql-connection.cfg");

            string[][] parameters;

            if(Login != null && Password != null) {
                parameters = new String[][] { 
                    new String[] { "Server", "Database", "Login", "Password" },
                    new String[] { Server, Database, Login, Password }
                };
            } else {
                parameters = new String[][] { 
                    new String[] { "Server", "Database", "WindowsAuthentication" },
                    new String[] { Server, Database, "true" }
                };
            }
            string lastContentFile = System.IO.File.ReadAllText("mssql-connection.cfg");
            configuration.ApplyParameters(
                parameters[0], 
                parameters[1]
            );

            
            try {
                /* Проверка корректности введенных данных */
                Context context = new Context();
            }
            catch(Exception) {
                System.IO.File.WriteAllText("mssql-connection.cfg", lastContentFile);
                bResult = false;
            }

            return new BooleanResponse(bResult);
        }
	}
}
