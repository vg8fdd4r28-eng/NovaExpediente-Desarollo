using System;

namespace NovaExpediente.Common
{
    public interface IDateTime
    {
        DateTime Now { get; }
        string Seniority(DateTime fromDate, DateTime toDate);

        int GetNumberOfWorkingDays(DateTime start, DateTime stop);
    }
}
