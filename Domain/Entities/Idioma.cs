using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NovaExpediente.Domain.Entities
{

    [Table("IDIOMA", Schema = "NE")]
    public class Idioma : AuditableEntity
    {
        public Idioma()
        {

        }
        [Key]
        public int IDIDIOMA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }


    }
}
