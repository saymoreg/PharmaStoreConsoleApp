using System;
using System.Text;
using Core.Constants;
using Core.Helpers;
using Data;
using Presentation.Services;

namespace Presentation
{
    public static class Program
    {
        readonly static AdminService _adminService;
        readonly static DruggistService _druggistService;
        readonly static DrugService _drugService;
        readonly static DrugstoreService _drugstoreService;
        readonly static OwnerService _ownerService;

        static Program()
        {
            Console.OutputEncoding = Encoding.UTF8;
            DbInitializer.SeedAdmins();
            _adminService = new AdminService();
            _druggistService = new DruggistService();
            _drugService = new DrugService();
            _drugstoreService = new DrugstoreService();
            _ownerService = new OwnerService();

        }
        static void Main()
        {

        AuthorizeDesc: var admin = _adminService.Authorize();
            if (admin is not null)
            {
                ConsoleHelper.WriteWithColor($"--- Welcome, {admin.Username} ---", ConsoleColor.DarkCyan);
                while (true)
                {
                MainMenuDesc: ConsoleHelper.WriteWithColor("(1) - Owners", ConsoleColor.DarkYellow);
                    ConsoleHelper.WriteWithColor("(2) - Drugstores", ConsoleColor.DarkYellow);
                    ConsoleHelper.WriteWithColor("(3) - Druggists", ConsoleColor.DarkYellow);
                    ConsoleHelper.WriteWithColor("(4) - Drugs", ConsoleColor.DarkYellow);
                    ConsoleHelper.WriteWithColor("(0) - Exit", ConsoleColor.DarkYellow);

                    int number;

                    bool isSucceeded = int.TryParse(Console.ReadLine(), out number);
                    if (!isSucceeded)
                    {
                        ConsoleHelper.WriteWithColor("Invalid Input :(", ConsoleColor.Red);
                        goto MainMenuDesc;
                    }
                    else
                    {
                        switch (number)
                        {
                            case (int)MainMenuOptions.Owners:
                                while (true)
                                {
                                OwnerMenuDesc: ConsoleHelper.WriteWithColor("(1) - Create Owner", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(2) - Update Owner", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(3) - Delete Owner", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(4) - Get All Owners", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(0) - Back To Main Menu", ConsoleColor.DarkYellow);

                                    isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor("Invalid Input :(", ConsoleColor.Red);
                                    }
                                    else
                                    {
                                        switch (number)
                                        {
                                            case (int)OwnerOptions.CreateOwner:
                                                _ownerService.Create();
                                                break;
                                            case (int)OwnerOptions.UpdateOwner:
                                                _ownerService.Update();
                                                break;
                                            case (int)OwnerOptions.DeleteOwner:
                                                _ownerService.Delete();
                                                break;
                                            case (int)OwnerOptions.GetAllOwners:
                                                _ownerService.GetAll();
                                                break;
                                            case (int)OwnerOptions.BackToMainMenu:
                                                goto MainMenuDesc;
                                            default:
                                                ConsoleHelper.WriteWithColor("Invalid Input :(", ConsoleColor.Red);
                                                goto OwnerMenuDesc;
                                        }
                                    }
                                }
                            case (int)MainMenuOptions.Drugstores:
                                while (true)
                                {
                                DrugStoreMenuDesc: ConsoleHelper.WriteWithColor("(1) - Create Drugstore", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(2) - Update Drugstore", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(3) - Delete Drugstore", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(4) - Get All Drugstores", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(5) - Get All Drugstores By Owner", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(6) - Sales", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(0) - Back To Main Menu", ConsoleColor.DarkYellow);

                                    isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor("Invalid Input :(", ConsoleColor.Red);
                                    }
                                    else
                                    {
                                        switch (number)
                                        {
                                            case (int)DrugstoreOptions.CreateDrugstore:
                                                _drugstoreService.Create();
                                                break;
                                            case (int)DrugstoreOptions.UpdateDrugstore:
                                                _drugstoreService.Update();
                                                break;
                                            case (int)DrugstoreOptions.DeleteDrugstore:
                                                _drugstoreService.Delete();
                                                break;
                                            case (int)DrugstoreOptions.GetAllDrugstores:
                                                _drugstoreService.GetAll();
                                                break;
                                            case (int)DrugstoreOptions.GetAllDrugstoresByOwner:
                                                _drugstoreService.GetAllDrugstoresByOwner();
                                                break;
                                            case (int)DrugstoreOptions.Sales:
                                                _drugstoreService.Sale();
                                                break;
                                            case (int)DrugstoreOptions.BackToMainMenu:
                                                goto MainMenuDesc;
                                            default:
                                                ConsoleHelper.WriteWithColor("Invalid Input :(", ConsoleColor.Red);
                                                goto DrugStoreMenuDesc;
                                        }
                                    }
                                }
                            case (int)MainMenuOptions.Druggists:
                                while (true)
                                {
                                DruggistDesc: ConsoleHelper.WriteWithColor("(1) - Create Druggist", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(2) - Update Druggist", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(3) - Delete Druggist", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(4) - Get All Druggist", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(5) - Get All Druggist By Drugstore", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(0) - Back To Main Menu", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("--- Select Option ---", ConsoleColor.DarkCyan);

                                    isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor("Invalid Input :(", ConsoleColor.Red);
                                    }
                                    else
                                    {
                                        switch (number)
                                        {
                                            case (int)DruggistOption.CreateDruggist:
                                                _druggistService.Create();
                                                break;
                                            case (int)DruggistOption.UpdateDruggist:
                                                _druggistService.Update();
                                                break;
                                            case (int)DruggistOption.DeleteDruggist:
                                                _druggistService.Delete();
                                                break;
                                            case (int)DruggistOption.GetAllDruggist:
                                                _druggistService.GetAll();
                                                break;
                                            case (int)DruggistOption.GetAllDruggistByDrugstore:
                                                _druggistService.GetAllDruggistByDrugstore();
                                                break;
                                            case (int)DruggistOption.BackToMainMenu:
                                                goto MainMenuDesc;
                                            default:
                                                ConsoleHelper.WriteWithColor("Invalid Input :(", ConsoleColor.Red);
                                                goto DruggistDesc;
                                        }
                                    }
                                }
                            case (int)MainMenuOptions.Drugs:
                                while (true)
                                {
                                DrugDesc: ConsoleHelper.WriteWithColor("(1) - Create Drug", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(2) - Update Drug", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(3) - Delete Drug", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(4) - Get All Drugs", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(5) - Get Drug By Drugstore", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(6) - Filter Drugs By Price", ConsoleColor.DarkYellow);
                                    ConsoleHelper.WriteWithColor("(0) - Back To Main Menu", ConsoleColor.DarkYellow);

                                    isSucceeded = int.TryParse(Console.ReadLine(), out number);
                                    if (!isSucceeded)
                                    {
                                        ConsoleHelper.WriteWithColor("Invalid Input :(", ConsoleColor.Red);
                                    }
                                    else
                                    {
                                        switch (number)
                                        {
                                            case (int)DrugOption.CreateDrug:
                                                _drugService.Create();
                                                break;
                                            case (int)DrugOption.UpdateDrug:
                                                _drugService.Update();
                                                break;
                                            case (int)DrugOption.DeleteDrug:
                                                _drugService.Delete();
                                                break;
                                            case (int)DrugOption.GetAllDrugs:
                                                _drugService.GetAll();
                                                break;
                                            case (int)DrugOption.GetAllDrugsByDrugstore:
                                                _drugService.GetAllDrugsByDrugstore();
                                                break;
                                            case (int)DrugOption.FilterDrugs:
                                                _drugService.Filter();
                                                break;
                                            case (int)DrugOption.BackToMainMenu:
                                                goto MainMenuDesc;
                                            default:
                                                ConsoleHelper.WriteWithColor("Invalid Input :(", ConsoleColor.Red);
                                                goto DrugDesc;
                                        }
                                    }
                                }
                            case (int)MainMenuOptions.Logout:
                                return;
                            default:
                                ConsoleHelper.WriteWithColor("Invalid Input :(", ConsoleColor.Red);
                                goto MainMenuDesc;
                        }
                    }
                }
            }
        }
    }
}