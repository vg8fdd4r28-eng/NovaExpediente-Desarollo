using System;
using NovaExpediente.Common;

namespace NovaExpediente.Application
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;

        public int CurrentYear => DateTime.Now.Year;

        public string Seniority(DateTime fromDate, DateTime toDate)
        {
            try
            {

                int[] monthDay = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                int years, months, days, increment = 0; 

                if (fromDate.Day > toDate.Day)
                {
                    increment = monthDay[fromDate.Month - 1];
                }

                if (increment == -1)
                {
                    if (DateTime.IsLeapYear(fromDate.Year))
                    {
                        increment = 29;
                    }
                    else
                    {
                        increment = 28;
                    }
                }

                if (increment != 0)
                {
                    days = (toDate.Day + increment) - fromDate.Day;
                    increment = 1;
                }
                else
                {
                    days = toDate.Day - fromDate.Day;
                }

                if ((fromDate.Month + increment) > toDate.Month)
                {
                    months = (toDate.Month + 12) - (fromDate.Month + increment);
                    increment = 1;
                }
                else
                {
                    months = (toDate.Month) - (fromDate.Month + increment);
                    increment = 0;
                }

                years = toDate.Year - (fromDate.Year + increment);

                return string.Concat(years > 0 ? years.ToString() + " años " : string.Empty,
                                 months > 0 ? months.ToString() + " meses " : string.Empty,
                                 days > 0 ? days.ToString() + " días " : string.Empty);

            }
            catch (Exception)
            {

                return "";
            }
        }

        public int GetNumberOfWorkingDays(DateTime start, DateTime stop)
        {
            var days = (stop - start).Days + 1;
            return workDaysInFullWeeks(days) + workDaysInPartialWeek(start.DayOfWeek, days);
        }

        private int workDaysInFullWeeks(int totalDays)
        {
            return (totalDays / 7) * 5;
        }

        private int workDaysInPartialWeek(DayOfWeek firstDay, int totalDays)
        {
            var remainingDays = totalDays % 7;
            var daysToSaturday = (int)DayOfWeek.Saturday - (int)firstDay;
            if (remainingDays <= daysToSaturday)
                return remainingDays;
            /* daysToSaturday are the days before the weekend,
             * the rest of the expression computes the days remaining after we
             * ignore Saturday and Sunday
             */
            // Range ends in a Saturday or in a Sunday
            if (remainingDays <= daysToSaturday + 2)
                return daysToSaturday;
            // Range ends after a Sunday
            else
                return remainingDays - 2;
        }
    }
}
