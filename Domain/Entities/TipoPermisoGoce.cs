using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("TIPOPERMISOGOCE", Schema = "NE")]
    public class TipoPermisoGoce : AuditableEntity
    {
        public TipoPermisoGoce()
        {

        }
        [Key]
        public int IDTIPOPERMISOGOCE { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public bool ESTADO { get; set; }

    }

}