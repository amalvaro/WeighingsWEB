using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;
 
namespace Middleware
{
    public class AuthorizationMiddleware
    {
        private readonly RequestDelegate nextHandler;
    
        public AuthorizationMiddleware(RequestDelegate next)
        {
            this.nextHandler = next;
        }
    
        public async Task InvokeAsync(HttpContext context)
        {
            string path = context.Request.Path.Value.ToLower();

            if(path.StartsWith("/weighinglog") || path.StartsWith("/home") || path.StartsWith("/dictionary") || path.StartsWith("/interface") || path.StartsWith("/report")) {
                if(context.Session.Keys.Contains("AUTH_STATE")) {
                    if(context.Session.GetInt32("AUTH_STATE") == 1) {
                        
                        await nextHandler.Invoke(context);
                        return ;

                    }
                }
                await context.Response.WriteAsync("You are not authorized");
            } else {
                await nextHandler.Invoke(context);
            }

        }
    }
}