using System;
using System.Collections.Generic;
using System.Linq;

public class cardHolder
{
    string cardNum;
    int pin;
    string firstName;
    string lastName;
    double balance;

    public cardHolder(string cardNum, int pin, string firstName, string lastName, double balance) //passed the variables into the constructor and instantiated them as new objects
    {
        this.cardNum = cardNum;
        this.pin = pin;
        this.firstName = firstName;
        this.lastName = lastName;
        this.balance = balance;
    }

    public string getNum()
    {
        return cardNum;
    }

    public int getPin()
    {
        return pin;
    }

    public string getFirstName()
    {
        return firstName;
    }

    public double getBalance()
    {
        return balance;
    }

    public void setNum(string newCardNum)
    {
        cardNum = newCardNum;
    }

    public void setPin(int newPin)
    {
        pin = newPin;
    }

    public void setFirstName(string newFirstName)
    {
        firstName = newFirstName;
    }
    
    public void setLastName(string newLastName)
    {
        lastName = newLastName;
    }

    public void setBalance(double newBalance)
    {
        balance = newBalance;
    }

    public static void Main(string[] args)
    {
        void printOptions() //running the actual atm menu options 
        {
            Console.WriteLine("Please choose from one of the following options: ");
            Console.WriteLine("1. Deposit");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. Show Balance");
            Console.WriteLine("4. Exit");
            
        }

        //handling deposits from users 
        void deposit(cardHolder currentUser)//object - currentuser 
        {
            Console.WriteLine("How much $$ would you like to deposit ? ");
            double deposit = double .Parse(Console.ReadLine()); //could cause error , use try / catch loop
            currentUser.setBalance(currentUser.getBalance() + deposit);
            Console.WriteLine("Thank you for your $$. Your new balance is: " + currentUser.getBalance());
        }

        void withdraw(cardHolder currentUser)
        {
            Console.WriteLine("How much $$ would you like to withdraw: ");
            double withdrawl = double .Parse(Console.ReadLine());
            //check if the user has enough money 
            if(currentUser.getBalance() < withdrawl)
            {
                Console.WriteLine("Insufficient balance!");
            }
            else
            {
                currentUser.setBalance(currentUser.getBalance() - withdrawl);
                Console.WriteLine("You're good to go! Thank you.");
            }
        }

        //return the balance of the user 
        void balance(cardHolder currentUser)
        {
            Console.WriteLine("Current Balance: " + currentUser.getBalance());
        }

        //list of cardholders - specify the type of object this list is going to contain : cardHolder , list of fake information 
        List<cardHolder> cardHolders = new List<cardHolder>();
        cardHolders.Add(new cardHolder("4534387439282984", 1234, "Pablo", "Salazar", 250.23));
        cardHolders.Add(new cardHolder("4728284629342816", 3719, "Andrew", "King", 190.23));
        cardHolders.Add(new cardHolder("2974204829432919", 9472, "Elliot", "Link", 298.23));
        cardHolders.Add(new cardHolder("2972937302942938", 3948, "Isabelle", "Smith", 483.24));
        cardHolders.Add(new cardHolder("3945038284572097", 2479, "Angela", "Curry", 894.93));

        //Prompt User
        Console.WriteLine("Welcome to SimpleATM");
        Console.WriteLine("Please inser your debit card: ");
        string debitCardNum = "";
        cardHolder currentUser;

        while (true)
        {
            try 
            {
                debitCardNum = Console.ReadLine();
                //check against temp DB -above
                currentUser = cardHolders.FirstOrDefault(a => a.cardNum == debitCardNum);

                if(currentUser != null)
                {
                    break;
                }
                else 
                { 
                    Console.WriteLine("Card is not recognized. Please try again."); 
                }
            }
            catch
            {
                Console.WriteLine("Card is not recognized. Please try again.");
            }
        }
        Console.WriteLine("Please enter your pin: ");
        int userPin = 0; //initialization of the variable , the initial value of 0 is overwritten with the user's input if the input can be successfully parsed as an integer.
        while (true)
        {
            try
            {
                userPin = int.Parse(Console.ReadLine());
                //check against temp DB -above

                if (currentUser.getPin() == userPin)
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Incorrect pin. Please try again.");
                }
            }
            catch
            {
                Console.WriteLine("Incorrect pin. Please try again.");
            }
        }

        Console.WriteLine("Welcome " + currentUser.getFirstName());
        int option = 0;
        do
        {
            printOptions();
            try
            {option = int.Parse(Console.ReadLine());}

            catch 
            { }

            if(option == 1)
            { deposit(currentUser);}

            else if(option == 2)
            {withdraw(currentUser);}

            else if(option == 3)
            { balance(currentUser);}

            else if (option == 4)
            { break;}

            else
            { option = 0;}
        }
        while (option != 4);
        Console.WriteLine("Thank you have a nice day!");

    }


}
