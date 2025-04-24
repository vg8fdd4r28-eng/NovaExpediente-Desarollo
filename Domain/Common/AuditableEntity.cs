using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NovaExpediente.Domain.Common
{
    public class AuditableEntity
    {
        public int IDUSUARIOCREA { get; set; }

        public DateTime FECHACREACION { get; set; }

        public int? IDUSUARIOMODIFICA { get; set; }

        public DateTime? FECHAMODIFICACION { get; set; }

        [Timestamp]
        public byte[] CAMBIO { get; set; }

        //public DateTime INICIOVERSION { get; set; }

        //public DateTime FINVERSION { get; set; }
    }
}
