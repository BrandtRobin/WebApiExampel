using System.Web.Http;
using WebApplication5.Interfaces;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [RoutePrefix("login")]
    public class LoginController : ApiController
    {
        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [Route("")]
        public IHttpActionResult Login(Credentials credentials)
        {
            var token = _loginService.Login(credentials);

            if (token == null) return Unauthorized();

            return Ok(token);
        }

    }
}
