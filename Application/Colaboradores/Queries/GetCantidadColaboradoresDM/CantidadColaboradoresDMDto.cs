using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;


namespace NovaExpediente.Application.Colaboradores.Queries.GetCantidadColaboradoresDM
{
    public class CantidadColaboradoresDMDto : IMapFrom<Colaborador>
    {

        public int? IdDeliveryManager { get; set; }
        public int? IdColaborador { get; set; }
        public string Nombre { get; set; }
        public int Cantidad { get; set; }
        public int Porcentaje { get; set; }
        public string FechaIngreso { get; set; }
        public List<int> ActualizacionExpediente { get; set; }
        public List<int> ActualizacionCurriculum { get; set; }
    }
}
