﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingCalendar
{
    public class RacingCalendar
    {
        //Use List<Race> to store races 
        public List<Race> Races { get; private set; } //collection to store races 
        public RacingCalendar()
        {
            Races = new List<Race>(); //initializing the list of races
        }
        //Method to add a race to the calendar 
        public void AddRace(string name, DateTime date, string trackName, Driver driver = null)
        {
            //Race race = new Race(name, date, trackName, driver);
            //Races.Add(race);
            Race race = new Race
            {
                Name = name,
                Date = date,
                TrackName = trackName,
                driver = driver
            };

            Races.Add(race);
        }
        //Method to print all races to the console 
        public void PrintRaces()
        {
            foreach(var race in Races)
            {
                Console.WriteLine(race.ToString());
                //race.DisplayDriverDetails(); // Call the method to display driver details for each race
            }
        }
    }
}
