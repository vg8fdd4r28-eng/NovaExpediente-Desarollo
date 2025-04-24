using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Certificaciones.Queries.GetCantidadCertificaciones
{
    public class CantidadCertificacionesVm
    {
        public IList<CantidadCertificacionesDto> CERTIFICACIONCOLABORADOR { get; set; } 

        public string CANTIDAD { get; set; }
    }
}
