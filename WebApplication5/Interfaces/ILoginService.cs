using WebApplication5.Models;

namespace WebApplication5.Interfaces
{
    public interface ILoginService
    {
        Token Login(Credentials credentials);
        bool ValidateToken(string tokenString);
    }
}