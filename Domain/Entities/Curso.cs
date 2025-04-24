using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("CURSO", Schema = "NE")]
   public class Curso : AuditableEntity
    {

        public Curso()
        {

        }
        [Key]
        public int IDCURSO { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }


    }
}
