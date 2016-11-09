using System.Collections.Generic;
using WebApplication5.Models;

namespace WebApplication5.Interfaces
{
    public interface IRoleService
    {
        IEnumerable<Role> GetAllRoles();
        Role FindRoleByRoleName(string roleName);
        Role FindRoleByRoleId(long roleId);
        Role CreateRole(Role roleToAdd);
        //Role GetRoleById(long roleId);
        //Role UpdateRole(Role role);
        //Role DeleteRole(long roleId);
    }
}
