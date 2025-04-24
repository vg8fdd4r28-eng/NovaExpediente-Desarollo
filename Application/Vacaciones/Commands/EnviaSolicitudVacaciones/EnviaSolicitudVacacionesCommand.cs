using AutoMapper;
using MediatR;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.MasterData.Commands.Workflow;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using static NovaExpediente.Application.MasterData.Commands.Workflow.WorkflowCommand;
using Newtonsoft.Json;

namespace NovaExpediente.Application.Vacaciones.Commands.EnviaSolicitudVacaciones
{
    public class EnviaSolicitudVacacionesCommand : IRequest<int>
    {
        public int idColaborador { get; set; }
        public string tipoSolicitud { get; set; }
        public DateTime fechaInicio { get; set; }
        public DateTime fechaFinal { get; set; }
        public int cantDias { get; set; }
        public decimal saldoVacaciones { get; set; }
        public decimal nuevoSaldo { get; set; }
        public string descripcion { get; set; }
      //  public string Comentario { get; set; }



        public class UpsertCategoryCommandHandler : IRequestHandler<EnviaSolicitudVacacionesCommand, int>
        {
            private readonly INovaExpedienteDbContext _context;
            private readonly IFireForgetCommandHandler _fireForgetCommand;
            private readonly ISecurityService _securityService;
            private readonly INotificationService _notificationService;
            private readonly INominaService _nominaService;
            private readonly IMapper _mapper;


            public UpsertCategoryCommandHandler(INovaExpedienteDbContext context, IFireForgetCommandHandler fireForgetCommand,
                                     ISecurityService securityService, INotificationService notificationService,
                                     INominaService nominaService, IMapper mapper) //, IEventBus eventBus)
            {
                _context = context;
                _fireForgetCommand = fireForgetCommand;
                _securityService = securityService;
                _notificationService = notificationService;
                _nominaService = nominaService;
                _mapper = mapper;
            }

            public async Task<int> Handle(EnviaSolicitudVacacionesCommand request, CancellationToken cancellationToken)
            {
                string tipoSolicitudNombre = "";
                switch (request.tipoSolicitud)
                {
                    case "Vacaciones":
                        tipoSolicitudNombre = "Solicitud de vacaciones";
                        break;

                    case "Permisos":
                        tipoSolicitudNombre = "Solicitud de permiso";
                        break;

                    case "Incapacidades":
                        tipoSolicitudNombre = "Registro de incapacidades";
                        break;

                    default:
                        break;
                }

                var tipoSolicitud = _context.TipoSolicitud
                                     .Where(c => c.NOMBRE == tipoSolicitudNombre)
                                     .FirstOrDefault();


                int result = 0;


                if (tipoSolicitud != null)
                {




                    WorkflowCommandHandler workflow = new WorkflowCommandHandler(_context, _fireForgetCommand, _securityService, _notificationService, _nominaService, _mapper);


                    DocSolicitud docJson = new DocSolicitud
                    {
                        fechaInicio = request.fechaInicio.ToString("yyyy-MM-dd"),
                        fechaFinal = request.fechaFinal.ToString("yyyy-MM-dd"),
                        idColaborador = request.idColaborador,
                        cantDias = request.cantDias,
                        saldoVacaciones = request.saldoVacaciones,
                        nuevoSaldo = request.nuevoSaldo,
                        descripcion = request.descripcion

                    };

                    WorkflowCommand command = new WorkflowCommand();

                    command.IdSolicitud = null;
                    command.IdTipoSolicitud = tipoSolicitud.IDTIPOSOLICITUD;
                    command.CodigoEstado = "RE";
                    command.IdAutor = request.idColaborador;
                    command.IdResponsable = request.idColaborador;
                    command.IdColaborador = request.idColaborador;
                    command.Accion = "Enviar";
                    command.Comentario = "";
                    command.DocSolicitud = JsonConvert.SerializeObject(docJson, Formatting.Indented);

                    WorkflowCommandValidator workflowValidator = new WorkflowCommandValidator(_context);
                    var resultadoValidacionWorkFlow = workflowValidator.Validate(command);

                    if (!resultadoValidacionWorkFlow.IsValid)
                    {
                        throw new Exception(resultadoValidacionWorkFlow.Errors[0].ToString());
                    }

                    result = await workflow.Handle(command, cancellationToken);


                }
                else
                {
                    throw new Exception("No existe el tipo de solicitud");
                };



                return result;

            }
        }


        private class DocSolicitud
        {
            public int idColaborador { get; set; }
            public string fechaInicio { get; set; }
            public string fechaFinal { get; set; }
            public int cantDias { get; set; }
            public decimal saldoVacaciones { get; set; }
            public decimal nuevoSaldo { get; set; }
            public string descripcion { get; set; }

        }

    }
}
