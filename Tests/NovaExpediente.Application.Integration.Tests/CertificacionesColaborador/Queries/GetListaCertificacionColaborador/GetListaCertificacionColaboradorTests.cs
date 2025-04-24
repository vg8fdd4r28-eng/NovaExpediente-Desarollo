using FluentAssertions;
using NovaExpediente.Application.CertificacionesColaborador.Queries.GetListaCertificacionColaborador;
using NovaExpediente.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Integration.Tests.CertificacionesColaborador.Queries.GetListaCertificacionColaborador
{
    // clase de configuración para ejecutar los casos de prueba
    using static Testing;

    public class GetListaCertificacionColaboradorTests
{
        public int IDCOLABORADOR { get; private set; }
        public int IDCERTIFICACIONPROVEEDOR { get; private set; }
        public int IDCERTIFICACION { get; private set; }
        public string DESCRIPCION { get; private set; }
        public int ANIO { get; private set; }
        public DateTime? VENCE { get; private set; }
        public DateTime FECHACREACION { get; private set; }
        public DateTime? FECHAMODIFICACION { get; private set; }

        /// <summary>
        /// Obtiene la lista de las certificaciones del consultor
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ShouldReturnAllLists()

        {
            // se agrega una Certificacion a la lista
            await AddAsync(new CertificacionColaborador

        {
            IDCERTIFICACIONCOLABORADOR = 1,
            IDCOLABORADOR = 1,
            IDCERTIFICACIONPROVEEDOR = 1,
            IDCERTIFICACION = 1,
            DESCRIPCION = "Descripcion de la Certificacion 1",
            ANIO = 2015,
            VENCE = DateTime.Now,
            FECHACREACION = DateTime.Now,
            FECHAMODIFICACION = DateTime.Now,
            //Certificacion = "CertificacionColaborador",
        }) ;

            // arrange
            var query = new GetCertificacionColaboradorListQuery();

            // act
            CertificacionColaboradorListVm result = await SendAsync(query);

            // assert
            result.Should().NotBeNull();
            result.CertificacionesColaborador.Should().HaveCount(1);
        }

        [Test]
        public async Task TheIDENTIFICACIONEntryNotBeNull()
        {
            // se agrega un IDCERTIFICACION a la lista con la descripcion como null
            await AddAsync(new CertificacionColaborador
            {
                IDCERTIFICACIONCOLABORADOR = 1,
                IDCOLABORADOR = 1,
                IDCERTIFICACIONPROVEEDOR = 1,
                //IDCERTIFICACION = null, (A nivel de código se valida que no es posible poner el IDCERTIFICAION como un valor nulo)
                DESCRIPCION = "Descripcion de la Certificacion 1",
                ANIO = 2015,
                VENCE = DateTime.Now,
                FECHACREACION = DateTime.Now,
                FECHAMODIFICACION = DateTime.Now,
                //Certificacion = "Certificacion 1",
            });

            // arrange
            var query = new GetCertificacionColaboradorListQuery();

            // act
            CertificacionColaboradorListVm IDCERTIFICACION = await SendAsync(query);

            // assert
            IDCERTIFICACION.Should().NotBeNull("La identificacion no puede ser nula o estar en blanco");
        }

        [Test]
        public async Task TheDESCRIPCIONEntryNotBeInt()
        {
            // se agrega una DESCRIPCION a la lista con un entero
            await AddAsync(new CertificacionColaborador
            {
                IDCERTIFICACIONCOLABORADOR = 1,
                IDCOLABORADOR = 1,
                IDCERTIFICACIONPROVEEDOR = 1,
                IDCERTIFICACION = 1,
                //DESCRIPCION = 2, (A nivel de código se valida que no es posible poner la DESCRIPCION como un valor Entero, debe de ser un String)
                ANIO = 2015,
                VENCE = DateTime.Now,
                FECHACREACION = DateTime.Now,
                FECHAMODIFICACION = DateTime.Now,
                //Certificacion = "Certificacion 1",
            });

            // arrange
            var query = new GetCertificacionColaboradorListQuery();

            // act
            CertificacionColaboradorListVm DESCRIPCION = await SendAsync(query);

            // assert
            DESCRIPCION.Should().NotBeOfType<string>("La identificacion no puede ser un int");
        }
    }
}
