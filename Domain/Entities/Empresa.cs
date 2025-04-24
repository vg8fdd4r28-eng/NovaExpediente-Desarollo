using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("EMPRESA", Schema = "NE")]
    public class Empresa : AuditableEntity
    {
        public Empresa()
        {

        }
        [Key]
        public int IDEMPRESA { get; set; }
        public string NOMBRE { get; set; }
        public string CEDULAJURIDICA { get; set; }
        public string DESCRIPCION { get; set; }





    }
}
