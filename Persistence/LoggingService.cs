using NovaExpediente.Application.Common.Interfaces;
using NovaExpediente.Application.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Persistence
{
    /// <summary>
    /// Se encarga de guardar en una tabla la bitácora de alguna
    /// de las acciones que se ejecutan en el sistema
    /// </summary>
    public class LoggingService : ILoggingService
    {
        /// <summary>
        /// Tipo de bitácora
        /// </summary>
        enum EnumLogType
        {
            Error,
            Information
        }

        // servicio que se se encarga de ejecutar el 
        // procedimiento almacenado que guarda en la tabla 
        // de bitácora
        IFireForgetCommandHandler _fireForgetCommand;

        public LoggingService(IFireForgetCommandHandler fireForgetCommand)
        {
            _fireForgetCommand = fireForgetCommand;
        }

        /// <summary>
        /// Envía una petición de generar una entrada en la bitácora por un error detectado
        /// </summary>
        /// <param name="type">Categoría de la acción</param>
        /// <param name="data">Datos asociados a la acción</param>
        /// <param name="id">Identificador de la solicitud asociada a la acción</param>
        public void LogError(EnumLoggingCategory type, string data, int? id)
        {
            this.Log(EnumLogType.Error, type, data, id);
        }

        /// <summary>
        /// Envía una petición de generar una entrada en la bitácora correspondiente a información
        /// </summary>
        /// <param name="type">Categoría de la acción</param>
        /// <param name="data">Datos asociados a la acción</param>
        /// <param name="id">Identificador de la solicitud asociada a la acción</param>
        public void LogInformation(EnumLoggingCategory type, string data, int? id)
        {
            this.Log(EnumLogType.Information, type, data, id);
        }

        /// <summary>
        /// Envia una petición para guardar en base de datos una entrada en la bitácora
        /// </summary>
        /// <param name="logType">Tipo de bitácora</param>
        /// <param name="type">Categoría de la acción</param>
        /// <param name="data">Datos asociados a la acción</param>
        /// <param name="id">Identificador de la solicitud asociada a la acción</param>
        private void Log(EnumLogType logType, EnumLoggingCategory type, string data, int? id) 
        {
            
            _fireForgetCommand.Execute(async db =>
            {
                await db.PersistirLog(logType.ToString(), type.ToString(), data, id);
            });
        }
    }
}
