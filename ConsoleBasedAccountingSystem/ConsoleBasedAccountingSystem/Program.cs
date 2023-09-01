using System;

namespace ConsoleBasedAccountingSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Account System 1.0"); //1. 

            CheckingAccount checkingAccount = new CheckingAccount(100, 0);
            PremiumAccount premiumAccount = new PremiumAccount(200, 0);

            //CheckingAccount checkingAccount = null;
            //PremiumAccount premiumAccount = null;

            while (true)
            {
                Console.WriteLine("Select an option: ");
                Console.WriteLine("1. Open Checking Account");
                Console.WriteLine("2. Open Premium Account");
                Console.WriteLine("3. Deposit Money");
                Console.WriteLine("4. Transfer Money");
                Console.WriteLine("5. Calculate Interest");
                Console.WriteLine("6. Print Account Details");
                Console.WriteLine("7. Exit");

                int choice; 
                if(!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 7)
                {
                    Console.WriteLine("Invalid choice. Please try again.");
                    continue;
                }

                switch (choice)
                {
                    case 1:
                        AccountManager.OpenCheckingAccount(ref checkingAccount);
                        break;
                    case 2:
                        AccountManager.OpenPremiumAccount(ref premiumAccount);
                        break;
                    case 3:
                        AccountManager.depositMoney(checkingAccount, premiumAccount);
                        break;
                    case 4:
                        AccountManager.TransferMoney(checkingAccount, premiumAccount);
                        break;
                    case 5:
                        AccountManager.CalculateAndPrintInterest(checkingAccount, premiumAccount);
                        break;
                    case 6:
                        AccountManager.PrintAccountDetails(checkingAccount, premiumAccount);
                        break;
                    case 7:
                        Console.WriteLine("Thank you for using Accounting System 1.0, Goodbye! ");
                        Environment.Exit(0);
                        break;
                }
            }
        }

        static void ExitApplication()
        {
        Console.WriteLine("Thank you for using Accounting System 1.0 , goodbye!");
        Environment.Exit(0);
        }

        static void PrintBalances(Account account1, Account account2)
        {
            Console.WriteLine($"Checking Account (ID: {account1.AccountID}): ${account1.Balance:F2}");
            Console.WriteLine($"Premium Account (ID: {account2.AccountID}): ${account2.Balance:F2}");
            Console.ReadLine();
        }    
    }
}
