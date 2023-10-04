namespace RacingCalendar
{
    class Program
    {
        static void Main(string[] args)
        {
            //creating racing calendar 
            RacingCalendar calendar = new RacingCalendar();
            //Adding races to the calendar 
            calendar.AddRace("Race 1", new DateTime(2003, 10, 25), "Track 1");
            calendar.AddRace("Race 2", new DateTime(2003, 10, 27), "Track 2");
            calendar.AddRace("Race 1", new DateTime(2003, 10, 30), "Track 3");

            calendar.PrintRaces();

            //intializing races 
            Race race1 = new Race { Name = "Race 1", Date = DateTime.Parse("2003-10-25"), TrackName = "Track A" };
            Race race2 = new Race { Name = "Race 2", Date = DateTime.Parse("2003-10-27"), TrackName = "Track B" };
            Race race3 = new Race { Name = "Race 3", Date = DateTime.Parse("2003-10-30"), TrackName = "Track C" };

            //initializing drivers 
            Driver driver1 = new Driver("Jon");
            Driver driver2 = new Driver("Patrick");
            Driver driver3 = new Driver("Jean");

            
            
            

            //race1.AddDriverToWaitingList(driver1);
            //race2.AddDriverToWaitingList(driver2);
            //race3.AddDriverToWaitingList(driver3);

            race1.AddDriver(driver1);
            race2.AddDriver(driver2);
            race3.AddDriver(driver3);
            Console.WriteLine("\n");
            race1.DisplayDriverDetails();

         
        }
    }
}
