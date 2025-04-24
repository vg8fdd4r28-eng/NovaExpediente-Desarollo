using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("NOTICIA", Schema = "NE")]
    public class Noticia : AuditableEntity
    {
        public Noticia() { }

        [Key]
        public int IDNOTICIA { get; set; }
        public int IDNOTICIACATEGORIA { get; set; }
        public int IDIDIOMA { get; set; }
        public DateTime FECHAVENCIMIENTO { get; set; }
        public string TITULO { get; set; }
        public string CONTENIDO { get; set; }
        public bool ACTIVO { get; set; }
        public string CORREOAMOSTRAR { get; set; }
        public string NOMBREAMOSTRAR { get; set; }
        public bool ESPUBLICA { get; set; }

        [ForeignKey("IDNOTICIACATEGORIA")]
        public virtual NoticiaCategoria NoticiaCategoria { get; set; }
        [ForeignKey("IDUSUARIOCREA")]
        public virtual Colaborador Autor { get; set; }
        [ForeignKey("IDIDIOMA")]
        public virtual Idioma Idioma { get; set; }
    }
}
