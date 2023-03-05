using System;
using Core.Entities;
using Core.Helpers;
using Data.Repositories.Abstract;
using Data.Repositories.Concrete;

namespace Presentation.Services
{
    public class DrugService
    {
        private readonly DrugRepository _drugRepository;
        private readonly DrugstoreRepository _drugstoreRepository;
        private readonly DrugstoreService _drugstoreService;
        int id;

        public DrugService()
        {
            _drugRepository = new DrugRepository();
            _drugstoreRepository = new DrugstoreRepository();
            _drugstoreService = new DrugstoreService();
        }

        public void GetAll()
        {
        DrugDesc: _drugRepository.GetAll();

            ////////////////////////////////////////////////////////////////

            var drugs = _drugRepository.GetAll();
            if (drugs is null)
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Drug By This ID...", ConsoleColor.Red);
                goto DrugDesc;
            }

            ////////////////////////////////////////////////////////////////

            ConsoleHelper.WriteWithColor("--- All Drugs ---", ConsoleColor.DarkCyan);
            foreach (var drug in drugs)
            {
                ConsoleHelper.WriteWithColor($"Drug: {drug.Name}, Drug ID: {drug.Id}, Price: {drug.Price}, Count: {drug.Count}, Drugstore Name: {drug.Drugstore.Name}, Drugstore ID: {drug.Drugstore.Id}");
            }
        }

        public void GetAllDrugsByDrugstore()
        {
            if (_drugRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Drug In The List....", ConsoleColor.Red);
            }

            ////////////////////////////////////////////////////////////////

            _drugstoreService.GetAll();

        DrugStoreIdDes: ConsoleHelper.WriteWithColor("--- Enter Drugstore ID ---", ConsoleColor.DarkCyan);
            int drugStoreId;
            bool issucceeded = int.TryParse(Console.ReadLine(), out drugStoreId);
            if (!issucceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid ID Format....", ConsoleColor.Red);
                goto DrugStoreIdDes;
            }

            ////////////////////////////////////////////////////////////////

            var drugStore = _drugstoreRepository.GetById(drugStoreId);
            if (drugStore is null)
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Drugstore By This ID :(", ConsoleColor.Red);
                goto DrugStoreIdDes;
            }

            ////////////////////////////////////////////////////////////////

            var drugs = _drugRepository.GetAll().Where(d => d.Drugstore == drugStore);
            if (drugs.Count() == 0)
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Drug By This Drugstore :(", ConsoleColor.Red);
                goto DrugStoreIdDes;
            }

            ////////////////////////////////////////////////////////////////

            foreach (var drug in drugs)
            {
                ConsoleHelper.WriteWithColor($"Name: {drug.Name},ID: {drug.Id}, Price: {drug.Price}, Count: {drug.Count}, DrugStore: {drug.Drugstore.Name}", ConsoleColor.DarkCyan);
            }
        }

