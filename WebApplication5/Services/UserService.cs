using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using WebApplication5.Models;
using WebApplication5.PasswordSecurity;

namespace WebApplication5.Services
{
    public class UserService : IUserService
    {
        private readonly UserContext _db;

        public UserService(UserContext userContext)
        {
            _db = userContext;
        }
        public IEnumerable<User> GetUsers()
        {
            return _db.Users.Include(user => user.Roles);
        }

        public User FindUserById(long userId)
        {
            return _db.Users.Include(user => user.Roles).FirstOrDefault(user => user.UserId == userId);
        }

        public User FindUserByEmail(string email)
        {
            return _db.Users.Include(user => user.Roles).FirstOrDefault(x => x.Email == email);
        }

        public User CreateUser(User userToAdd)
        {
           userToAdd.Password = PasswordStorage.CreateHash(userToAdd.Password);
           var user = _db.Users.Add(userToAdd);
            _db.SaveChanges();
            return user;
        }

        public User AddRoleToUser(long userId, long roleId)
        {
            var user = FindUserById(userId);
            var role = _db.Roles.Find(roleId);
            if (user == null || role == null)
            {
                return null;
            }
            user.Roles.Add(role);
            _db.SaveChanges();
            return user;
        }

        public List<User> CreateUsers(List<User> usersToAdd)
        {
            var users = _db.Users.AddRange(usersToAdd).ToList();
            _db.SaveChanges();
            return users;
        }

        public User UpdateUser(User user)
        {
            _db.Users.AddOrUpdate(user);
            _db.SaveChanges();
            return user;
        }

        public User DeleteUser(long userId)
        {
            var user = _db.Users.Find(userId);
            if (user != null)
            {
                _db.Users.Remove(user);
                _db.SaveChanges();
            }
            return user;
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}