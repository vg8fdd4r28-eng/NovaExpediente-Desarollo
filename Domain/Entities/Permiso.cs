using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("PERMISOTRABAJO", Schema = "NE")]
    public class Permiso : AuditableEntity
    {
        public Permiso()
        {

        }
        [Key]
        public int IDPERMISOTRABAJO { get; set; }
        public int IDTIPOPERMISOGOCE { get; set; }
        public int IDCOLABORADOR { get; set; }

        public DateTime FECHAINICIO { get; set; }
        public DateTime FECHAFINAL { get; set; }
        public int CANTIDADDIAS { get; set; }
        public string DESCRIPCION { get; set; }
        public string TIPOPERMISO { get; set; }
        public int? IDSOLICITUDMASTERDATA { get; set; }


        [ForeignKey("IDCOLABORADOR")]
        public virtual Colaborador Colaborador { get; set; }

        [ForeignKey("IDTIPOPERMISOGOCE")]
        public virtual TipoPermisoGoce TipoPermisoGoce { get; set; }
        [ForeignKey("IDSOLICITUDMASTERDATA")]
        public virtual SolicitudMasterData SolicitudMasterData { get; set; }

    }

}
