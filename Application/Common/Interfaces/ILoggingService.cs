using NovaExpediente.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Common.Interfaces
{
   
    public interface ILoggingService
    {
        void LogInformation(EnumLoggingCategory type, string data, int? id);
        void LogError(EnumLoggingCategory type, string data, int? id);
    }
}
