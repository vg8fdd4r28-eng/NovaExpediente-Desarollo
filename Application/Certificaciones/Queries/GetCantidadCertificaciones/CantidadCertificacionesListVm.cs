using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Certificaciones.Queries.GetCantidadCertificaciones
{
    public class CantidadCertificacionesListVm
    {
        public IList<CantidadCertificacionesDto> CERTIFICACIONCOLABORADOR { get; set; }

        
        public int CANTIDAD { get; set; }

    }
}
