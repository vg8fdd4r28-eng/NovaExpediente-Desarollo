using FluentAssertions;
using NovaExpediente.Application.ExperienciasAmbientesDesarollo.Queries.GetListaExperienciasAmbientesDesarrollo;
using NovaExpediente.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Integration.Tests.ExperienciasAmbientesDesarollo.Queries.GetListaExperienciasAmbientesDesarrollo
{
    // clase de configuración para ejecutar los casos de prueba
    using static Testing;

    public class GetListaExperienciasAmbientesDesarrolloTests
    {
        public int IDUSUARIOCREA { get; private set; }
        public int? IDUSUARIOMODIFICA { get; private set; }
        public DateTime FECHACREACION { get; private set; }
        public DateTime? FECHAMODIFICACION { get; private set; }
        /// <summary>
        /// Obtiene la lista de la Experiencia en Ambientes de Desarrollo del consultor
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ShouldReturnAllLists()
        {
            // se agrega una Experiencia en Ambientes de Desarrollo a la lista
            await AddAsync(new ExperienciaAmbienteDesarrollo
            {
                IDCOLABORADOR = 1,
                IDAMBIENTEDESARROLLO = 2,
                IDNIVELCONOCIMIENTO = 2,
                MESESEXPERIENCIA = 20,
                IDUSUARIOCREA = 1,
                IDUSUARIOMODIFICA = 1,
                FECHACREACION = DateTime.Now,
                FECHAMODIFICACION = DateTime.Now,
                //AmbienteDesarrollo = "Python",
                //NivelConocimiento = 
                //PorcentajeConocimiento =
            });

            // arrange
            var query = new GetExperienciaAmbienteDesarrolloListQuery();

            // act
            ExperienciaAmbienteDesarrolloListVm result = await SendAsync(query);

            // assert
            result.Should().NotBeNull();
            result.ExperienciaAmbientesDesarrollo.Should().HaveCount(1);
        }

        [Test]
        public async Task TheMESESEXPERIENCIAEntryNotBeNull()
        {
            // se agrega una Experiencia en Ambientes de Desarrollo a la lista con los meses de experiencia como null
            await AddAsync(new ExperienciaAmbienteDesarrollo
            {
                IDCOLABORADOR = 1,
                IDAMBIENTEDESARROLLO = 1,
                IDNIVELCONOCIMIENTO = 1,
                //MESESEXPERIENCIA = null, (A nivel de código se valida que no es posible poner los MESESEXPERIENCIA como un valor nulo)
                IDUSUARIOCREA = 1,
                IDUSUARIOMODIFICA = 1,
                FECHACREACION = DateTime.Now,
                FECHAMODIFICACION = DateTime.Now,
                //AmbienteDesarrollo = "Python",
                //NivelConocimiento =
                //PorcentajeConocimiento =
            });

            // arrange
            var query = new GetExperienciaAmbienteDesarrolloListQuery();

            // act
            ExperienciaAmbienteDesarrolloListVm MESESEXPERIENCIA = await SendAsync(query);

            // assert
            MESESEXPERIENCIA.Should().NotBeNull("Los meses de Experiencia en Ambientes de Desarrollo no puede ser nula o estar en blanco");
        }
    }
}
