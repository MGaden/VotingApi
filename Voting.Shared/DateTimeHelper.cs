using System;
using System.Collections.Generic;
using System.Text;

namespace Voting.Shared
{
    public static class DateTimeHelper
    {
        public enum DateInterval
        {
            Years,
            Months,
            Weeks,
            Days
        }
        public static DateTime ComputeDateFromNow(int Nth, DateInterval interval)
        {
            switch (interval)
            {
                case DateInterval.Years:
                    return DateTime.Now.AddYears(Nth);
                case DateInterval.Months:
                    return DateTime.Now.AddMonths(Nth);
                case DateInterval.Weeks:
                    return DateTime.Now.AddDays(Nth * 7);
                case DateInterval.Days:
                    return DateTime.Now.AddDays(Nth);
            }
            return DateTime.Now;
        }

        public static DateTime GetLastSunday(DateTime date)
        {
            var lastSunday = date;
            while (lastSunday.DayOfWeek != DayOfWeek.Sunday)
                lastSunday = lastSunday.AddDays(-1);
            return new DateTime(lastSunday.Year, lastSunday.Month, lastSunday.Day, 0, 0, 0);
        }
    }
}
