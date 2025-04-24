using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("PROYECTO", Schema = "NE")]
    public class Proyecto : AuditableEntity
    {
        public Proyecto()
        {

        }
        [Key]
        public int IDPROYECTO { get; set; }
        public int IDCLIENTE { get; set; }
        public int IDSECTOR { get; set; }
        public int IDREFERENCIA { get; set; }
        public string CODPROYECTO { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string NUMEROCONTRATO { get; set; }
        public DateTime FECHAINICIO { get; set; }
        public DateTime FECHAFINAL { get; set; }
        public int CANTIDADHORAS { get; set; }
        public bool ACTIVO { get; set; }


         [ForeignKey("IDCLIENTE")]
            public virtual Cliente Cliente { get; set; }

 





    }
}
