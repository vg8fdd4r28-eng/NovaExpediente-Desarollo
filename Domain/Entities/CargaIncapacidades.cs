using NovaExpediente.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{

    [Table("CARGAINCAPACIDADES", Schema = "NE")]

    public class CargaIncapacidades : AuditableEntity
    {

        public CargaIncapacidades()
        {

        }
        [Key]
        public int IDCARGAINCAPACIDADES { get; set; }
        public string NOMBREARCHIVO { get; set; }
        public int CANTIDADREGISTROS { get; set; }
        public int CANTIDADINGRESADOS { get; set; }
        public string CONTENIDOARCHIVO { get; set; }
        public string RESULTADOCARGA { get; set; }
        public string DESCRIPCIONVALIDACION { get; set; }


    }
}
