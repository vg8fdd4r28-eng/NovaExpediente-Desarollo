using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("TECNOLOGIA", Schema = "NE")]
    public class Tecnologia : AuditableEntity
    {
        public Tecnologia()
        {

        }
        [Key]
        public int IDTECNOLOGIA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string VERSIONES { get; set; }

    }
}
