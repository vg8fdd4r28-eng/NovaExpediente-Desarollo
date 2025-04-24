using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.AplicacionMovil.Eventos.GetListaEventos
{
    public class GetListaEventosQueryHandler : IRequestHandler<GetListaEventosQuery, JObject>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;

        public GetListaEventosQueryHandler(INovaExpedienteDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<JObject> Handle(GetListaEventosQuery request, CancellationToken cancellationToken)
        {
            var eventos = await _context.Eventos
                                                .Include(e => e.Imagenes)
                                                    .ThenInclude(imgEvento => imgEvento.Imagen)
                                                .Include(e => e.EventoSecciones) // Include the EventoSecciones navigation property
                                                .ToListAsync();

            var eventDtos = eventos.Select(e => new EventoDto
            {
                IDEVENTO = e.IDEVENTO,
                IDIDIOMA = e.IDIDIOMA,
                NOMBRE = e.NOMBRE,
                DESCRIPCION = e.DESCRIPCION,
                FECHA = e.FECHA,
                ENLACE = e.ENLACE,
                ACTIVO = e.ACTIVO,
                LAT = e.LAT.ToString(),
                LNG = e.LNG.ToString(),
                Imagenes = e.Imagenes?.Select(img => img.Imagen != null ? new ImagenDto
                {
                    IDIMAGEN = img.Imagen.IDIMAGEN,
                    URLIMAGEN = img.Imagen.URLIMAGEN
                } : null).ToList(),
                EventoSecciones = e.EventoSecciones?.Select(es => es != null ? new EventoSeccionDto
                {
                    IDEVENTOSECCION = es.IDEVENTOSECCION,
                    NOMBRE = es.NOMBRE,
                    TITULO = es.TITULO,
                    TIPO = es.TIPO,
                    OPCIONES = es.OPCIONES.Split(',', StringSplitOptions.RemoveEmptyEntries),
                    ESREQUERIDO = es.ESREQUERIDO
                } : null).ToList()
            }).ToList();



                var vm = new ListaEventosVm
                {
                    Eventos = eventDtos,
                    Count = eventDtos.Count,
                };
                // Convert the result to a JObject
                JObject jsonObject = JObject.FromObject(vm);

                // Convert all property names to uppercase
                MakePropertiesUppercase(jsonObject);

                return jsonObject;
        }

        private static void MakePropertiesUppercase(JToken token)
        {
            if (token is JObject obj)
            {
                foreach (var prop in obj.Properties().ToList())
                {
                    obj.Remove(prop.Name);
                    obj.Add(prop.Name.ToUpperInvariant(), prop.Value);
                }

                foreach (JToken childToken in obj.Values())
                {
                    MakePropertiesUppercase(childToken);
                }
            }
            else if (token is JArray arr)
            {
                foreach (JToken childToken in arr)
                {
                    MakePropertiesUppercase(childToken);
                }
            }
        }
    }
}
