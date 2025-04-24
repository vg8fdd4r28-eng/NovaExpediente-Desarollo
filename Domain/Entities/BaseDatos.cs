using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NovaExpediente.Domain.Entities
{
    [Table("BASEDATOS", Schema = "NE")]
    public class BaseDatos : AuditableEntity
    {
        public BaseDatos()
        {

        }
        [Key]
        public int IDBASEDATOS { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string VERSIONES { get; set; }


    }
}
