using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NovaExpediente.Domain.Entities
{
    [Table("LENGUAJEPROGRAMACION", Schema = "NE")]
    public class LenguajeProgramacion : AuditableEntity
    {
        public LenguajeProgramacion()
        {

        }
        [Key]
        public int IDLENGUAJEPROGRAMACION { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string VERSIONES { get; set; }

    }
}
