using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("ERPCORE", Schema = "NE")]
    public class ErpCore : AuditableEntity
    {
        public ErpCore()
        {

        }
        [Key]
        public int IDERPCORE { get; set; }
        public int IDTIPOCORE { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public bool ESOPENSOURCE { get; set; }
        public string TECNOLOGIA { get; set; }

        [ForeignKey("IDTIPOCORE")]
        public virtual TipoCore TipoCore { get; set; }


    }
}
