using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NovaExpediente.Domain.Entities
{
    [Table("NIVELCONOCIMIENTO", Schema = "NE")]
    public class NivelConocimiento : AuditableEntity
    {
        public NivelConocimiento()
        {

        }
        [Key]
        public int IDNIVELCONOCIMIENTO { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }

    }
}
