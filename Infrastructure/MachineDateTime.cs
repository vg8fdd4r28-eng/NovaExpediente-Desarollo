using System;
using NovaExpediente.Common;

namespace NovaExpediente.Infrastructure
{
    public class MachineDateTime : IDateTime
    {
        public DateTime Now => DateTime.Now;

        public int CurrentYear => DateTime.Now.Year;

        public string Seniority (DateTime fromDate, DateTime toDate)
        {
            DateTime zeroTime = new DateTime(1, 1, 1);
            int years, months, days = 0;
            TimeSpan timeSpan;

            timeSpan = toDate - fromDate;
            years = (zeroTime + timeSpan).Year - 1;

            fromDate = fromDate.AddYears(years);
            timeSpan = toDate - fromDate;
            months = (zeroTime + timeSpan).Month;

            if (fromDate.AddMonths(months) > toDate)
            {
                months--;
            }

            fromDate = fromDate.AddMonths(months);
            timeSpan = toDate - fromDate;
            days = (zeroTime + timeSpan).Day;

            return string.Concat(years > 0 ? years.ToString() + " años " : string.Empty,
                                 months > 0 ? months.ToString() + " meses " : string.Empty,
                                 days > 0 ? days.ToString() + " días " : string.Empty);
        }

        public int GetNumberOfWorkingDays(DateTime start, DateTime stop)
        {
            var days = (stop - start).Days + 1;
            return workDaysInFullWeeks(days) + workDaysInPartialWeek(start.DayOfWeek, days);
        }

        private  int workDaysInFullWeeks(int totalDays)
        {
            return (totalDays / 7) * 5;
        }

        private  int workDaysInPartialWeek(DayOfWeek firstDay, int totalDays)
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
