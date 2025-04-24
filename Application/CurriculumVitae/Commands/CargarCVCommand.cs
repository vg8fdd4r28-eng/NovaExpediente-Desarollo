using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using System;
using System.Reflection;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.CurriculumVitae.Commands
{
    public class CargarCVCommand : IRequest<string>
    {
        public string nombreArchivo { get; set; }
        public string Json { get; set; }
        public int idUsuarioCrea { get; set; }

        public class CargarCVCommandHandler : IRequestHandler<CargarCVCommand, string>
        {
            private readonly INovaExpedienteDbContext _context;
            private readonly ICurriculumVitaeService _curriculumVitaeService;

            public CargarCVCommandHandler(INovaExpedienteDbContext context, ICurriculumVitaeService curriculumVitaeService)
            {
                _context = context;
                _curriculumVitaeService = curriculumVitaeService;
            }

            public async Task<string> Handle(CargarCVCommand request, CancellationToken cancellationToken)
            {
                var document = JsonSerializer.Deserialize<ModeloCarga>(request.Json);
                Type type = typeof(ModeloCarga);
                PropertyInfo propertyInfo = type.GetProperty("idCurriculum");
                if (propertyInfo != null && propertyInfo.PropertyType == typeof(int) && document.idCurriculum > 0)
                {
                    var datos = new
                    {
                        document.fechaRegistro,
                        nombreDocumento = request.nombreArchivo,
                        document.documentoBase64,
                        document.correo,
                        document.tipoDocumento
                    };
                    string jsonString = JsonSerializer.Serialize(datos);
                    await _context.ActualizarDetalleCV(jsonString, request.idUsuarioCrea, request.idUsuarioCrea, document.idCurriculum);
                    // utilizar servicio para actualizar cv
                    var (estado, mensaje) = await _curriculumVitaeService.ActualizarCVAsync(document);
                    await _context.ActualizarEstadoBitacora(document.idCurriculum, estado, mensaje, request.idUsuarioCrea, document.uuid);
                }
                else
                { 
                    var datos = new
                    {
                        document.fechaRegistro,
                        nombreDocumento = request.nombreArchivo,
                        document.documentoBase64,
                        document.correo,
                        document.tipoDocumento,
                        document.nombreUsuario
                    };
                    string jsonString = JsonSerializer.Serialize(datos);
                    var id = await _context.CargarDetalleCV(jsonString, request.idUsuarioCrea, request.idUsuarioCrea); 
                    // Utilizar servicio para cargar el CV
                    var (uuid, estado, mensaje) = await _curriculumVitaeService.CargarCVAsync(document);
                    // Registrar detalle en la bitácora
                    await _context.ActualizarEstadoBitacora(id, estado, mensaje, request.idUsuarioCrea, uuid);

                }
                return "";
            }
        }

        public class ModeloCarga
        {
            public int idCurriculum { get; set; }
            public DateTime fechaRegistro { get; set; }
            public string nombreDocumento { get; set; }
            public string documentoBase64 { get; set; }
            public string correo { get; set; }
            public string tipoDocumento { get; set; }
            public string uuid { get; set; }
            public string nombreUsuario { get; set; }
        }
    }
}
