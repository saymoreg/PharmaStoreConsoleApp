using System;
using Core.Entities;
using Core.Helpers;
using Data.Repositories.Abstract;
using Data.Repositories.Concrete;

namespace Presentation.Services
{
	public class DruggistService
	{
		static int id;
		private readonly DruggistRepository _druggistRepository;
		private readonly DrugstoreRepository _drugstoreRepository;
		private readonly DrugstoreService _drugstoreService;

		public DruggistService()
		{
			_druggistRepository = new DruggistRepository();
			_drugstoreRepository = new DrugstoreRepository();
			_drugstoreService = new DrugstoreService();
        }
        public void GetAll()
		{
            DruggistDesc: _druggistRepository.GetAll();

            var druggists = _druggistRepository.GetAll();
            if (druggists is null)
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Drugstore By This ID...", ConsoleColor.Red);
                goto DruggistDesc;
            }

            ConsoleHelper.WriteWithColor("--- All Drugstores ---", ConsoleColor.DarkCyan);
            foreach (var druggist in druggists)
            {
                ConsoleHelper.WriteWithColor($"Druggist's Fullname: {druggist.Name} {druggist.Surname}, Age: {druggist.Age}, Experience: {druggist.Experience}, Drugstore where he belongs to {druggist?.Drugstore}");
            }
        }

        public void GetAllDruggistByDrugstore()
        {
            if (_druggistRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Druggist Which Linked To Any Drugstore....",ConsoleColor.Red);
                return;
            }

            ////////////////////////////////////////////////////////////////

            DrugStoreIdDesc: _drugstoreRepository.GetAll();
            ConsoleHelper.WriteWithColor("--- Enter Drugstore's ID ---",ConsoleColor.DarkCyan);
            int drugStoreId;
            bool isSucceeded = int.TryParse(Console.ReadLine(),out drugStoreId);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid ID Format...",ConsoleColor.Red);
                goto DrugStoreIdDesc;
            }

            ////////////////////////////////////////////////////////////////

            var drugStore = _drugstoreRepository.GetById(drugStoreId);
            if (drugStore is null)
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Drugstore By This ID :(",ConsoleColor.Red);
                goto DrugStoreIdDesc;
            }

            ////////////////////////////////////////////////////////////////

            var druggists = _druggistRepository.GetAll().Where(d => d.Drugstore == drugStore);
            if (druggists.Count() == 0)
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Druggist In This Drugstore :(",ConsoleColor.Red);
                goto DrugStoreIdDesc;
            }

            ////////////////////////////////////////////////////////////////

