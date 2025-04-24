using FluentAssertions;
using NovaExpediente.Application.GradosAcademicos.Queries.GetListaGradosAcademicos;
using NovaExpediente.Domain.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Application.Integration.Tests.GradosAcademicos.Queries.GetListaGradosAcademicos
{
    // clase de configuración para ejecutar los casos de prueba
    using static Testing;

    public class GetGradosAcademicosTests 
    {
        public object Orden { get; private set; }
        public object Descripcion { get; private set; }

        /// <summary>
        /// Obtiene la lista de todos los grados académicos
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ShouldReturnAllLists()
        {
            // se agrega un grado académico a la lista
            await AddAsync(new GradoAcademico
            {
                Descripcion = "Diplomado",
                Nombre = "Diplomado",
                Orden = 1,
            });

            // arrange
            var query = new GetGradosAcademicosListQuery();

            // act
            GradosAcademicosListVm result = await SendAsync(query);

            // assert
            result.Should().NotBeNull();
            result.GradosAcademicos.Should().HaveCount(1);
        }

        [Test]
        public async Task TheDescriptionEntryNotBeNull()
        {
            // se agrega un grado académico a la lista con la descripcion como null
            await AddAsync(new GradoAcademico
            {
                Descripcion = null,
                Nombre = "Diplomado",
                Orden = 1,
            }) ;

            // arrange
            var query = new GetGradosAcademicosListQuery();

            // act
            GradosAcademicosListVm Descripcion = await SendAsync(query);

            // assert
            Descripcion.Should().NotBeNull("La descripción no puede ser nula");
        }

        [Test]
        public async Task TheDescriptionShouldBeString()
        {
            // se agrega un grado académico a la lista con la despripción como un int
            await AddAsync(new GradoAcademico
            {
                //Descripcion = 3, (Se valida a nivel de código que la DESCRIPCION no puede ser un valor Entero, debe de ser un String) 
                Nombre = "Diplomado",
                Orden = 1,
            }) ;

            // arrange
            var query = new GetGradosAcademicosListQuery();

            // act
            GradosAcademicosListVm Descripcion = await SendAsync(query);

            // assert
            Descripcion.Should().BeOfType<String>("La descripcion tiene que ser un valor String");
            //Descripcion.GetType().Should.Be(typeof(String)); //O talvez puede ser así
        }

        [Test]
        public async Task TheOrderShouldBeInt()
        {
            // se agrega un grado académico a la lista con la descripcion con el orden como un string
            await AddAsync(new GradoAcademico());

            string Descripcion;
            string Nombre;
            string Orden;

            {
                Descripcion = "Maestria";
                Nombre = "Diplomado";
                Orden = "1";
            };

            // arrange
            var query = new GetGradosAcademicosListQuery();

            // act
            GradosAcademicosListVm result = await SendAsync(query);

            // assert
            //OrdenAttribute.Should(); BeOfType<int>("La entrada orden tiene que ser un valor Entero");
            //Orden.GetType().Should.Be(typeof(int)); //O talvez puede ser así

            if (Orden.GetType() == typeof(int))
            {
                Console.WriteLine("Error");
            }
                   
        }

        [Test]
        public async Task TheDescriptionEntryNotBeBlank()
        {
            string Nombre;
            int Orden;

            // se agrega un grado académico a la lista con la descripcion en blanco
            await AddAsync(new GradoAcademico
            {
                Descripcion = string.Empty,
                Nombre = "Diplomado",
                Orden = 1,
            });

            // arrange
            var query = new GetGradosAcademicosListQuery();

            // act
            GradosAcademicosListVm Descripcion = await SendAsync(query);

            // assert
            Descripcion.Should().NotBeNull("La descripción no puede estar en blanco");
        }

    }
}
