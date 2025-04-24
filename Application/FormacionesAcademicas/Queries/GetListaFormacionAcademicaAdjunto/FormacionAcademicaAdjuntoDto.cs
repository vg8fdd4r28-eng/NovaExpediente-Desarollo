using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.FormacionesAcademicas.Queries.GetListaFormacionAcademicaAdjunto
{
    public class FormacionAcademicaAdjuntoDto : IMapFrom<FormacionAcademica>
    {

        public int IDFORMACIONACADEMICA { get; set; }
        public int IDCENTROESTUDIO { get; set; }
        public int IDGRADOACADEMICO { get; set; }
        public int IDCOLABORADOR { get; set; }
        public int IDUSUARIOCREA { get; set; }
        public int IDUSUARIOMODIFICA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public int ANIO { get; set; }
        public string CERTIFICADO { get; set; }
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }


        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormacionAcademica, FormacionAcademicaAdjuntoDto>()
                .ForMember(d => d.IDFORMACIONACADEMICA, opt => opt.MapFrom(s => s.IDFORMACIONACADEMICA))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                .ForMember(d => d.CERTIFICADO, opt => opt.MapFrom(s => s.CERTIFICADO != null ? $"{s.MIMETYPE}{Convert.ToBase64String(s.CERTIFICADO)}" : null));
        }


    }
}
