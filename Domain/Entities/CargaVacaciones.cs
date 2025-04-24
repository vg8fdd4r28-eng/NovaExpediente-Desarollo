using NovaExpediente.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("CARGAVACACIONES", Schema = "NE")]
    public class CargaVacaciones : AuditableEntity
    {

        public CargaVacaciones()
        {

        }
        [Key]
        public int IDCARGAVACACIONES { get; set; }
        public string NOMBREARCHIVO { get; set; }
        public int CANTIDADREGISTROS { get; set; }
        public int CANTIDADINGRESADOS { get; set; }
        public string CONTENIDOARCHIVO { get; set; }
        public string RESULTADOCARGA { get; set; }
        public string DESCRIPCIONVALIDACION { get; set; }


    }
}