        public void Filter()
        {
            if (_drugRepository.GetAll().Count != 0)
            {
            MaxPriceDesc: ConsoleHelper.WriteWithColor("--- Enter Max Price Of Drug ---", ConsoleColor.DarkCyan);
                int maxPrice;
                bool issucceeded = int.TryParse(Console.ReadLine(), out maxPrice);
                if (!issucceeded)
                {
                    ConsoleHelper.WriteWithColor("Invalid Price Format....", ConsoleColor.Red);
                    goto MaxPriceDesc;
                }

                ////////////////////////////////////////////////////////////////

                if (maxPrice <= 0)
                {
                    ConsoleHelper.WriteWithColor("Max Price Can't be Less Than Or Equal 0.....", ConsoleColor.Red);
                    goto MaxPriceDesc;
                }

                ////////////////////////////////////////////////////////////////

                var drugs = _drugRepository.GetAll().Where(d => d.Price <= maxPrice);

                if (drugs.Count() == 0)
                {
                    ConsoleHelper.WriteWithColor("We Can't Find Ant Drugs Which Is Less Than Inputed Digit", ConsoleColor.Red);
                    goto MaxPriceDesc;
                }

                ////////////////////////////////////////////////////////////////

                foreach (var drug in drugs)
                {
                    ConsoleHelper.WriteWithColor($"Drug Name: {drug.Name}, ID: {drug.Id}, Price: {drug.Price}, Count: {drug.Count}, DrugStore Id: {drug.Drugstore.Id}, DrugStore Name: {drug.Drugstore.Name}", ConsoleColor.DarkCyan);
                }
            }

            else
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Drug In The List....", ConsoleColor.Red);
            }
        }

        public void Create()
        {
            if (_drugstoreRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("Firstly You Should Create a Drugstore....", ConsoleColor.Red);
                return;
            }

        ////////////////////////////////////////////////////////////////

        DrugDesc: ConsoleHelper.WriteWithColor("--- Enter Drug's ID ---", ConsoleColor.DarkCyan);

            int drugId;

            bool isSucceded = int.TryParse(Console.ReadLine(), out drugId);
            if (!isSucceded)
            {
                ConsoleHelper.WriteWithColor("Invalid ID Format....", ConsoleColor.Red);
                goto DrugDesc;
            }

            ////////////////////////////////////////////////////////////////

            ConsoleHelper.WriteWithColor("--- Enter Drug's Name ---", ConsoleColor.DarkCyan);
            string name = Console.ReadLine();

        ////////////////////////////////////////////////////////////////

        PriceDesc: ConsoleHelper.WriteWithColor("--- Enter Drug's Price ---", ConsoleColor.DarkCyan);
            double drugPrice;
            bool isSucceeded = double.TryParse(Console.ReadLine(), out drugPrice);
            if (!isSucceded)
            {
                ConsoleHelper.WriteWithColor("Invalid Price Format :(", ConsoleColor.Red);
                goto PriceDesc;
            }

            if (drugPrice <= 0)
            {
                ConsoleHelper.WriteWithColor("Drug Can't Cost 0 Or Less Than 0 ....", ConsoleColor.Red);
                goto PriceDesc;
            }

        ////////////////////////////////////////////////////////////////

        CountDesc: ConsoleHelper.WriteWithColor("--- Enter Drug's Count ---", ConsoleColor.DarkCyan);
            int count;
            isSucceded = int.TryParse(Console.ReadLine(), out count);
            if (count <= 0)
            {
                ConsoleHelper.WriteWithColor("Drug Count Can't Be Less Than 0...", ConsoleColor.Red);
                goto CountDesc;
            }

        DrugStoreIdDesc: _drugstoreService.GetAll();
            ConsoleHelper.WriteWithColor("--- Enter Drugstore's ID Of The Drug ---", ConsoleColor.DarkCyan);
            int drugStoreId;
            isSucceded = int.TryParse(Console.ReadLine(), out drugStoreId);
            if (!isSucceded)
            {
                ConsoleHelper.WriteWithColor("Invalid ID Format....", ConsoleColor.Red);
                goto DrugStoreIdDesc;
            }

            var drugStore = _drugstoreRepository.GetById(drugStoreId);
            if (drugStore is null)
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Drugstore By This ID :(", ConsoleColor.Red);
                goto DrugStoreIdDesc;
            }

            var drug = new Drug
            {
                Id = id,
                Name = name,
                Price = drugPrice,
                Count = count,
                Drugstore = drugStore
            };

            _drugRepository.Create(drug);

            ConsoleHelper.WriteWithColor($"Drug: {drug.Name}, Drug ID: {drug.Id}, Price: {drug.Price}, Count: {drug.Count}, Drugstore: {drug.Drugstore.Name}, has been succesfuly created!", ConsoleColor.Green);
        }

        public void Update()
        {
            if (_drugRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Drug To Update :(", ConsoleColor.Red);
            }

            ////////////////////////////////////////////////////////////////

            GetAll();

            ////////////////////////////////////////////////////////////////


            DrugIdDesc: ConsoleHelper.WriteWithColor("--- Enter Drug ID ---", ConsoleColor.DarkCyan);


            int drugId;
            bool issucceeded = int.TryParse(Console.ReadLine(), out drugId);
            if (!issucceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid ID Format.....", ConsoleColor.Red);
                goto DrugIdDesc;
            }

            ////////////////////////////////////////////////////////////////

            var drug = _drugRepository.GetById(drugId);
            if (drug is null)
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Drug By This ID....", ConsoleColor.Red);
                goto DrugIdDesc;
            }

            ////////////////////////////////////////////////////////////////

            ConsoleHelper.WriteWithColor("--- Enter New Drug Name ---", ConsoleColor.Cyan);
            string name = Console.ReadLine();

            DrugPriceDesc: ConsoleHelper.WriteWithColor("--- Enter New Drug Price ---", ConsoleColor.DarkCyan);
            double price;
            issucceeded = double.TryParse(Console.ReadLine(), out price);
            if (!issucceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid Price Format....", ConsoleColor.Red);
                goto DrugPriceDesc;
            }

            ////////////////////////////////////////////////////////////////

            if (price <= 0)
            {
                ConsoleHelper.WriteWithColor("Price Can't Be Less Than Or Equal 0....", ConsoleColor.Red);
                goto DrugPriceDesc;
            }

            ////////////////////////////////////////////////////////////////

            DrugCountDesc: ConsoleHelper.WriteWithColor("--- Enter New Drug Count ---", ConsoleColor.Cyan);
            int updatedCount;
            issucceeded = int.TryParse(Console.ReadLine(), out updatedCount);
            if (!issucceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid Count Format....", ConsoleColor.Red);
                goto DrugCountDesc;
            }

            ////////////////////////////////////////////////////////////////

            if (updatedCount <= 0)
            {
                ConsoleHelper.WriteWithColor("New Count Can't Be Less Than Or Equal 0....", ConsoleColor.Red);
                goto DrugCountDesc;
            }

            ////////////////////////////////////////////////////////////////

            DrugstoreIdDesc: ConsoleHelper.WriteWithColor("--- Enter Drugstore ID ---", ConsoleColor.Cyan);

            ////////////////////////////////////////////////////////////////

            _drugstoreService.GetAll();

            ////////////////////////////////////////////////////////////////

            int drugStoreId;
            issucceeded = int.TryParse(Console.ReadLine(), out drugStoreId);
            if (!issucceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid ID Format....", ConsoleColor.Red);
                goto DrugstoreIdDesc;
            }

            ////////////////////////////////////////////////////////////////

            var drugStore = _drugstoreRepository.GetById(drugStoreId);
            if (drugStore is null)
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Drugstore By This ID", ConsoleColor.Red);
                goto DrugstoreIdDesc;
            }
        }

        public void Delete()
        {
        IdDesc: GetAll();

            ConsoleHelper.WriteWithColor("--- Enter ID Of The Drug You Want To Delete ---", ConsoleColor.DarkCyan);

            int id;
            var isSucceeded = int.TryParse(Console.ReadLine(), out id);

            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid ID Format :(", ConsoleColor.Red);
                goto IdDesc;
            }

            ////////////////////////////////////////////////////////////////

            var drug = _drugRepository.GetById(id);

            if (drug is null)
            {
                ConsoleHelper.WriteWithColor("We Cant Find Any Drug By This ID :(", ConsoleColor.Red);
                goto IdDesc;
            }

            ////////////////////////////////////////////////////////////////

            _drugRepository.Delete(drug);

            ConsoleHelper.WriteWithColor($"Drug Has Been Succesfuly Deleted from {drug.Drugstore.Name} ! ", ConsoleColor.Green);
        }
    }
}

