using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovaExpediente.Domain.Common
{
    /// <summary>
    /// Clase base para entidades que requieren auditoría automática.
    /// Todas las entidades que hereden de esta clase tendrán tracking de creación y modificación.
    /// </summary>
    public class AuditableEntity
    {
        /// <summary>
        /// ID del usuario que creó el registro
        /// </summary>
        public int IDUSUARIOCREA { get; set; }

        /// <summary>
        /// Fecha y hora de creación del registro
        /// </summary>
        public DateTime FECHACREACION { get; set; }

        /// <summary>
        /// ID del usuario que modificó el registro por última vez (nullable)
        /// </summary>
        public int? IDUSUARIOMODIFICA { get; set; }

        /// <summary>
        /// Fecha y hora de la última modificación (nullable)
        /// </summary>
        public DateTime? FECHAMODIFICACION { get; set; }

        /// <summary>
        /// Timestamp para control de concurrencia optimista.
        /// SQL Server actualiza automáticamente este campo en cada UPDATE.
        /// </summary>
        [Timestamp]
        public byte[] CAMBIO { get; set; }

        // Campos comentados para versionado temporal (no implementado actualmente)
        //public DateTime INICIOVERSION { get; set; }
        //public DateTime FINVERSION { get; set; }
    }
}
