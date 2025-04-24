using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System.Linq;

namespace NovaExpediente.Application.VacunacionColaboradores.Queries.GetVacunacionColaboradorReporte
{
    public class VacunacionColaboradorReporteDto : IMapFrom<Colaborador>
    {


        public VacunacionColaboradorReporteDto()
        {
        }

        public int IdColaborador { get; set; }
        public string Identificacion { get; set; }
        public string NombreCompleto { get; set; }

        public int IdDeliveryManager { get; set; }
        public string NombreDeliveryManager { get; set; }

        public int IdCliente { get; set; }
        public string NombreCliente { get; set; }

        public bool TieneEsquemaVacunacion { get; set; }

        public int IdVacunacionColaborador { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<Colaborador, VacunacionColaboradorReporteDto>()
                .ForMember(d => d.IdColaborador, opt => opt.MapFrom(s => s.IDCOLABORADOR))
                .ForMember(d => d.Identificacion, opt => opt.MapFrom(s => s.IDENTIFICACION))
                .ForMember(d => d.NombreCompleto, opt => opt.MapFrom(s => s.NOMBRE + " " + s.PRIMERAPELLIDO + " " + s.SEGUNDOAPELLIDO))
                .ForMember(d => d.IdDeliveryManager, opt => opt.MapFrom(s => s.DeliveryManager != null ? s.DeliveryManager.IDCOLABORADOR : 0))
                .ForMember(d => d.NombreDeliveryManager, opt => opt.MapFrom(s => s.DeliveryManager != null ? s.DeliveryManager.NOMBRE + " " + s.DeliveryManager.PRIMERAPELLIDO + " " + s.DeliveryManager.SEGUNDOAPELLIDO : ""))
                .ForMember(d => d.IdCliente, opt => opt.MapFrom(s => s.Cliente != null ? s.Cliente.IDCLIENTE : 0))
                .ForMember(d => d.NombreCliente, opt => opt.MapFrom(s => s.Cliente != null ? s.Cliente.NOMBRE : ""))
                .ForMember(d => d.IdVacunacionColaborador, opt => opt.MapFrom(s => s.VacunacionColaborador != null && s.VacunacionColaborador.FirstOrDefault() != null ? s.VacunacionColaborador.FirstOrDefault().IdVacunacionColaborador : 0))
                .ForMember(d => d.TieneEsquemaVacunacion, opt => opt.MapFrom(s => s.VacunacionColaborador != null
                                                                                    && s.VacunacionColaborador.FirstOrDefault() != null
                                                                                    && s.VacunacionColaborador.FirstOrDefault().VacunasColaborador != null
                                                                                    && s.VacunacionColaborador.FirstOrDefault().VacunasColaborador.Count > 0));

        }


    }
}
