using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleBasedAccountingSystem
{ //2 
    public class PremiumAccount: Account
    {
        public PremiumAccount(int accountID, decimal startingBalance): base(accountID, startingBalance)
        {
            
        }

        public override decimal calculateInterest(decimal interestRate)
        { //The premium account has the same behavior as the checking account. The only difference is that the premium account gets a 1% extra interest on top of the user input.
            return base.calculateInterest(interestRate) + (Balance * 0.01m);
        }
    }
}
