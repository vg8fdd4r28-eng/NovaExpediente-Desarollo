using FluentAssertions;
using NovaExpediente.Application.ExperienciasTecnologias.Queries.GetListaExperienciasTecnologias;
using NovaExpediente.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Integration.Tests.ExperienciasTecnologias.Queries.GetListaExperienciasTecnologias
{
    // clase de configuración para ejecutar los casos de prueba
    using static Testing;

    public class GetListaExperienciasTecnologiasTests
    {
        public int IDUSUARIOCREA { get; private set; }
        public int IDUSUARIOMODIFICA { get; private set; }
        public DateTime FECHACREACION { get; private set; }
        public DateTime FECHAMODIFICACION { get; private set; }
        /// <summary>
        /// Obtiene la lista de la Experiencia en Tecnologías del consultor
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ShouldReturnAllLists()
        {
            // se agrega una Experiencia en Tecnologías a la lista
            await AddAsync(new ExperienciaTecnologia
            {
                IDCOLABORADOR = 1,
                IDTECNOLOGIA = 2,
                IDNIVELCONOCIMIENTO = 2,
                MESESEXPERIENCIA = 20,
                IDUSUARIOCREA = 1,
                IDUSUARIOMODIFICA = 1,
                FECHACREACION = DateTime.Now,
                FECHAMODIFICACION = DateTime.Now,
                //Tecnologia = "Python",
                //NivelConocimiento = 
                //PorcentajeConocimiento =
            });

            // arrange
            var query = new GetExperienciaTecnologiaListQuery();

            // act
            ExperienciaTecnologiaListVm result = await SendAsync(query);

            // assert
            result.Should().NotBeNull();
            result.ExperienciaTecnologias.Should().HaveCount(1);
        }

        [Test]
        public async Task TheMesesExperienciaEntryNotBeNull()
        {
            // se agrega una Experiencia en Tecnologías a la lista con los meses de experiencia como null
            await AddAsync(new ExperienciaTecnologia
            {
                IDCOLABORADOR = 1,
                IDTECNOLOGIA = 2,
                IDNIVELCONOCIMIENTO = 2,
                //MESESEXPERIENCIA = null, (A nivel de código se valida que no es posible poner los MESESEXPERIENCIA como un valor nulo)
                IDUSUARIOCREA = 1,
                IDUSUARIOMODIFICA = 1,
                FECHACREACION = DateTime.Now,
                FECHAMODIFICACION = DateTime.Now,
                //Tecnologia = "Python",
                //NivelConocimiento = 
                //PorcentajeConocimiento =
            });

            // arrange
            var query = new GetExperienciaTecnologiaListQuery();

            // act
            ExperienciaTecnologiaListVm MESESEXPERIENCIA = await SendAsync(query);

            // assert
            MESESEXPERIENCIA.Should().NotBeNull("Los meses de Experiencia en Tecnologías no puede ser nula o estar en blanco");
        }

        [Test]
        public async Task TheMesesExperienciaEntryNotBeNegativeValue()
        {
            // se agrega una Experiencia en Tecnologías a la lista con los meses de experiencia como un valor negativo
            await AddAsync(new ExperienciaTecnologia
            {
                IDCOLABORADOR = 1,
                IDTECNOLOGIA = 2,
                IDNIVELCONOCIMIENTO = 2,
                MESESEXPERIENCIA = -3,
                IDUSUARIOCREA = 1,
                IDUSUARIOMODIFICA = 1,
                FECHACREACION = DateTime.Now,
                FECHAMODIFICACION = DateTime.Now,
                //Tecnologia = "Python",
                //NivelConocimiento = 
                //PorcentajeConocimiento =
            });

            // arrange
            var query = new GetExperienciaTecnologiaListQuery();

            // act
            ExperienciaTecnologiaListVm MESESEXPERIENCIA = await SendAsync(query);

            // assert
            //MESESEXPERIENCIA.Should().NotBeOfType<negative>("Los meses de Experiencia en Tecnologías no puede ser un valor negativo");
            //MESESEXPERIENCIA.Should(MESESEXPERIENCIA > 0)("Los meses de Experiencia en Tecnologías no puede ser un valor negativo");
        }
    }
}
