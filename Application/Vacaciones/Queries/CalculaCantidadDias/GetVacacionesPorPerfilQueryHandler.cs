using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Vacaciones.Queries.CalculaCantidadDias
{
    public class GetCalculaCantidadDiasListQueryHandler : IRequestHandler<GetCalculaCantidadDiasListQuery, CalculaCantidadDiasVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetCalculaCantidadDiasListQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<CalculaCantidadDiasVm> Handle(GetCalculaCantidadDiasListQuery request, CancellationToken cancellationToken)
        {
            CalculaCantidadDiasVm vm = new CalculaCantidadDiasVm();

            var colaborador = _context.Colaboradores
                .Where(c => c.IDCOLABORADOR == request.IdColaborador)
                .Include(c => c.PaisFeriado)
                .FirstOrDefault();


            DateTime[] bankHolidays = this.GetDiasFeriados(colaborador.PaisFeriado.CODPAIS);

            DateTime firstDay = request.FechaInicio.Date;
            DateTime lastDay = request.FechaFin.Date;
            //  if (firstDay > lastDay)
            //  throw new ArgumentException("Incorrect last day " + lastDay);

            TimeSpan span = lastDay - firstDay;
            int businessDays = span.Days + 1;
            int fullWeekCount = businessDays / 7;
            // find out if there are weekends during the time exceedng the full weeks
            if (businessDays > fullWeekCount * 7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend
                // in the time interval remaining after subtracting the complete weeks
                //int firstDayOfWeek = (int)firstDay.DayOfWeek;
                //int lastDayOfWeek = (int)lastDay.DayOfWeek;
                int firstDayOfWeek = firstDay.DayOfWeek == DayOfWeek.Sunday
                    ? 7 : (int)firstDay.DayOfWeek;
                int lastDayOfWeek = lastDay.DayOfWeek == DayOfWeek.Sunday
                    ? 7 : (int)lastDay.DayOfWeek;


                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                    businessDays -= 1;
            }

            // subtract the weekends during the full weeks in the interval
            businessDays -= fullWeekCount + fullWeekCount;

            // subtract the number of bank holidays during the time interval
            foreach (DateTime bankHoliday in bankHolidays)
            {
                DateTime bh = bankHoliday.Date;
                if (firstDay <= bh && bh <= lastDay && bh.DayOfWeek != DayOfWeek.Sunday
                 && bh.DayOfWeek != DayOfWeek.Saturday)
                {
                    --businessDays;
                }
            }


            vm.Cantidad = businessDays;

            return vm;
        }


        private DateTime[] GetDiasFeriados(string CodigoPais)
        {
            return  _context.Feriados.Where(f => f.Pais.CODPAIS == CodigoPais).Select(f => f.FECHA).ToArray();
        }



    }
}
