using System;
using Core.Entities;
using Core.Helpers;
using Data.Repositories.Concrete;

namespace Presentation.Services
{
    public class AdminService
    {
        private readonly AdminRepository _adminRepository;
        public AdminService()
        {
            _adminRepository = new AdminRepository();
        }
        public Admin Authorize()
        {
        LogInDesc: ConsoleHelper.WriteWithColor("--- Log In ---", ConsoleColor.DarkGreen);

            ConsoleHelper.WriteWithColor("Username: ", ConsoleColor.DarkCyan);
            string username = Console.ReadLine();
            ConsoleHelper.WriteWithColor("Password: ", ConsoleColor.DarkCyan);
            string password = Console.ReadLine();

            var admin = _adminRepository.GetUsernameAndPassword(username, password);

            if (admin is null)
            {
                ConsoleHelper.WriteWithColor("Invalid Username or Password :/", ConsoleColor.Red);
                goto LogInDesc;
            }
            return admin;
        }
    }
}

