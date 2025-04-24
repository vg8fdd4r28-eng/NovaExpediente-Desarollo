using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;

namespace NovaExpediente.Application.FormacionesAcademicas.Queries.GetListaFormacionAcademica
{
    public class FormacionAcademicaDto : IMapFrom<FormacionAcademica>
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
        public DateTime FECHACREACION { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
        public string GradoAcademico { get; set; }
        public string CentroEstudio { get; set; }

        public bool TieneCertificado { get; set; }
        public void Mapping(Profile profile)
        {
            profile.CreateMap<FormacionAcademica, FormacionAcademicaDto>()
                .ForMember(d => d.IDFORMACIONACADEMICA, opt => opt.MapFrom(s => s.IDFORMACIONACADEMICA))
                .ForMember(d => d.NOMBRE, opt => opt.MapFrom(s => s.NOMBRE))
                 .ForMember(d => d.TieneCertificado, opt => opt.MapFrom(s => s.CERTIFICADO != null))
                .ForMember(d => d.GradoAcademico, opt => opt.MapFrom(s => s.GradoAcademico.Nombre))
                .ForMember(d => d.CentroEstudio, opt => opt.MapFrom(s => s.CentroEstudio.Nombre));
        }


    }
}
