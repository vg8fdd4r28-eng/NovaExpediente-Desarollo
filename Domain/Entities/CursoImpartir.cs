using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("CURSOIMPARTIR", Schema = "NE")]
    public class CursoImpartir : AuditableEntity
    {

        public CursoImpartir()
        {

        }
        [Key]
        public int IDCURSOIMPARTIR { get; set; }
        public int IDCOLABORADOR { get; set; }
        public int IDCURSO { get; set; }
        public string DESCRIPCION { get; set; }

        [ForeignKey("IDCURSO")]
        public virtual Curso Curso { get; set; }


    }
}