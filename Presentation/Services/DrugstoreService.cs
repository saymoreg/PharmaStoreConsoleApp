﻿using System;
using System.Globalization;
using System.Text.RegularExpressions;
using Core.Entities;
using Core.Extentions;
using Core.Helpers;
using Data.Repositories.Abstract;
using Data.Repositories.Concrete;

namespace Presentation.Services
{
    public class DrugstoreService
    {
        private readonly OwnerRepository _ownerRepository;
        private readonly DrugstoreRepository _drugStoreRepository;
        private readonly DrugRepository _drugRepository;
        private readonly OwnerService _ownerService;

        public DrugstoreService()
        {
            _ownerRepository = new OwnerRepository();
            _drugStoreRepository = new DrugstoreRepository(); // creating connection with base
            _drugRepository = new DrugRepository();
            _ownerService = new OwnerService();
        }

        public void GetAll()
        {
        DrugStoreDesc: _drugStoreRepository.GetAll();

            var drugstores = _drugStoreRepository.GetAll();
            if (drugstores is null)
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Drugstore By This ID...", ConsoleColor.Red);
                goto DrugStoreDesc;
            }

            ConsoleHelper.WriteWithColor("--- All Drugstores ---", ConsoleColor.DarkCyan);
            foreach (var drugstore in drugstores)
            {
                ConsoleHelper.WriteWithColor($"Drugstore: {drugstore.Name}, Drugstore ID: {drugstore.Id}");
            }
        }

        public void GetAllDrugstoresByOwner()
        {
            if (_drugStoreRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("We Cantf Find Any Drugstore In The List....", ConsoleColor.Red);
                return;
            }
            else
            {
            OwnerIdDesc: _ownerService.GetAll();

                ////////////////////////////////////////////////////////////////

                ConsoleHelper.WriteWithColor("--- Enter Owner's ID ---", ConsoleColor.DarkCyan);
                int ownerId;
                bool isSucceeded = int.TryParse(Console.ReadLine(), out ownerId);
                if (!isSucceeded)
                {
                    ConsoleHelper.WriteWithColor("Invalid ID Format....", ConsoleColor.Red);
                    goto OwnerIdDesc;
                }

                ////////////////////////////////////////////////////////////////

                var owner = _ownerRepository.GetById(ownerId);
                if (owner is null)
                {
                    ConsoleHelper.WriteWithColor("We Can't Find Any Owner By This ID :(", ConsoleColor.Red);
                    goto OwnerIdDesc;
                }

                ////////////////////////////////////////////////////////////////

                var drugStores = _drugStoreRepository.GetAll().Where(st => st.Owner == owner);
                if (drugStores.Count() == 0)
                {
                    ConsoleHelper.WriteWithColor("This Owner Does Not Have Any Drugstore :(", ConsoleColor.Red);
                    goto OwnerIdDesc;
                }

                ////////////////////////////////////////////////////////////////

                foreach (var drugStore in drugStores)
                {
                    ConsoleHelper.WriteWithColor($"Drugstore: {drugStore.Name}, ID: {drugStore.Id}, Address: {drugStore.Address}, Contact Number & Email: {drugStore.ContactNumber} {drugStore.Email}");
                }
            }
        }

