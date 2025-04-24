using NovaExpediente.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("EVENTO", Schema = "NE")]
    public class Evento : AuditableEntity
    {
        public Evento() { }

        [Key]
        public int IDEVENTO { get; set; }
        public int IDIDIOMA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public DateTime FECHA { get; set; }
        public string ENLACE { get; set; }
        public bool ACTIVO { get; set; }
        public string LAT { get; set; }
        public string LNG { get; set; }

        [ForeignKey("IDIDIOMA")]
        public virtual Idioma Idioma { get; set; }

         public virtual ICollection<ImagenEvento> Imagenes { get; set; }
         public virtual ICollection<EventoSeccion> EventoSecciones { get; set; }
    }
}
