using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("AREAORGANIZATIVA", Schema = "NE")]
    public class AreaOrganizativa : AuditableEntity
    {
        public AreaOrganizativa()
        {

        }
        [Key]
        public int IDAREAORGANIZATIVA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }





    }
}
