
using MediatR;
using System;

namespace NovaExpediente.Application.VacunasColaboradores.Queries.GetListaVacunasColaborador
{
    public class GetVacunasColaboradorListQuery : IRequest<VacunasColaboradorListVm>
    {
        public int IdColaborador { get; set; }

    }
}