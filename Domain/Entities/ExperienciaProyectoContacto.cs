using NovaExpediente.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NovaExpediente.Domain.Entities
{
    [Table("EXPERIENCIAPROYECTOCONTACTO", Schema = "NE")]
    public class ExperienciaProyectoContacto : AuditableEntity
    {
        public ExperienciaProyectoContacto()
        {

        }
        [Key]
        public int IDEXPERIENCIAPROYECTOCONTACTO { get; set; }
        public int IDEXPERIENCIAPROYECTO { get; set; }
        public string CONTACTO { get; set; }
        public string CORREO { get; set; }
        public string TELEFONO { get; set; }


    }
}