        public void Create()
        {
            if (_ownerRepository.GetAll().Count == 0)
            {
                ConsoleHelper.WriteWithColor("First You Should Create An Owner...", ConsoleColor.DarkCyan);
                return;
            }

            ////////////////////////////////////////////////////////////////

            ConsoleHelper.WriteWithColor("--- Enter DrugStore's Name ---", ConsoleColor.DarkCyan);
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("--- Enter DrugStore's Address ---", ConsoleColor.DarkCyan);
            string address = Console.ReadLine();
            ConsoleHelper.WriteWithColor("--- Enter Drugstore's Email ---", ConsoleColor.DarkCyan);
            string email = Console.ReadLine();
            ConsoleHelper.WriteWithColor("--- Enter Drugstore's Contact Number ---", ConsoleColor.DarkCyan);
            string contactNumber = Console.ReadLine();

        ////////////////////////////////////////////////////////////////

        EnterIdDesc: ConsoleHelper.WriteWithColor("--- Enter Owner's ID ---", ConsoleColor.DarkCyan);
            int ownerId;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out ownerId);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid ID Format....", ConsoleColor.Red);
                goto EnterIdDesc;
            }
            var owner = _ownerRepository.GetById(ownerId);
            if (owner is null)
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Owner By This ID....", ConsoleColor.Red);
                goto EnterIdDesc;
            }

            ////////////////////////////////////////////////////////////////

            var drugStore = new Drugstore
            {
                Name = name,
                Address = address,
                Email = email,
                ContactNumber = contactNumber,
                Owner = owner
            };

            ////////////////////////////////////////////////////////////////

            owner.Drugstores.Add(drugStore);
            _drugStoreRepository.Create(drugStore);  // similar to add
            ConsoleHelper.WriteWithColor($"Drugstore: {drugStore.Name} is Succesfuly Created", ConsoleColor.Green);
        }

        public void Update()
        {
            GetAll();

            if (_drugStoreRepository.GetAll().Count == 0)
            {
                return; // if there is no drugstores we are going away from if statement
            }
        UpdateDesc: ConsoleHelper.WriteWithColor("--- Enter Drugstore's ID ---", ConsoleColor.Cyan);
            int id;
            bool isSucceeded = int.TryParse(Console.ReadLine(), out id);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid ID Format....", ConsoleColor.Red);
                goto UpdateDesc;
            }

            ////////////////////////////////////////////////////////////////

            var drugStore = _drugStoreRepository.GetById(id);
            if (drugStore is null)
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Drugstore By This ID...", ConsoleColor.Red);
                goto UpdateDesc;
            }

            ////////////////////////////////////////////////////////////////

            ConsoleHelper.WriteWithColor("--- Enter New Drugstore's Name ---");
            string name = Console.ReadLine();
            ConsoleHelper.WriteWithColor("--- Enter New DrugStore's Address ---", ConsoleColor.Cyan);
            string address = Console.ReadLine();

            ConsoleHelper.WriteWithColor("--- Enter new DrugStore Contact Number ---", ConsoleColor.Cyan);
            string contactnumber = Console.ReadLine();

        ////////////////////////////////////////////////////////////////

        EmailDesc: ConsoleHelper.WriteWithColor("Enter New Drugstore Email", ConsoleColor.Cyan);
            string email = Console.ReadLine();
            if (!email.IsEmail())
            {
                ConsoleHelper.WriteWithColor("Incorrect Email Format....", ConsoleColor.Red);
                goto EmailDesc;
            }

            if (_drugStoreRepository.IsDublicatedEmail(email))
            {
                ConsoleHelper.WriteWithColor("This Email Is Already Used :(", ConsoleColor.Red);
                goto EmailDesc;
            }

            ////////////////////////////////////////////////////////////////

            _ownerService.GetAll();
        EnterIdDesc: ConsoleHelper.WriteWithColor("--- Enter Owner's ID ---", ConsoleColor.Cyan);
            int ownerid;
            isSucceeded = int.TryParse(Console.ReadLine(), out ownerid);
            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid ID Format....", ConsoleColor.Red);
                goto EnterIdDesc;
            }
            var owner = _ownerRepository.GetById(ownerid);
            if (owner is null)
            {
                ConsoleHelper.WriteWithColor("We Can't Find Any Owner By This ID....", ConsoleColor.Red);
                goto EnterIdDesc;
            }

            ////////////////////////////////////////////////////////////////


            drugStore.Name = name;
            drugStore.Email = email;
            drugStore.Address = address;
            drugStore.ContactNumber = contactnumber;
            drugStore.Owner = owner;

            ////////////////////////////////////////////////////////////////

            _drugStoreRepository.Update(drugStore);
            ConsoleHelper.WriteWithColor("DrugStore is succesfully updating", ConsoleColor.Green);
        }

        public void Delete()
        {
        IdDesc: GetAll();

            ConsoleHelper.WriteWithColor("--- Enter ID Of The Drugstore You Want To Delete ---", ConsoleColor.DarkCyan);

            int id;
            var isSucceeded = int.TryParse(Console.ReadLine(), out id);

            if (!isSucceeded)
            {
                ConsoleHelper.WriteWithColor("Invalid ID Format :(", ConsoleColor.Red);
                goto IdDesc;
            }

            ////////////////////////////////////////////////////////////////

            var drugStore = _drugStoreRepository.GetById(id);

            if (drugStore is null)
            {
                ConsoleHelper.WriteWithColor("We Cant Find Any Drugstore By This ID :(", ConsoleColor.Red);
                goto IdDesc;
            }

            ////////////////////////////////////////////////////////////////

            _drugStoreRepository.Delete(drugStore);

            ConsoleHelper.WriteWithColor("Drugstore Has Been Succesfuly Deleted", ConsoleColor.Green);
        }

        public void Sale()
        {
        SaleDesc: if (_drugRepository.GetAll().Count != 0)
            {
                ConsoleHelper.WriteWithColor("-- All Drugs ---", ConsoleColor.Blue);
                var drugs = _drugRepository.GetAll();
                foreach (var drug in drugs)
                {
                    ConsoleHelper.WriteWithColor($"Name: {drug.Name}, ID: {drug.Id} Price: {drug.Price}, Count: {drug.Count},DrugStore Name: {drug.Drugstore.Name}", ConsoleColor.Cyan);
                }

            ////////////////////////////////////////////////////////////////

            DrugidDesc: ConsoleHelper.WriteWithColor("--- Enter Drug's ID You Want To Sell ---", ConsoleColor.DarkCyan);
                int drugId;
                bool issucceeded = int.TryParse(Console.ReadLine(), out drugId);
                if (!issucceeded)
                {
                    ConsoleHelper.WriteWithColor("Invalid ID Format", ConsoleColor.Red);
                    goto DrugidDesc;
                }

                ////////////////////////////////////////////////////////////////

                var saleDrug = _drugRepository.GetById(drugId);
                if (saleDrug is null)
                {
                    ConsoleHelper.WriteWithColor("We Can't Find Any Drug By This ID....", ConsoleColor.Red);
                    goto DrugidDesc;
                }

                ////////////////////////////////////////////////////////////////

                if (saleDrug.Count != 0)
                {


                    ConsoleHelper.WriteWithColor("--- Enter The Quantity Of Drugs You Want To Sell ---", ConsoleColor.DarkCyan;
                    int quantity;
                    issucceeded = int.TryParse(Console.ReadLine(), out quantity);
                    if (!issucceeded)
                    {
                        ConsoleHelper.WriteWithColor("Invalid Quantity Format....", ConsoleColor.Red);
                        goto DrugidDesc;
                    }

                    ////////////////////////////////////////////////////////////////

                    if (quantity <= 0)
                    {
                        ConsoleHelper.WriteWithColor("Quantity Can't Be Less Than Or Equal 0....", ConsoleColor.Red);
                        goto DrugidDesc;
                    }

                    ////////////////////////////////////////////////////////////////

                    if (quantity < saleDrug.Count && quantity == 1)
                    {
                        var commonPrice = saleDrug.Price * quantity;
                        saleDrug.Count -= quantity;
                        ConsoleHelper.WriteWithColor($"{quantity} Drug(s) Is/Are Sold. Common Price Is {commonPrice}", ConsoleColor.Green);
                    }

                    ////////////////////////////////////////////////////////////////

                    if (quantity < saleDrug.Count && quantity != 1)
                    {
                        var commonPrice = saleDrug.Price * quantity;
                        saleDrug.Count -= quantity;
                        ConsoleHelper.WriteWithColor($"{quantity} drug(s) Is/Are Sold. Common Price Is {commonPrice}", ConsoleColor.Green);
                    }

                    ////////////////////////////////////////////////////////////////

                    if (quantity > saleDrug.Count)
                    {
                    SelectionDescription: ConsoleHelper.WriteWithColor($"We Have Only {saleDrug.Count} Drugs. Do You Want To Get Them? 1-YES, 2-NO", ConsoleColor.Red);
                        int selection;
                        issucceeded = int.TryParse(Console.ReadLine(), out selection);
                        if (!issucceeded)
                        {
                            ConsoleHelper.WriteWithColor("Invalid Selection Format....", ConsoleColor.Red);
                            goto SelectionDescription;
                        }

                        ////////////////////////////////////////////////////////////////

                        if (!(selection == 1 || selection == 2))
                        {
                            ConsoleHelper.WriteWithColor("Invalid Selection Option....", ConsoleColor.Red);
                            goto SelectionDescription;
                        }

                        ////////////////////////////////////////////////////////////////

                        if (selection == 1)
                        {
                            var commonPrice = saleDrug.Price * saleDrug.Count;
                            ConsoleHelper.WriteWithColor($"All Selected Drugs Have Been Sold. Common Price Is {commonPrice}", ConsoleColor.Green);

                            saleDrug.Count = 0; // to set the count of drugs to zero, because all of them were sold
                        }
                        else if (selection == 2)
                        {
                            goto SaleDesc;
                        }
                    }

                    ////////////////////////////////////////////////////////////////

                    if (quantity == saleDrug.Count)
                    {
                        saleDrug.Count = 0;
                        var commonPrice = saleDrug.Price * quantity;
                        ConsoleHelper.WriteWithColor($"All Drugs Have Been Sold. Common Price Is {commonPrice}", ConsoleColor.Green);
                    }

                    ////////////////////////////////////////////////////////////////
                }
                ConsoleHelper.WriteWithColor("All Drugs Have Been Sold :(", ConsoleColor.Red);
            }
            ConsoleHelper.WriteWithColor("We Can't Find Any Drug To Sell....", ConsoleColor.Red);
        }
    }
}

