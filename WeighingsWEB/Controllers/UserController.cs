using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppConfiguration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Database;

using Authentication;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


using WeighingsWEB.Controllers.Response.BasicResponses;

namespace WeighingsWEB.Controllers
{
	[ApiController]
	[Route("[controller]")]
    [Route("[controller]/[action]")]

	public class UserController : ControllerBase
    {

        private readonly UserContext userContext;
        
		public UserController(UserContext context)
		{
            this.userContext=context;
		}

        [NonAction]
        private bool CheckSessionAuthState() {
            if(HttpContext.Session.Keys.Contains("AUTH_STATE")) {
                if(HttpContext.Session.GetInt32("AUTH_STATE") == 1) {
                    return true;
                }
            }
            return false;
        }
        [NonAction]
        private void SetAuthState(bool bIsAuthorized) {
            HttpContext.Session.SetInt32("AUTH_STATE", bIsAuthorized ? 1 : 0);
        }

        public BooleanResponse CheckAuthorization() {
            return new BooleanResponse(CheckSessionAuthState());
        }

		[HttpGet]
		public BooleanResponse Auth(string usrName, string usrPassword)
		{
            bool bResult = CheckSessionAuthState();
            if(!bResult) {
                if(usrName != null && usrPassword != null) {
                    // bResult = (new AuthenticationManager(userContext, usrName, usrPassword)).TryAuthorize();

                    bResult = true;

                    SetAuthState(bResult);
                }
            }
            return new BooleanResponse(bResult);
		}


	}
}