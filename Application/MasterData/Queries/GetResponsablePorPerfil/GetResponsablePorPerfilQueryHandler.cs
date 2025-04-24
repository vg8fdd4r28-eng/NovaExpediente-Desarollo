using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NovaExpediente.Application.Common.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace NovaExpediente.Application.MasterData.Queries.GetResponsablePorPerfil
{
    public class GetResponsablePorPerfilQueryHandler : IRequestHandler<GetResponsablePorPerfilQuery, ResponsablePorPerfilVm>
    {
        private readonly INovaExpedienteDbContext _context;
        private readonly IMapper _mapper;
        private readonly ISecurityService _securityService;


        public GetResponsablePorPerfilQueryHandler(INovaExpedienteDbContext context, IMapper mapper, ISecurityService securityService)
        {
            _context = context;
            _mapper = mapper;
            _securityService = securityService;

        }

        public async Task<ResponsablePorPerfilVm> Handle(GetResponsablePorPerfilQuery request, CancellationToken cancellationToken)
        {
            ResponsablePorPerfilVm vm = new ResponsablePorPerfilVm();
            List<ResponsablePorPerfilDto> responsables = new List<ResponsablePorPerfilDto>();




            var Rol = _context.TransicionTipoSolicitud
                                      .Where(c => c.ESTADOSIGUIENTE == request.estadoSolicitud && c.IDTIPOSOLICITUD == request.idTipoSolicitud)
                                      .Select(c => c.PERFILRESPONSABLE)
                                     .FirstOrDefault();


            // si es RH todos los RH
            if (Rol.Contains("Expediente.RRHH"))
            {

                var usuariosEmail = await _securityService.GetUsersWithRole(("Expediente.RRHH").ToUpper());
                responsables = _context.Colaboradores
                                    .Where(c => usuariosEmail.Contains(c.CORREO))
                                    .ProjectTo<ResponsablePorPerfilDto>(_mapper.ConfigurationProvider)
                                    .ToList();

                responsables = responsables.Where(r => r.IDCOLABORADOR != request.IdColaborador && r.IDCOLABORADOR != request.IdColaboradorSolicitud).ToList();

            }
            // si es SuperUsuario todos los SuperUsuario
            else if (Rol.Contains("Expediente.SuperUsuario"))
            {
                var usuariosEmail = await _securityService.GetUsersWithRole(("Expediente.SuperUsuario").ToUpper());
                responsables = _context.Colaboradores
                                    .Where(c => usuariosEmail.Contains(c.CORREO))
                                    .ProjectTo<ResponsablePorPerfilDto>(_mapper.ConfigurationProvider)
                                    .ToList();
                responsables = responsables.Where(r => r.IDCOLABORADOR != request.IdColaborador && r.IDCOLABORADOR != request.IdColaboradorSolicitud).ToList();
            }
            // si es Financiero todos los Financiero
            else if (Rol.Contains("Expediente.Financiero"))
            {
                var usuariosEmail = await _securityService.GetUsersWithRole(("Expediente.Financiero").ToUpper());
                responsables = _context.Colaboradores
                                    .Where(c => usuariosEmail.Contains(c.CORREO))
                                    .ProjectTo<ResponsablePorPerfilDto>(_mapper.ConfigurationProvider)
                                    .ToList();
                responsables = responsables.Where(r => r.IDCOLABORADOR != request.IdColaborador && r.IDCOLABORADOR != request.IdColaboradorSolicitud).ToList();
            }

            // si es un DM  trae su dm superior
            else if (Rol.Contains("Expediente.DM"))
            {
                var colaboradorDM = _context.Colaboradores
                                      .Where(c => c.IDCOLABORADOR == request.IdColaborador)
                                  .FirstOrDefault();

                responsables = await _context.Colaboradores
                                       .Where(c => c.IDCOLABORADOR == colaboradorDM.IDDELIVERYMANAGER && c.IDCOLABORADOR != request.IdColaborador)
                                       .ProjectTo<ResponsablePorPerfilDto>(_mapper.ConfigurationProvider)
                                       .ToListAsync(cancellationToken);




            }

            // excepto los autores y el mismo responsable

            vm.Responsables = responsables;
            vm.Count = responsables.Count();

            return vm;
        }
    }
}
