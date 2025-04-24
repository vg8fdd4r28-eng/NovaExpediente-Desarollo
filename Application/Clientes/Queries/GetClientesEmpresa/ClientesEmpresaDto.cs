using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Clientes.Queries.GetClientesEmpresa
{
    public class ClientesEmpresaDto : IMapFrom<Cliente>
    {
        public int IdCliente { get; set; }
        public string Nombre { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Cliente, ClientesEmpresaDto>()
               .ForMember(d => d.IdCliente, opt => opt.MapFrom(s => s.IDCLIENTE))
               .ForMember(d => d.Nombre, opt => opt.MapFrom(s => s.NOMBRE));
        }
    }
}
