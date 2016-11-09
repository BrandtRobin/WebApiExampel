using System;
using System.Diagnostics;
using System.Linq;
using WebApplication5.Interfaces;
using WebApplication5.Models;
using WebApplication5.PasswordSecurity;

namespace WebApplication5.Services
{
    public class LoginService : ILoginService
    {
        private readonly UserContext _db;
        private readonly IUserService _userService;

        public LoginService(UserContext userContext, IUserService userService)
        {
            _db = userContext;
            _userService = userService;
        }

        public Token Login(Credentials credentials)
        {
            var user = _userService.FindUserByEmail(credentials.Email);
            if (user == null) return null;

            if (!PasswordStorage.VerifyPassword(credentials.Password, user.Password)) return null;

            var token = _db.Tokens.FirstOrDefault(t => t.User.UserId == user.UserId);

            if (token != null)
            {
                _db.Tokens.Remove(token);
            }

            var newToken = new Token(user);
            _db.Tokens.Add(newToken);
            _db.SaveChanges();
            return newToken;
        }

        public bool ValidateToken(string tokenString)
        {
            Debug.WriteLine("VALIDATING");
            //var token = _db.Tokens.FirstOrDefault(t => t.TokenString == tokenString);
            var token = _db.Tokens.Find(tokenString);
            if (token == null) return false;
            if (token.ExpirationDate < DateTime.Today)
            {
                _db.Tokens.Remove(token);
                _db.SaveChanges();
                return false;
            }
            return true;
        }
    }
}