using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetColaboradoresPorPerfil
{
    public class ColaboradoresPorPerfilVm
    {
        public IList<ColaboradorPerfilDto> Colaboradores { get; set; }
        public int Count { get; set; }
    }
}
