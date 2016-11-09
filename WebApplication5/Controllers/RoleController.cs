using System.Web.Http;
using WebApplication5.Interfaces;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    [RoutePrefix("roles")]
    public class RoleController : ApiController
    {
        private readonly IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            var roles = _roleService.GetAllRoles();
            if (roles == null)
            {
                return NotFound();
            }
            return Ok(roles);
        }

        [Route("id/{roleId}")]
        public IHttpActionResult GetRoleByRoleId(long roleId)
        {
            var role = _roleService.FindRoleByRoleId(roleId);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [Route("{roleName}")]
        public IHttpActionResult GetRoleByRoleName(string roleName)
        {
            var role = _roleService.FindRoleByRoleName(roleName);
            if (role == null)
            {
                return NotFound();
            }
            return Ok(role);
        }

        [Route("")]
        public IHttpActionResult Post(Role roleToAdd)
        {
            if (ModelState.IsValid)
            {
                var role = _roleService.CreateRole(roleToAdd);
                if (role == null)
                {
                    return BadRequest("Role exists");
                }
                var location = Request.RequestUri + "/" + role.RoleId;
                return Created(location, role);
            }

            return BadRequest("Invalid json");
        }
    }
}
