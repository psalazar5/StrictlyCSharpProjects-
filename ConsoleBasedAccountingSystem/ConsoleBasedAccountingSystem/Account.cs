using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleBasedAccountingSystem
{ //2
     class Account
    {
        public int AccountID { get; set; }
        public decimal Balance { get; set; }

        public Account(int accountID, decimal startingBalance) //3
        {
            AccountID = accountID;
            Balance = startingBalance;
        }
        
        public void Deposit(decimal amount)
        {
            Balance += amount; 
        }

        public virtual decimal calculateInterest(decimal interestRate)
        {
            return Balance * (interestRate / 100); 
        }

        public static void Transfer(Account sourceAccount, Account targetAccount, decimal amount)
        { //9. You should be able to transfer money from one account to another at any time.
            if (sourceAccount == null || targetAccount == null)
            {
                Console.WriteLine("Please open both Checking and Premium account first.");
                return;
            }
            if(sourceAccount.Balance >= amount)
            {
                sourceAccount.Balance -= amount;
                targetAccount.Balance += amount;
                Console.WriteLine($"Successfully transferred ${amount:F2} from account {sourceAccount.AccountID} to Account {targetAccount.AccountID}.");
            }
            else { Console.WriteLine("Insufficient funds for the transfer."); }
        }
    }
}
