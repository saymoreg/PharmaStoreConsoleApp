using System;
using Core.Entities;
using Core.Helpers;
using Data.Contexts;

namespace Data.Repositories.Concrete
{
	public class AdminRepository
	{
        public Admin GetUsernameAndPassword(string username, string password)
        {
            return DbContext.Admins.FirstOrDefault(a => a.Username.ToLower() == username.ToLower() && PasswordHasher.Decrypt(a.Password) == password.ToLower());
        }
    }
}

