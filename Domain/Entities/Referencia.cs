using NovaExpediente.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("REFERENCIA", Schema = "NE")]
    public class Referencia : AuditableEntity
    {
        public Referencia()
        {

        }
        [Key]
        public int IDREFERENCIA { get; set; }
        public int IDCLIENTE { get; set; }       
        public string IDENTIFICACION { get; set; }
        public string NOMBRE { get; set; }
        public string CORREO { get; set; }
        public string TELEFONO { get; set; }
        public string EXTENSION { get; set; }
        public string CELULAR { get; set; }
        public string PUESTO { get; set; }
        public bool ACTIVO { get; set; }




        [ForeignKey("IDCLIENTE")]
        public virtual Cliente Cliente { get; set; }

 




    }
}
