using System.Collections.Generic;
using System.Linq;
using WebApplication5.Interfaces;
using WebApplication5.Models;

namespace WebApplication5.Services
{
    public class RoleService : IRoleService
    {
        private readonly UserContext _db;

        public RoleService(UserContext userContext)
        {
            _db = userContext;
        }

        public IEnumerable<Role> GetAllRoles()
        {
            return _db.Roles;
        }

        public Role FindRoleByRoleId(long roleId)
        {
            return _db.Roles.Find(roleId);
        }

        public Role FindRoleByRoleName(string roleName)
        {
            return _db.Roles.FirstOrDefault(x => x.RoleName == roleName);
        }

        public Role CreateRole(Role roleToAdd)
        {
            var role = _db.Roles.FirstOrDefault(x => x.RoleName == roleToAdd.RoleName);
            if (role == null)
            {
                _db.Roles.Add(roleToAdd);
                _db.SaveChanges();
                return roleToAdd;
            }
            return null;
        }
    }
}