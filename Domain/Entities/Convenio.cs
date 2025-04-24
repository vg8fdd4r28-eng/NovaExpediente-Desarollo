using NovaExpediente.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("CONVENIO", Schema = "NE")]
    public class Convenio : AuditableEntity
    {

        public Convenio() { }

        [Key]
        public int IDCONVENIO { get; set; }
        public int IDCONVENIOCATEGORIA { get; set; }
        public int IDIDIOMA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string TELEFONO { get; set; }
        public string UBICACION { get; set; }
        public bool ACTIVO { get; set; }



        [ForeignKey("IDCONVENIOCATEGORIA")]
        public virtual ConvenioCategoria ConvenioCategoria { get; set; }
 
        [ForeignKey("IDIDIOMA")]
        public virtual Idioma Idioma { get; set; }







    }
}
