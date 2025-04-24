using NovaExpediente.Application.Afiliacion.Queries.GetAfiliacionDetalle;
using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.Afiliacion.Queries.GetListaAfiliacion
{
    public class AfiliacionListVm
    {
        public IList<AfiliacionColaboradorBaseDto> Afiliaciones { get; set; }
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
