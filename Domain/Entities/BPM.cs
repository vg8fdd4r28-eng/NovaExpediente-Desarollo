using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("BPM", Schema = "NE")]
    public class BPM : AuditableEntity
    {
        public BPM()
        {

        }
        [Key]
        public int IDBPM { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }





    }
}
