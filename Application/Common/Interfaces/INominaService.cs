using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Common.Interfaces
{
    public interface INominaService
    {
        Task<string> Login();
        Task<string> LogOut(string session);
        Task<string> ConsultaSaldo(int? IdNomina);
        Task<Dictionary<int, decimal>> ConsultaSaldo(Dictionary<int, int?> identificadoresNomina);
        Task<string> ActualizaSaldoVacaciones(int? IdNomina, DateTime FechaInicio, DateTime FechaFinal, int CantDias, long numeroSolicitud);


    }
}
