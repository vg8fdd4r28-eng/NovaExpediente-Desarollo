using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Common;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.Afiliacion.Queries.GetAfiliacionDetalle
{
    public class AfiliacionColaboradorDto : IMapFrom<AfiliacionColaborador>
    {
        public int IdAfiliacionColaborador{ get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public DateTime FechaInicio { get; set; }
        public string Certificado { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<AfiliacionColaborador, AfiliacionColaboradorDto>()
                .ForMember(d => d.Certificado, opt => opt.MapFrom(s => s.CERTIFICADO != null ? $"{s.MIMETYPE}{Convert.ToBase64String(s.CERTIFICADO)}" : null));
        }


    }
}
