using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RacingCalendar.Models
{
    public class Driver
    {
        public string Name { get; set; }
        public Driver(string name)
        {
            Name = name;
        }
    }
}
