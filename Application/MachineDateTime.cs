using System;
using NovaExpediente.Common;

namespace NovaExpediente.Application
{
    /// <summary>
    /// Implementación concreta de IDateTime que usa la fecha/hora del sistema.
    /// Proporciona utilidades para cálculos de fechas y días laborables.
    /// </summary>
    public class MachineDateTime : IDateTime
    {
        /// <summary>
        /// Obtiene la fecha y hora actual del sistema
        /// </summary>
        public DateTime Now => DateTime.Now;

        /// <summary>
        /// Obtiene el año actual
        /// </summary>
        public int CurrentYear => DateTime.Now.Year;

        /// <summary>
        /// Calcula la antigüedad entre dos fechas en formato legible.
        /// Ejemplo: "2 años 3 meses 15 días"
        /// </summary>
        /// <param name="fromDate">Fecha de inicio</param>
        /// <param name="toDate">Fecha de fin</param>
        /// <returns>String formateado con años, meses y días</returns>
        public string Seniority(DateTime fromDate, DateTime toDate)
        {
            try
            {
                // Array con días de cada mes (febrero = -1 para calcular año bisiesto)
                int[] monthDay = new int[12] { 31, -1, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
                int years, months, days, increment = 0; 

                // Si el día inicial es mayor que el final, ajustar con días del mes anterior
                if (fromDate.Day > toDate.Day)
                {
                    increment = monthDay[fromDate.Month - 1];
                }

                // Calcular días de febrero según año bisiesto
                if (increment == -1)
                {
                    if (DateTime.IsLeapYear(fromDate.Year))
                    {
                        increment = 29;
                    }
                    else
                    {
                        increment = 28;
                    }
                }

                // Calcular días
                if (increment != 0)
                {
                    days = (toDate.Day + increment) - fromDate.Day;
                    increment = 1;
                }
                else
                {
                    days = toDate.Day - fromDate.Day;
                }

                // Calcular meses
                if ((fromDate.Month + increment) > toDate.Month)
                {
                    months = (toDate.Month + 12) - (fromDate.Month + increment);
                    increment = 1;
                }
                else
                {
                    months = (toDate.Month) - (fromDate.Month + increment);
                    increment = 0;
                }

                // Calcular años
                years = toDate.Year - (fromDate.Year + increment);

                // Formatear resultado
                return string.Concat(years > 0 ? years.ToString() + " años " : string.Empty,
                                 months > 0 ? months.ToString() + " meses " : string.Empty,
                                 days > 0 ? days.ToString() + " días " : string.Empty);

            }
            catch (Exception)
            {
                return "";
            }
        }

        /// <summary>
        /// Calcula el número de días laborables entre dos fechas (excluyendo fines de semana).
        /// </summary>
        /// <param name="start">Fecha de inicio</param>
        /// <param name="stop">Fecha de fin</param>
        /// <returns>Número de días laborables</returns>
        public int GetNumberOfWorkingDays(DateTime start, DateTime stop)
        {
            var days = (stop - start).Days + 1;
            return workDaysInFullWeeks(days) + workDaysInPartialWeek(start.DayOfWeek, days);
        }

        /// <summary>
        /// Calcula días laborables en semanas completas (5 días por semana)
        /// </summary>
        private int workDaysInFullWeeks(int totalDays)
        {
            return (totalDays / 7) * 5;
        }

        /// <summary>
        /// Calcula días laborables en la semana parcial restante
        /// </summary>
        private int workDaysInPartialWeek(DayOfWeek firstDay, int totalDays)
        {
            var remainingDays = totalDays % 7;
            var daysToSaturday = (int)DayOfWeek.Saturday - (int)firstDay;
            
            // Si termina antes del sábado, todos los días son laborables
            if (remainingDays <= daysToSaturday)
                return remainingDays;
            
            // Si termina en sábado o domingo, solo contar hasta viernes
            if (remainingDays <= daysToSaturday + 2)
                return daysToSaturday;
            
            // Si termina después del domingo, restar el fin de semana
            else
                return remainingDays - 2;
        }
    }
}
