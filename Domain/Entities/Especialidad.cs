using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("ESPECIALIDAD", Schema = "NE")]
    public class Especialidad : AuditableEntity
    {
        public Especialidad()
        {

        }
        [Key]
        public int IDESPECIALIDAD { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }


    }
}
