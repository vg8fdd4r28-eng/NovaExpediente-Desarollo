using FluentAssertions;
using NovaExpediente.Application.Colaboradores.Queries.GetListaColaboradores;
using NovaExpediente.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Integration.Tests.Colaboradores.Queries.GetListaColaboradores
{
    // clase de configuración para ejecutar los casos de prueba
    using static Testing;

    public class GetListaColaboradoresTests
    {
        public int IDPUESTO { get; private set; }
        public int IDPAISORIGEN { get; private set; }
        public int IDPAISRESIDENCIA { get; private set; }
        public int IDUSUARIOCREA { get; private set; }
        public int? IDUSUARIOMODIFICA { get; private set; }
        public int IDCANTON { get; private set; }
        public string IDENTIFICACION { get; private set; }
        public string PRIMERAPELLIDO { get; private set; }
        public string SEGUNDOAPELLIDO { get; private set; }
        public DateTime FECHANACIMIENTO { get; private set; }
        public char GENERO { get; private set; }
        public DateTime FECHAINGRESO { get; private set; }
        public string DIRECCION { get; private set; }
        public string CELULAR { get; private set; }
        public string TELEFONO { get; private set; }
        public string CORREO { get; private set; }
        public string CORREOPERSONAL { get; private set; }
        public string RESENAPERFIL { get; private set; }
        public string NUMEROCPIC { get; private set; }
        public string RAMAESPECIALIDAD { get; private set; }
        public bool ACTIVO { get; set; }
        //  public image FOTO { get; set; }
        public char ESTADOCIVIL { get; private set; }
        public string TELEFONOEMERGENCIA { get; private set; }
        public string PERSONALLAMAR { get; private set; }
        public int NUMEROHIJOS { get; private set; }
        /// <summary>
        /// Obtiene la lista de todos los colaboradores
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ShouldReturnAllLists()
        {
            // se agrega un Colaborador a la lista
            await AddAsync(new Colaborador
            {
                IDCOLABORADOR = 1,
                IDPUESTO = 1,
                IDPAISORIGEN = 1,
                IDPAISRESIDENCIA = 1,
                IDUSUARIOCREA = 1,
                IDUSUARIOMODIFICA = 1,
                IDCANTON = 1,
                IDENTIFICACION = "113110814",
                NOMBRE = "Allan",
                PRIMERAPELLIDO = "Vado",
                SEGUNDOAPELLIDO = "Loaiza",
                FECHANACIMIENTO = DateTime.Now,
                GENERO = 'M',
                FECHAINGRESO = DateTime.Now,
                DIRECCION = "Sabana",
                CELULAR = "63547760",
                TELEFONO = "63547760",
                CORREO = "avado@novacompcr.com",
                CORREOPERSONAL = "allanvado@gmail.com",
                RESENAPERFIL = "QA",
                NUMEROCPIC = "1234",
                RAMAESPECIALIDAD = "QA",
                ACTIVO = true,
                FOTO = null,
                ESTADOCIVIL = 'S',
                TELEFONOEMERGENCIA = "911",
                PERSONALLAMAR = "Emergencias",
                NUMEROHIJOS = 0,
            }) ;

            // arrange
            var query = new GetColaboradoresListQuery();

            // act
            ColaboradoresListVm result = await SendAsync(query);

            // assert
            result.Should().NotBeNull();
            result.Colaboradores.Should().HaveCount(1);
        }

        [Test]
        public async Task TheIDENTIFICACIONEntryNotBeNull()
        {
            // se agrega un IDENTIFICACION a la lista con la descripcion como null
            await AddAsync(new Colaborador
            {
                IDCOLABORADOR = 1,
                IDPUESTO = 1,
                IDPAISORIGEN = 1,
                IDPAISRESIDENCIA = 1,
                IDUSUARIOCREA = 1,
                IDUSUARIOMODIFICA = 1,
                IDCANTON = 1,
                IDENTIFICACION = String.Empty,
                NOMBRE = "Allan",
                PRIMERAPELLIDO = "Vado",
                SEGUNDOAPELLIDO = "Loaiza",
                FECHANACIMIENTO = DateTime.Now,
                GENERO = 'M',
                FECHAINGRESO = DateTime.Now,
                DIRECCION = "Sabana",
                CELULAR = "63547760",
                TELEFONO = "63547760",
                CORREO = "avado@novacompcr.com",
                CORREOPERSONAL = "allanvado@gmail.com",
                RESENAPERFIL = "QA",
                NUMEROCPIC = "1234",
                RAMAESPECIALIDAD = "QA",
                ACTIVO = true,
                FOTO = null,
                ESTADOCIVIL = 'S',
                TELEFONOEMERGENCIA = "911",
                PERSONALLAMAR = "Emergencias",
                NUMEROHIJOS = 0,
            });

            // arrange
            var query = new GetColaboradoresListQuery();

            // act
            ColaboradoresListVm IDENTIFICACION = await SendAsync(query);

            // assert
            IDENTIFICACION.Should().NotBeNull("La identificacion no puede ser nula o estar en blanco");
        }
    }
}
