using Microsoft.EntityFrameworkCore;
using Moq;
using SkiServiceAPI.Data;
using SkiServiceAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkiServiceAPI.Tests.MockData
{
    internal class MockUserData
    {

        public static List<User> Items()
        {
            var users = new List<User>();

            var userData = new List<(int Id, string Username, string PlainPassword, RoleNames Role)>
            {
                (1, "Superadmin", "super", RoleNames.SuperAdmin),
                (2, "Mitarbeiter 1", "m1", RoleNames.User),
                (3, "Mitarbeiter 2", "m2", RoleNames.User),
                (4, "Mitarbeiter 3", "m3", RoleNames.User),
                (5, "Mitarbeiter 4", "m4", RoleNames.User),
                (6, "Mitarbeiter 5", "m5", RoleNames.User),
                (7, "Mitarbeiter 6", "m6", RoleNames.User),
                (8, "Mitarbeiter 7", "m7", RoleNames.User),
                (9, "Mitarbeiter 8", "m8", RoleNames.User),
                (10, "Mitarbeiter 9", "m9", RoleNames.User),
                (11, "Mitarbeiter 10", "m10", RoleNames.User)
            };

            foreach (var user in userData)
            {
                CreatePasswordHash(user.PlainPassword, out byte[] passwordHash, out byte[] passwordSalt);

                users.Add(new User
                {
                    Id = user.Id,
                    Username = user.Username,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    Role = user.Role
                });
            }

            return users;
        }

        /// <summary>
        /// Coppied from SkiServiceAPI.Services.UserService.cs
        /// </summary>
        /// <param name="password">the password</param>
        /// <param name="passwordHash">the hash</param>
        /// <param name="passwordSalt">the salt</param>
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
