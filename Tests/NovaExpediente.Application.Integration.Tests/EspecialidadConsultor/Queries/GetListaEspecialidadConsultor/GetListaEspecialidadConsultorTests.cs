using FluentAssertions;
using NovaExpediente.Application.EspecialidadesConsultor.Queries.GetListaEspecialidadConsultor;
using NovaExpediente.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Integration.Tests.EspecialidadesConsultor.Queries.GetListaEspecialidadConsultor
{
    // clase de configuración para ejecutar los casos de prueba
    using static Testing;

    public class GetListaEspecialidadConsultorTests
    {
        public int IDESPECIALIDAD { get; private set; }
        public int IDUSUARIOCREA { get; private set; }
        public int IDCOLABORADOR { get; private set; }
        public int? IDUSUARIOMODIFICA { get; private set; }
        public int EXPERIENCIA { get; private set; }
        public DateTime FECHACREACION { get; private set; }
        public DateTime? FECHAMODIFICACION { get; private set; }
        public string Especialidad { get; private set; }
        /// <summary>
        /// Obtiene la lista de las especialidades del consultor
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ShouldReturnAllLists()
        {
            // se agrega una Especialidad a la lista
            await AddAsync(new EspecialidadConsultor
            {
                IDESPECIALIDADCONSULTOR = 1,
                IDESPECIALIDAD = 1,
                IDUSUARIOCREA = 1,
                IDCOLABORADOR = 1,
                IDUSUARIOMODIFICA = 1,
                EXPERIENCIA = 3,
                FECHACREACION = DateTime.Now,
                FECHAMODIFICACION = DateTime.Now,
                //Especialidad = "Calidad",
            }) ;

            // arrange
            var query = new GetEspecialidadConsultorListQuery();

            // act
            EspecialidadConsultorListVm result = await SendAsync(query);

            // assert
            result.Should().NotBeNull();
            result.EspecialidadesConsultor.Should().HaveCount(1);
        }

        [Test]
        public async Task TheEXPERIENCIAEntryNotSTRING()
        {
            // se agrega una EXPERIENCIA a la lista como un string
            await AddAsync(new EspecialidadConsultor
            {
                IDESPECIALIDADCONSULTOR = 1,
                IDESPECIALIDAD = 1,
                IDUSUARIOCREA = 1,
                IDCOLABORADOR = 1,
                IDUSUARIOMODIFICA = 1,
                //EXPERIENCIA = "medio", (A nivel de código se valida que no es posible poner una EXPERIENCIA como un valor String, debe de ser un Entero)
                FECHACREACION = DateTime.Now,
                FECHAMODIFICACION = DateTime.Now,
                //Especialidad = "Calidad",
            });

            // arrange
            var query = new GetEspecialidadConsultorListQuery();

            // act
            EspecialidadConsultorListVm EXPERIENCIA = await SendAsync(query);

            // assert
            EXPERIENCIA.Should().NotBeOfType<string> ("La EXPERIENCIA no puede ser un string");
        }
    }
}

