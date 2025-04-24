using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("VACACION", Schema = "NE")]
  public  class Vacacion : AuditableEntity
    {
        public Vacacion()
        {

        }
        [Key]
        public int IDVACACION { get; set; }
        public int IDCOLABORADOR { get; set; }
        public DateTime FECHAINICIO { get; set; }
        public DateTime FECHAFINAL { get; set; }
        public int CANTIDADDIAS { get; set; }

        public int? IDSOLICITUDMASTERDATA { get; set; }

        [ForeignKey("IDCOLABORADOR")]
        public virtual Colaborador Colaborador { get; set; }
        [ForeignKey("IDSOLICITUDMASTERDATA")]
        public virtual SolicitudMasterData SolicitudMasterData { get; set; }

    }

}
