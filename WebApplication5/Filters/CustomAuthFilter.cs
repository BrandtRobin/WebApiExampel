using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using WebApplication5.Interfaces;
using ActionFilterAttribute = System.Web.Http.Filters.ActionFilterAttribute;

namespace WebApplication5.Filters
{
    public class CustomAuthFilter : ActionFilterAttribute
    {

        private readonly ILoginService _loginService;

        public CustomAuthFilter(ILoginService loginService)
        {
            _loginService = loginService;
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var path = actionContext.Request.RequestUri.AbsolutePath;

                if (!path.StartsWith("/login"))
                {
                    if (!actionContext.Request.Headers.Contains("Authorization"))
                    {
                        actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    }

                    if (actionContext.Request.Headers.Contains("Authorization"))
                    {
                        if (string.IsNullOrEmpty(actionContext.Request.Headers.Authorization.Parameter) || !actionContext.Request.Headers.Authorization.Scheme.StartsWith("Bearer"))
                        {
                            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        }

                        var tokenString = actionContext.Request.Headers.Authorization.Parameter;

                        if (!_loginService.ValidateToken(tokenString))
                        {
                            actionContext.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                        }

                        Debug.WriteLine("AUTH: " + tokenString);
                    }
                
                }
        }

    }
}