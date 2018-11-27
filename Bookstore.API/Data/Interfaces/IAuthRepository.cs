using System.Threading.Tasks;
using Bookstore.API.Models;

namespace Bookstore.API.Data.Interfaces
{
    public interface IAuthRepository
    {
         Task<User> Register(User user, string password);
         Task<User> SignIn(string login, string password);
         Task<bool> UserExists(string login);
    }
}