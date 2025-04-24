using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("formacionacademica", Schema = "NE")]
   public class FormacionAcademica : AuditableEntity
    {

        public FormacionAcademica()
        {

        }
        [Key]
        public int IDFORMACIONACADEMICA { get; set; }
        public int IDCENTROESTUDIO { get; set; }
        public int IDGRADOACADEMICO { get; set; }
        public int IDCOLABORADOR { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public int ANIO { get; set; }
        public byte[] CERTIFICADO { get; set; }
        public string MIMETYPE { get; set; }


        [ForeignKey("IDGRADOACADEMICO")]
        public virtual GradoAcademico GradoAcademico { get; set; }
        [ForeignKey("IDCENTROESTUDIO")]
        public virtual CentroEstudio CentroEstudio { get; set; }

    }
}
