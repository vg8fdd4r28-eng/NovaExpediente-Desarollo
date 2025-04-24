using FluentAssertions;
using NovaExpediente.Application.IdiomasColaborador.Queries.GetListaIdiomasColaborador;
using NovaExpediente.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Integration.Tests.IdiomasColaborador.Queries.GetListaIdiomasColaborador
{
    // clase de configuración para ejecutar los casos de prueba
    using static Testing;

    public class GetListaIdiomasColaboradorTests
    {
        public int IDNIVELIDIOMA { get; private set; }
        public int IDUSUARIOCREA { get; private set; }
        public int? IDUSUARIOMODIFICA { get; private set; }
        public DateTime FECHACREACION { get; private set; }
        public DateTime? FECHAMODIFICACION { get; private set; }
        /// <summary>
        /// Obtiene la lista de los idiomas del consultor
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ShouldReturnAllLists()
        {
            // se agrega un Idioma a la lista
            await AddAsync(new IdiomaColaborador
            {
                IDIDIOMA = 1,
                IDNIVELIDIOMA = 1,
                IDUSUARIOCREA = 1,
                IDCOLABORADOR = 1,
                IDUSUARIOMODIFICA = 1,
                FECHACREACION = DateTime.Now,
                FECHAMODIFICACION = DateTime.Now,
                //Idioma = 
                //NivelIdioma = "Intermedio",
            });

            // arrange
            var query = new GetIdiomasColaboradorListQuery();

            // act
            IdiomaColaboradorListVm result = await SendAsync(query);

            // assert
            result.Should().NotBeNull();
            result.IdiomasColaborador.Should().HaveCount(1);
        }

        [Test]
        public async Task TheNivelIdiomaEntryNotBeNull()
        {
            // se agrega un NivelIdioma a la lista con la descripcion como null
            await AddAsync(new IdiomaColaborador
            {
                IDIDIOMA = 1,
                //IDNIVELIDIOMA = null, (Se valida a nivel de código que el IDNIVELIDIOMA no puede ser un valor nulo)
                IDUSUARIOCREA = 1,
                IDCOLABORADOR = 1,
                IDUSUARIOMODIFICA = 1,
                FECHACREACION = DateTime.Now,
                FECHAMODIFICACION = DateTime.Now,
                //Idioma =
                //NivelIdioma = "Intermedio",
            });

            // arrange
            var query = new GetIdiomasColaboradorListQuery();

            // act
            IdiomaColaboradorListVm IDNIVELIDIOMA = await SendAsync(query);

            // assert
            IDNIVELIDIOMA.Should().NotBeNull("El nivel de idioma no puede ser nula o estar en blanco");
        }
    }
}
