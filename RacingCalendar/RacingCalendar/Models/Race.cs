using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingCalendar
{
    public class Race
    { //name of the race, date of race and name of track 
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public string TrackName { get; set; }
        public Driver driver { get; set; }

        //Collection to store drivers on waiting list (1)
        private Queue<Driver> waitingList; 


        //Keeping track of drivers inside the race: (2)
        private List<Driver> drivers = new List<Driver>();


        //(1)
        public Race()
        {
            waitingList = new Queue<Driver>();
        }

        public void AddDriverToWaitingList(Driver driver)
        {
            waitingList.Enqueue(driver);
            Console.WriteLine($"{driver.Name} added to the waiting list for {TrackName}. ");
        }


        //(2)
        //Method to add driver to the race 
        public void AddDriver(Driver driver)
        {
            if (drivers.Count < 20)
            {
                drivers.Add(driver);
                Console.WriteLine($"Driver: '{driver.Name}' successfully added to the race!");
            }
            else { Console.WriteLine("Cannot add more drivers. Starting grid is full."); }
        }


        public Race(string name, DateTime date, string trackName)
        {
            Name = name;
            Date = date;
            TrackName = trackName;
        }



        public void DisplayDriverDetails()
        {
            Console.WriteLine($"Race Details : Race Name - {Name}, Date: {Date.ToShortDateString()}, Track: {TrackName}");
            int raceNumber = 1; 
            foreach(var driver in drivers)
            {
                Console.WriteLine($"Race {raceNumber} Driver Details: " );
                Console.WriteLine($"    Driver Name : {driver.Name}");
                Console.WriteLine($"    Race Time: {Date.ToShortDateString()}");
                Console.WriteLine($"    Track Name: {TrackName}");
                Console.WriteLine();
                raceNumber++;
            }

        }



        //override method to include racing info
        public override string ToString()
        {
            //string driversInfo = string.Join(", ", drivers.Select(driver => driver.Name));
            return $"Race: {Name}, Date: {Date.ToShortDateString()}, Track: {TrackName}, Drivers: {driver.Name}";
        }
    }
}
