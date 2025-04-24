using AutoMapper;
using NovaExpediente.Application.Common.Mappings;
using NovaExpediente.Domain.Entities;
using System;

namespace NovaExpediente.Application.UnosAUnos.Queries.GetListaUnosAUnos
{
    public class UnoAUnoDto : IMapFrom<UnoAUno>
    {
        public int IdUnoAUno { get; set; }
        public int IdColaborador { get; set; }
        public int IdUnoAUnoEstadoColaborador { get; set; }
        public DateTime FechaEjecucion { get; set; }
        public String Observacion { get; set; }
        public bool Activo { get; set; }
        public int IdDeliveryManager { get; set; }
        public string Colaborador { get; set; }
        public string EstadoColaborador { get; set; }
        public int Mes { get; set; }
        public string MesTexto { get; set; }
        public int Anno { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UnoAUno, UnoAUnoDto>()
            .ForMember(d => d.IdUnoAUno, opt => opt.MapFrom(s => s.IDUNOAUNO))
            .ForMember(d => d.IdColaborador, opt => opt.MapFrom(s => s.IDCOLABORADOR))
            .ForMember(d => d.IdUnoAUnoEstadoColaborador, opt => opt.MapFrom(s => s.IDUNOAUNOESTADOCOLABORADOR))
            .ForMember(d => d.Activo, opt => opt.MapFrom(s => s.ACTIVO))
            .ForMember(d => d.IdDeliveryManager, opt => opt.MapFrom(s => s.Colaborador.IDDELIVERYMANAGER))
            .ForMember(d => d.Colaborador, opt => opt.MapFrom(s => string.Format("{0} {1} {2}", s.Colaborador.NOMBRE.Trim(), s.Colaborador.PRIMERAPELLIDO.Trim(), s.Colaborador.SEGUNDOAPELLIDO.Trim())))
            .ForMember(d => d.EstadoColaborador, opt => opt.MapFrom(s => s.UnoAUnoEstadoColaborador.NOMBRE))
            .ForMember(d => d.Mes, opt => opt.MapFrom(s => s.FECHA_EJECUCION.Month))
            .ForMember(d => d.MesTexto, opt => opt.MapFrom(s => GetMes(s.FECHA_EJECUCION.Month)))
            .ForMember(d => d.Anno, opt => opt.MapFrom(s => s.FECHA_EJECUCION.Year));
        }

        public static string GetMes(int mes)
        {
            string MesTexto = string.Empty;
            switch (mes)
            {
                case 1:
                    MesTexto = "Enero";
                    break;
                case 2:
                    MesTexto = "Febrero";
                    break;
                case 3:
                    MesTexto = "Marzo";
                    break;
                case 4:
                    MesTexto = "Abril";
                    break;
                case 5:
                    MesTexto = "Mayo";
                    break;
                case 6:
                    MesTexto = "Junio";
                    break;
                case 7:
                    MesTexto = "Julio";
                    break;
                case 8:
                    MesTexto = "Agosto";
                    break;
                case 9:
                    MesTexto = "Septiembre";
                    break;
                case 10:
                    MesTexto = "Octubre";
                    break;
                case 11:
                    MesTexto = "Noviembre";
                    break;
                case 12:
                    MesTexto = "Diciembre";
                    break;
            }
            return MesTexto;
        }

    }
}
