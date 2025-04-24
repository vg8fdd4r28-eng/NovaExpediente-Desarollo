using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.Log.Commands.SaveLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NovaExpediente.WebUI.Controllers
{
    public class LogController : BaseController
    {
        ICurrentUserService _currentUserService;
        ILogger<LogController> _logger;
        public LogController(ICurrentUserService currentUserService, ILogger<LogController> logger)
        {
            _currentUserService = currentUserService;
            _logger = logger;
        }
       

        [HttpPost]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesDefaultResponseType]
        public async Task<IActionResult> SaveUserLog(SaveLogCommand command)
        {
            var id = await Mediator.Send(command);
            

            _logger.LogInformation($"Log: '{command.Email}'\t'{command.Opcion}'\t'{command.Params}'\t'{command.NombreOpcion}'\t'{command.SubjectId}'\t'{DateTime.Now}' ");

            return Ok(id);
        }
    }
}
