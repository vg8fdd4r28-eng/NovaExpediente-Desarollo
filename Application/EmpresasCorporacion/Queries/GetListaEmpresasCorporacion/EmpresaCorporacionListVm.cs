using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.EmpresasCorporacion.Queries.GetListaEmpresasCorporacion
{
    public class EmpresaCorporacionListVm
    {
        public IList<EmpresaCorporacionDto> EmpresaCorporacions { get; set; }

        public int Count { get; set; }
    }
}
