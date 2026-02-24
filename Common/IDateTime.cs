using System;

namespace NovaExpediente.Common
{
    /// <summary>
    /// Interfaz para abstraer el acceso a fecha/hora del sistema.
    /// Permite inyectar implementaciones mock en pruebas unitarias.
    /// </summary>
    public interface IDateTime
    {
        /// <summary>
        /// Obtiene la fecha y hora actual del sistema
        /// </summary>
        DateTime Now { get; }
        
        /// <summary>
        /// Calcula la antigüedad entre dos fechas en formato legible (años, meses, días)
        /// </summary>
        /// <param name="fromDate">Fecha inicial</param>
        /// <param name="toDate">Fecha final</param>
        /// <returns>String con formato "X años Y meses Z días"</returns>
        string Seniority(DateTime fromDate, DateTime toDate);

        /// <summary>
        /// Calcula el número de días laborables entre dos fechas (excluyendo sábados y domingos)
        /// </summary>
        /// <param name="start">Fecha de inicio</param>
        /// <param name="stop">Fecha de fin</param>
        /// <returns>Número de días laborables</returns>
        int GetNumberOfWorkingDays(DateTime start, DateTime stop);
    }
}
