using SkiServiceAPI.Data;
using SkiServiceAPI.Interfaces;
using SkiServiceAPI.Models;

namespace SkiServiceAPI.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDBContext _context;

        public UserService(ApplicationDBContext context)
        {
            _context = context;
        }

        public void CreateUser(string username, string password, RoleNames role = RoleNames.Mitarbeiter)
        {
            CreatePasswordHash(password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = role
            };

            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public RoleNames GetRole(string username)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) return RoleNames.Mitarbeiter;
            return user.Role;
        }

        public bool VerifyPassword(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) return false;

            return VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt);
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
    }
}
