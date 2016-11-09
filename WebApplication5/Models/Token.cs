using System;
using System.Security.Cryptography;

namespace WebApplication5.Models
{
    public class Token
    {
        public Token() { }

        public Token(User user)
        {
            TokenString = CreateTokenString();
            ExpirationDate = DateTime.Today.AddDays(30).Date;
            User = user;
        }

        public long TokenId { get; set; }
        public string TokenString { get; set; }
        public DateTime ExpirationDate { get; set; }
        public User User { get; set; }

        public string CreateTokenString()
        {
            RandomNumberGenerator rng = new RNGCryptoServiceProvider();

            var tokenData = new byte[32];
            rng.GetBytes(tokenData);

            var token = Convert.ToBase64String(tokenData);

            return token;
        }
    }

}