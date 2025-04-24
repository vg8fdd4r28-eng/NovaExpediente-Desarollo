using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("HABILIDADCOLABORADOR", Schema = "NE")]
    public class HabilidadColaborador : AuditableEntity
    {
        public HabilidadColaborador()
        {

        }
        [Key]
        public int IDHABILIDADCOLABORADOR { get; set; }
        public int IDHABILIDADBLANDA { get; set; }
        public int IDCOLABORADOR { get; set; }
        public char NIVELHABILIDAD { get; set; }

        [ForeignKey("IDHABILIDADBLANDA")]
        public virtual HabilidadBlanda HabilidadBlanda { get; set; }




    }
}
