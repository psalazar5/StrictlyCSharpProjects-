using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleBasedAccountingSystem
{
     class CheckingAccount: Account
    {
        public CheckingAccount(int accountID, decimal startingBalance): base(accountID, startingBalance) //3 & 4
        {
            
        }
        public override decimal calculateInterest(decimal interestRate)
        {
            return base.calculateInterest(interestRate);
        }
    }
}
