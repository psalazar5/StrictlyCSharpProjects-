using RacingCalendar.Models;
using System.Diagnostics;

namespace RacingCalendar.Extensions;


public static class RacingCalendarExtension
{
    public static string FormatRacingDetails(this RacingCalendar calendar)
    {
        return $"Race: {calendar.Name}, Date: {calendar.Date.ToShortDateString()}, Track: {calendar.TrackName}, Drivers: {calendar.driver.Name}";
    }
}
