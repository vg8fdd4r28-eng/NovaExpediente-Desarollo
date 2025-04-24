using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("TIPOCORE", Schema = "NE")]
    public class TipoCore : AuditableEntity
    {
        public TipoCore()
        {

        }
        [Key]
        public int IDTIPOCORE { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }




    }
}
