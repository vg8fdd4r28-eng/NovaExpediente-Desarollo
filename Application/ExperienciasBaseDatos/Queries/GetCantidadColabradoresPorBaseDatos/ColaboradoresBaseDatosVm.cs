using System;
using System.Collections.Generic;
using System.Text;

namespace NovaExpediente.Application.ExperienciasBaseDatos.Queries.GetCantidadColabradoresPorBaseDatos
{
    public class ColaboradoresBaseDatosVm
    {

        public IList<ColaboradoresBaseDatosDto> Colaboradores { get; set; }    

        public int Cantidad { get; set; }
    }
}
