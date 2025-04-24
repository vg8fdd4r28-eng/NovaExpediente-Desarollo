using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.ExperienciasProyectos.Queries.GetProyectoClienteValido
{
    public class GetProyectoClienteValidoQuery : IRequest<ProyectoClienteValidoVm>
    {
        public int IdColaborador { get; set; }
        public string NombreCliente { get; set; }
        public string NombreProyecto { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }
    }
}
