using System.Collections.Generic;
using System.Web.Http;
using WebApplication5.Filters;
using WebApplication5.Models;
using WebApplication5.Services;

namespace WebApplication5.Controllers
{
    [RoutePrefix("users")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [RolesAllowed("")]
        [Route("")]
        public IHttpActionResult Get()
        {
            var users = _userService.GetUsers();
            if (users == null)
            {
                return NotFound();
            }
            return Ok(users);
        }

        [Route("{userId:long}")]
        public IHttpActionResult Get(long userId)
        {
            var user = _userService.FindUserById(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [Route("{email}")]
        public IHttpActionResult GetUserByEmail(string email)
        {
            var user = _userService.FindUserByEmail(email);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [Route("")]
        public IHttpActionResult Post(User userToAdd)
        {
            if (ModelState.IsValid)
            {
                var user =_userService.CreateUser(userToAdd);
                var location = Request.RequestUri + "/" + user.UserId;
                return Created(location, user);
            }

            return BadRequest();
        }

        [Route("bulk")]
        public IHttpActionResult PostUsers(List<User> usersToAdd)
        {
            if (ModelState.IsValid)
            {
                var users =_userService.CreateUsers(usersToAdd);
                return Ok(users);
            }

            return BadRequest(ModelState);
        }

        [Route("{userId}/{roleId}")]
        public IHttpActionResult AddRoleToUser(long userId, long roleId)
        {
            var user = _userService.AddRoleToUser(userId, roleId);
            if (user == null)
            {
                NotFound();
            }
            return Ok(user);
        }

        [Route("")]
        public IHttpActionResult Put(User userToUpdate)
        {
            if (ModelState.IsValid)
            {
                var user =_userService.UpdateUser(userToUpdate);
                return Ok(user);
            }

            return BadRequest(ModelState);
        }

        [Route("{userId}")]
        public IHttpActionResult Delete(int userId)
        {
            var user =_userService.DeleteUser(userId);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

    }
}   