            foreach (var druggist in druggists)
            {
                ConsoleHelper.WriteWithColor($"Druggist Fulname: {druggist.Name} {druggist.Surname}, ID: {druggist.Id}, Age: {druggist.Age}, Experience: {druggist.Experience}, Drugstore: {drugStore.Name}");
            }
        }

        public void Create()
		{
			if (_drugstoreRepository.GetAll().Count != 0)
			{
				id++; // we are plussing it to make unique id
				ConsoleHelper.WriteWithColor("--- Enter Druggist's Name ---",ConsoleColor.DarkCyan);
				string name = Console.ReadLine();
				ConsoleHelper.WriteWithColor("--- Enter Druggist's Surname ---", ConsoleColor.DarkCyan);
				string surname = Console.ReadLine();

				////////////////////////////////////////////////////////////////

				DruggistAgeDesc: ConsoleHelper.WriteWithColor("--- Enter Druggist's Age ---",ConsoleColor.DarkCyan);
				int age;
				bool isSucceeded = int.TryParse(Console.ReadLine(), out age);
				if (!isSucceeded)
				{
					ConsoleHelper.WriteWithColor("Invalid Age Input :(",ConsoleColor.Red);
					goto DruggistAgeDesc;
				}

				if (age < 16 && age > 65)
				{
					ConsoleHelper.WriteWithColor("Druggist Can't Be Less Than 16 And More Than 65 Years Old",ConsoleColor.Red);
				}

				////////////////////////////////////////////////////////////////

				ConsoleHelper.WriteWithColor("--- Enter Druggist's Experience ---",ConsoleColor.DarkCyan);
                int experience;
                isSucceeded = int.TryParse(Console.ReadLine(), out experience);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Invalid Experience Input :(", ConsoleColor.Red);
                }

				if (age - experience < 16)
				{
					ConsoleHelper.WriteWithColor("Age Of Druggist Can't be More Than His Experience...",ConsoleColor.Red);
				}

                ////////////////////////////////////////////////////////////////

                DrugstoreDesc: _drugstoreService.GetAll(); // calling all drugstores from service

				ConsoleHelper.WriteWithColor("--- Enter Drugstore's ID Where Druggist Works ---",ConsoleColor.DarkCyan);
				int drugstoreId;
				isSucceeded = int.TryParse(Console.ReadLine(),out drugstoreId);
				if (!isSucceeded)
				{
					ConsoleHelper.WriteWithColor("Invalid Drugstore ID Format :(",ConsoleColor.Red);
					goto DrugstoreDesc;
				}

				var drugStore = _drugstoreRepository.GetById(drugstoreId); // calling existing drugstore by its id

				if (drugStore is null)
				{
					ConsoleHelper.WriteWithColor("We Cant Find Any Drugstore By This ID :(",ConsoleColor.Red);
				}

				////////////////////////////////////////////////////////////////

				var druggist = new Druggist
				{
					Id = id,
					Name = name,
					Surname = surname,
					Age = age,
					Experience = experience,
					Drugstore = drugStore
				};

				_druggistRepository.Create(druggist); // creating and addign druggist to base

				ConsoleHelper.WriteWithColor($"Druggist: {druggist.Name} {druggist.Surname}, ID: {druggist.Id}, Experience: {druggist.Experience}, Drugstore: {druggist.Drugstore} is succesfuly created!",ConsoleColor.Green);

				////////////////////////////////////////////////////////////////
			}
			else
			{
				ConsoleHelper.WriteWithColor("Firstly You Have To Create A Drugstore!",ConsoleColor.Red);
			}
        }

		public void Update()
		{
            GetAll();

            ////////////////////////////////////////////////////////////////

            if (_druggistRepository.GetAll().Count == 0)
            {
                return;
            }
        UpdateDesc: ConsoleHelper.WriteWithColor("Enter Drugstore's ID ---", ConsoleColor.DarkCyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid ID Format :(", ConsoleColor.Red);
                goto UpdateDesc;
            }
            var druggist = _druggistRepository.GetById(id);
            if (druggist is null)
            {
                ConsoleHelper.WriteWithColor("We Cant Find Any Druggist By This ID", ConsoleColor.Red);
                goto UpdateDesc;
            }

            ////////////////////////////////////////////////////////////////

            ConsoleHelper.WriteWithColor("--- Enter New Druggist Name ---", ConsoleColor.DarkCyan);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("--- Enter New Druggist Surname ---", ConsoleColor.DarkCyan);
            string surname = Console.ReadLine();
        EnterAgeDesc: ConsoleHelper.WriteWithColor("--- Enter New Druggist Age ---", ConsoleColor.DarkCyan);
            byte age;
            isSucceeded = byte.TryParse(Console.ReadLine(), out age);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Incorrect Age Format :(", ConsoleColor.Red);
                goto EnterAgeDesc;
            }

            ////////////////////////////////////////////////////////////////

        EnterExperienceDesc: ConsoleHelper.WriteWithColor("--- Enter New Druggist Experience ---", ConsoleColor.DarkCyan);
            byte experience;
            isSucceeded = byte.TryParse(Console.ReadLine(), out experience);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Incorrect Experience Format...", ConsoleColor.Red);
                goto EnterExperienceDesc;
            }
            if (experience > age - 18)
            {
                ConsoleHelper.WriteWithColor("Experience Can't be more than Age....", ConsoleColor.Red);
                goto EnterExperienceDesc;
            }

            ////////////////////////////////////////////////////////////////

            _drugstoreService.GetAll();
        EnterIdDesc: ConsoleHelper.WriteWithColor("--- Enter New DrugStore ID ---");
            int drugStoreId;
            isSucceeded = int.TryParse(Console.ReadLine(), out drugStoreId);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Incorrect ID Format", ConsoleColor.Red);
                goto EnterIdDesc;
            }
            var drugStore = _drugstoreRepository.GetById(drugStoreId);
            if (drugStore is null)
            {
                ConsoleHelper.WriteWithColor("We Cant Find Any Drugstore By This ID..", ConsoleColor.Red);
                goto EnterIdDesc;
            }

            ////////////////////////////////////////////////////////////////

            druggist.Name = name;
            druggist.Surname = surname;
            druggist.Age = age;
            druggist.Experience = experience;
            druggist.Drugstore = drugStore;

            ////////////////////////////////////////////////////////////////

            _druggistRepository.Update(druggist);
            ConsoleHelper.WriteWithColor("--- Druggist Is Succesfuly Updated ---", ConsoleColor.Green);
        }

		public void Delete()
		{
            GetAll();

            if (_druggistRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Druggist To Delete :(",ConsoleColor.Red);
                return;
            }

            ////////////////////////////////////////////////////////////////
            EnterIdDesc: ConsoleHelper.WriteWithColor("--- Enter Druggist's ID ---", ConsoleColor.DarkCyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Incorrect ID Format....", ConsoleColor.Red);
                goto EnterIdDesc;
            }

            ////////////////////////////////////////////////////////////////

            var dbDruggist = _druggistRepository.GetById(id);
            if (dbDruggist is null)
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Druggist By This ID :(", ConsoleColor.Red);
                goto EnterIdDesc;
            }

            ////////////////////////////////////////////////////////////////

            _druggistRepository.Delete(dbDruggist);
            ConsoleHelper.WriteWithColor("Druggist Is Succesfuly Deleted!", ConsoleColor.Green);
        }
	}
}

