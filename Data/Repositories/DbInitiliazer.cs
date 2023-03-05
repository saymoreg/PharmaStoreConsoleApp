using System;
using Core.Entities;
using Core.Helpers;
using Data.Contexts;

namespace Data
{
    public static class DbInitializer
    {
        static int id;
        public static void SeedAdmins()
        {
            var admins = new List<Admin>
            {
                new Admin
                {
                    Id = ++id,
                    Username = "admin",
                    Password = PasswordHasher.Encrypt("admin1"),
                },

                new Admin
                {
                    Id = ++id,
                    Username = "admin2",
                    Password = PasswordHasher.Encrypt("admin2"),
                },

                new Admin
                {
                    Id = ++id,
                    Username = "admin3",
                    Password = PasswordHasher.Encrypt("admin3")
                }
            };

            DbContext.Admins.AddRange(admins);
        }
    }
}

