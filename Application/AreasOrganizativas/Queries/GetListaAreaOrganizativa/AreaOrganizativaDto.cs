using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.AreasOrganizativas.Queries.GetListaAreaOrganizativa
{
    public class AreaOrganizativaDto : IMapFrom<AreaOrganizativa>
    {
        public int IDAREAORGANIZATIVA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }


        public void Mapping(Profile profile)
        {


            profile.CreateMap<AreaOrganizativa, AreaOrganizativaDto>()
                .ForMember(d => d.IDAREAORGANIZATIVA, opt => opt.MapFrom(s => s.IDAREAORGANIZATIVA))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.DESCRIPCION, opt => opt.MapFrom(s => s.DESCRIPCION));
        }
    }
}
