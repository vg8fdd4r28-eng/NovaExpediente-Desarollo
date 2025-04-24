using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("CALIDAD", Schema = "NE")]
    public class Calidad : AuditableEntity
    {
        public Calidad()
        {

        }
        [Key]
        public int IDCALIDAD { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }





    }
}
