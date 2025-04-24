using NovaExpediente.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NovaExpediente.Domain.Entities
{
    [Table("gradoacademico", Schema = "NE")]
    public class GradoAcademico : AuditableEntity
    {
        public GradoAcademico()
        {
           
        }
        [Key]
        public int IdGradoAcademico { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int Orden { get; set; }

    }
}
