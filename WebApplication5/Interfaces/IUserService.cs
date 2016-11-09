using System.Collections.Generic;
using WebApplication5.Models;

namespace WebApplication5.Services
{
    public interface IUserService
    {
        IEnumerable<User> GetUsers();
        User FindUserById(long userId);
        User FindUserByEmail(string email);
        User CreateUser(User userToAdd);
        List<User> CreateUsers(List<User> usersToAdd);
        User UpdateUser(User user);
        User DeleteUser(long userId);
        User AddRoleToUser(long userId, long roleId);
    }
}