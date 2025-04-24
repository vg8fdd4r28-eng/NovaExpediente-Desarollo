using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Common.Interfaces
{
    public interface IFireForgetCommandHandler
    {
            void Execute(Func<INovaExpedienteDbContext, Task> databaseWork);
    }
}
