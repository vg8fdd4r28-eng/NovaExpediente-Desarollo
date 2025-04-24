using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("CLASIFICADOR", Schema = "NE")]
    public class Clasificador : AuditableEntity
    {
        public Clasificador()
        {

        }
        [Key]
        public int IdClasificador { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }


    }
}
