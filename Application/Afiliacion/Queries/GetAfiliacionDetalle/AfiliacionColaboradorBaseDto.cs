using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Common;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Afiliacion.Queries.GetAfiliacionDetalle
{
    public class AfiliacionColaboradorBaseDto : IMapFrom<AfiliacionColaborador>
    {
        public int IdAfiliacionColaborador{ get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public bool TieneCertificado { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AfiliacionColaborador, AfiliacionColaboradorBaseDto>()
                .ForMember(d => d.TieneCertificado, opt => opt.MapFrom(s => s.CERTIFICADO != null ));
        }


    }
}
