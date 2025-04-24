using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using CloudinaryDotNet.Actions;
using DocumentFormat.OpenXml.InkML;
using Microsoft.AspNetCore.Http;
using NovaExpediente.Application.Common.Interfaces;
using MediatR;
using System.Threading.Tasks;
using NovaExpediente.Application.Colaboradores.Queries.GetColaboradorEsNovaShore;

namespace NovaExpediente.WebUI.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        IRolesPermissions _permissions;
        IHttpContextAccessor _httpContextAccessor;
        IMediator _mediator;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor, IRolesPermissions permissions, IMediator mediator)
        {
            int id;
            if (int.TryParse(httpContextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier), out id))
            {
                UserId = id;
            }
            else
                UserId = 1;

            IsAuthenticated = UserId != null;

            _mediator = mediator;
            _permissions = permissions;
            _httpContextAccessor = httpContextAccessor;
        }

        public List<string> GetRoles()
        {
            List<string> roles = new List<string>();

            foreach (var identity in _httpContextAccessor.HttpContext?.User?.Identities)
            {
                foreach (var claim in identity.Claims)
                {
                    if (claim.Type == "role")
                    {
                        roles.Add(claim.Value);
                    }
                }
            }
            if (roles.Count == 0 || !roles.Contains(_permissions.GetDefaultRole()))
            {
                roles.Add(_permissions.GetDefaultRole());
            }

            return roles.OrderBy(p => p).ToList();
        }

        public async Task<List<string>> GetPermissions()
        {
            List<string> roles = this.GetRoles();
            List<string> permissions = new List<string>();
            //var identity = _httpContextAccessor.HttpContext?.User?.Identity as ClaimsIdentity;
            //var userName = identity.FindFirst("name")?.Value;

            //bool EsNovaShore = await _mediator.Send(new GetColaboradorEsNovaShoreQuery { Email = userName });
            //if (!EsNovaShore)
            //{
            //    permissions.Add("AccederMinimoPermiso");
            //}

            foreach (var role in roles)
            {
                permissions.AddRange(_permissions.GetRolePermissions(role));
            }

            return permissions.Distinct().OrderBy(p => p).ToList(); ;
        }

        public int UserId { get; }
        public bool IsAuthenticated { get; }
    }
}
