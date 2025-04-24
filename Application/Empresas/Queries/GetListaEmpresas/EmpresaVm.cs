using System.Collections.Generic;

namespace NovaExpediente.Application.Empresas.Queries.GetListaEmpresas
{
    public class EmpresaVm
    {
        public IList<EmpresaDto> Empresas { get; set; }

        public int Count { get; set; }
    }
}
