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
                        OpenCheckingAccount(ref checkingAccount);
                        break;
                    case 2:
                        OpenPremiumAccount(ref premiumAccount);
                        break;
                    case 3:
                        depositMoney(checkingAccount, premiumAccount);
                        break;
                    case 4:
                        TransferMoney(checkingAccount,premiumAccount);
                        break;
                    case 5:
                        CalculateAndPrintInterest(checkingAccount, premiumAccount);
                        break;
                    case 6:
                        PrintAccountDetails(checkingAccount, premiumAccount);
                        break;
                    case 7:
                        Console.WriteLine("Thank you for using Accounting System 1.0, Goodbye! ");
                        Environment.Exit(0);
                        break;
                }

            }

        }

        static void OpenCheckingAccount(ref CheckingAccount checkingAccount) //4. You should be able to set a starting balance when creating a checking account.
        {
            if(checkingAccount != null)
            {
                Console.WriteLine("Checking Account is already open");
            }
            else 
            { 
                Console.WriteLine("Enter the starting balance for the Checking Account");
                decimal checkingStartingBalance;
                if(!decimal.TryParse(Console.ReadLine(), out checkingStartingBalance))
                {
                    Console.WriteLine("Invalid input. Please enter a valid decimal number");
                }
                else
                {
                    checkingAccount = new CheckingAccount(100, checkingStartingBalance);
                    Console.WriteLine("Checking Account opened successfully!");
                }
            }
        }

        static void OpenPremiumAccount(ref PremiumAccount premiumAccount)
        {
            if(premiumAccount != null)
            {
                Console.WriteLine("Premium Account is already open.");
            }
            else
            {
                Console.WriteLine("Enter the starting balace for the Premium Account");
                decimal premiumStartingBalance; 
                if(!decimal.TryParse(Console.ReadLine(), out premiumStartingBalance))
                {
                    Console.WriteLine("Invalid input. Please enter a valid decimal number");
                }
                else
                {
                    premiumAccount = new PremiumAccount(200, premiumStartingBalance);
                    Console.WriteLine("Premium Account opened successfully!");
                }
            }
        }

        static void depositMoney(CheckingAccount checkingAccount, PremiumAccount premiumAccount) //5.You should be able to deposit money (decimal number) at any time after creating a checking account. 
        {
            if (checkingAccount == null && premiumAccount == null)
            {
                Console.WriteLine("Please open an account first.");
                return;
            }

            Console.WriteLine("Enter the account ID to deposit money (100 for checking or 200 for Premium)");

            int accountID;

            if (!int.TryParse(Console.ReadLine(), out accountID) || (accountID != 100 && accountID != 200))
            {
                Console.WriteLine("Invalid Account ID. Please enter 100 for Checking or 200 for Premium ");
                return;
            }

            //Account selectedAccount = accountID == 100 ? checkingAccount : premiumAccount;
            Account selectedAccount;
            if (accountID == 100)
            {
                selectedAccount = checkingAccount;
            }
            else
            {
                selectedAccount = premiumAccount;
            }

            Console.WriteLine("Enter the amount to deposit:");
            decimal amount; 

            if(!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("Invalid input. Please enter a valid decimal number. ");
                return;
            }

            selectedAccount.Deposit(amount);
            Console.WriteLine($"Successfully deposited ${amount:F2} to the account with ID {selectedAccount.AccountID}.");
        }

        static void TransferMoney(CheckingAccount checkingAccount, PremiumAccount premiumAccount)
        {
            if(checkingAccount == null && premiumAccount == null)
            {
                Console.WriteLine("Please open both Checking and Premium accounts first");
                return;
            }

            Console.WriteLine("Enter the amount to transfer:");
            decimal amount;

            if(!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.WriteLine("Invalid input. Please enter a valid decimal number ");
                return;
            }

            Console.WriteLine("Enter the source account ID (100 for Checking, 200 for Premium)");
            int sourceAccountID;

            if(!int.TryParse(Console.ReadLine(),out sourceAccountID) || (sourceAccountID != 100 && sourceAccountID != 200))
            {
                Console.WriteLine("Invalid source account ID. Please enter 100 for Checking and 200 for Premium");
                return;
            }

            Console.WriteLine("Enter the target account ID (100 for Checking, 200 for Premium )");
            int targetAccountID;

            if (!int.TryParse(Console.ReadLine(), out targetAccountID) || (targetAccountID != 100 && targetAccountID != 200))
            {
                Console.WriteLine("Invalid target account ID. Please enter 100 for Checking or 200 for Premium");
                return;
            }

            Account sourceAccount = null;
            Account targetAccount = null;

            if (sourceAccountID == 100)
            {
                sourceAccount = checkingAccount;
            }
            else if(sourceAccountID == 200 )
            {
                sourceAccount = premiumAccount;
            }

            if(targetAccountID == 100)
            {
                targetAccount = checkingAccount;
            }
            else if(targetAccountID == 200)
            {
                targetAccount = premiumAccount;
            }


            Account.Transfer(sourceAccount, targetAccount, amount);
            
            Console.WriteLine($"Successfully transferred ${amount:F2} from Account {sourceAccount.AccountID} to account {targetAccount.AccountID}");
        }

        static void CalculateAndPrintInterest(CheckingAccount checkingAccount, PremiumAccount premiumAccount)
        { //6. You should be able to calculate interest based on the current balance of the checking account and an input.
            if (checkingAccount == null && premiumAccount == null)
            {
                Console.WriteLine("Please open up an account first.");
                return;
            }

            if(checkingAccount != null)
            {
                decimal checkingInterest = checkingAccount.calculateInterest(3);
                Console.WriteLine($"Premium Account interest: ${checkingInterest:F2}");
            }

            if(premiumAccount != null)
            {
                decimal premiumInterest = premiumAccount.calculateInterest(3);
                Console.WriteLine($"Checking Account interest: ${premiumInterest:F2}");
            }
        }

        static void PrintAccountDetails(CheckingAccount checkingAccount, PremiumAccount premiumAccount)
        { //7. You should be able to print the ID and the current balance of a checking account at any time.
            if (checkingAccount == null && premiumAccount == null)
            {
                Console.WriteLine("Please open up an account first.");
                return;
            }
            if(checkingAccount != null)
            {
                Console.WriteLine($"The account with ID '{checkingAccount.AccountID}' has a balance of : ${checkingAccount.Balance:F2}");
            }
            if(premiumAccount != null)
            {
                Console.WriteLine($"The account with ID '{premiumAccount.AccountID}' has a balance of : ${premiumAccount.Balance:F2}");
            }
        }

        static void PrintBalances(Account account1, Account account2)
        {
            Console.WriteLine($"Checking Account (ID: {account1.AccountID}): ${account1.Balance:F2}");
            Console.WriteLine($"Premium Account (ID: {account2.AccountID}): ${account2.Balance:F2}");
            Console.ReadLine();
        }    
    }
}
