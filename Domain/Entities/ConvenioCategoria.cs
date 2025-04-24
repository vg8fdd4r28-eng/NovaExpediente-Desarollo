using NovaExpediente.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("CONVENIOCATEGORIA", Schema = "NE")]
    public class ConvenioCategoria : AuditableEntity
    {

        public ConvenioCategoria() { }

        [Key]
        public int IDCONVENIOCATEGORIA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public bool ACTIVO { get; set; }
    }
}
