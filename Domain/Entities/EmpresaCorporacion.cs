using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("EMPRESACORPORACION", Schema = "NE")]
    public class EmpresaCorporacion : AuditableEntity
    {
        public EmpresaCorporacion()
        {

        }
        [Key]
        public int IDEMPRESACORPORACION { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }





    }
}
