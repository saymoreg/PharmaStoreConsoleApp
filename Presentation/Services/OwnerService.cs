using System;
using System.Globalization;
using Core.Entities;
using Core.Helpers;
using Data.Repositories.Abstract;
using Data.Repositories.Concrete;

namespace Presentation.Services
{
	public class OwnerService
	{
        private readonly OwnerRepository _ownerRepository;

        public OwnerService()
        {
            _ownerRepository = new OwnerRepository(); // creating connection with base
        }

        int id;

        public void GetAll()
        {
            OwnerDesc: _ownerRepository.GetAll();

            var owners = _ownerRepository.GetAll();
            if (owners is null)
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Owner By This ID...", ConsoleColor.Red);
                goto OwnerDesc;
            }

            ConsoleHelper.WriteWithColor("--- All Owners ---", ConsoleColor.DarkCyan);
            foreach (var owner in owners)
            {
                ConsoleHelper.WriteWithColor($"Owner's Fullname: {owner.Name} {owner.Surname}");
            }
        }

        public void Create()
        {
            ConsoleHelper.WriteWithColor("--- Enter Owner's Name ---", ConsoleColor.DarkCyan);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("--- Enter Owner's Surname ---", ConsoleColor.DarkCyan);
            string surname = Console.ReadLine();

            ////////////////////////////////////////////////////////////////

            var owner = new Owner
            {
                Id = id,
                Name = name,
                Surname = surname,
            };

            ////////////////////////////////////////////////////////////////

            _ownerRepository.Create(owner);
            ConsoleHelper.WriteWithColor($"Fullname: {owner.Name} {owner.Surname} has been created!", ConsoleColor.DarkGreen);
        }

        public void Update()
        {
            UpdateDesc: GetAll(); // calling all owners

            ConsoleHelper.WriteWithColor("Enter Owner's ID: ",ConsoleColor.DarkCyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid ID Format :(",ConsoleColor.Red);
                goto UpdateDesc; // going to the list of all owners to choose the correct owner's id
            }

            ////////////////////////////////////////////////////////////////

            var owner = _ownerRepository.GetById(id); // calling get method from owner repo

            if (owner is null)
            {
                ConsoleHelper.WriteWithColor("We Cant Find Any Owner By This ID :(",ConsoleColor.Red);
                goto UpdateDesc;
            }

            ////////////////////////////////////////////////////////////////

            ConsoleHelper.WriteWithColor("--- Enter New Owner's Name ---");
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("--- Enter New Owner's Surname ---");
            string surname = Console.ReadLine();

            ////////////////////////////////////////////////////////////////

            owner.Name = name;
            owner.Surname = surname;

            ////////////////////////////////////////////////////////////////

            _ownerRepository.Update(owner); // sending our updated information to data

            ConsoleHelper.WriteWithColor("Owner's Information Has Been Succesfuly Updated",ConsoleColor.Green);
        }

        public void Delete()
        {
            IdDesc: GetAll();

            ////////////////////////////////////////////////////////////////

            ConsoleHelper.WriteWithColor("--- Enter ID Of The Owner You Want To Delete ---",ConsoleColor.DarkCyan);

            int id;

            var isSucceeded = int.TryParse(Console.ReadLine(), out id);

            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid ID Format :(",ConsoleColor.Red);
                goto IdDesc;
            }

            ////////////////////////////////////////////////////////////////

            var ownerId = _ownerRepository.GetById(id); // calling owner by existing id

            if (ownerId is null)
            {
                ConsoleHelper.WriteWithColor("We Cant Find Any Owner By This ID :(",ConsoleColor.Red);
                goto IdDesc;
            }

            ////////////////////////////////////////////////////////////////

            _ownerRepository.Delete(ownerId); // if statement is not null than we call the owner from repo by its id and delete it from data

            ConsoleHelper.WriteWithColor("Owner Has Been Succesfuly Deleted",ConsoleColor.Green);
        }

    }
}

