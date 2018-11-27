using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Bookstore.API.Data.Interfaces;
using Bookstore.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.API.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext _context;
        public AuthRepository(DataContext context)
        {
            _context = context;
        }
        public async Task<User> SignIn(string login, string password)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Login == login);

            if(user == null)
                return null;

            if(!CheckPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            return user;
        }

        private bool CheckPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var HashMessageAuthCode = new HMACSHA512(passwordSalt))
            {
                var computedHash = HashMessageAuthCode.ComputeHash(Encoding.UTF8.GetBytes(password));
                if(!computedHash.SequenceEqual(passwordHash))
                    return false;
            }

            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var HashMessageAuthCode = new HMACSHA512())
            {
                passwordSalt = HashMessageAuthCode.Key;
                passwordHash = HashMessageAuthCode.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }

        public async Task<bool> UserExists(string login)
        {
            return await _context.Users.AnyAsync(x => x.Login == login);
        }
    }
}