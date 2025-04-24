using MediatR;
using System;
using System.Collections.Generic;

namespace NovaExpediente.Application.MasterData.Queries.GetVacacionesAPP
{ 
    public class GetVacacionesAPPQuery : IRequest<VacacionesAPPVm>
    {
        public int IdColaborador { get; set; }
        public List<string> Roles { get; set; }
    }
}
