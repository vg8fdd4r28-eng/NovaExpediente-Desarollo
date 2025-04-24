using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("AMBIENTEDESARROLLO", Schema = "NE")]
    public class AmbienteDesarrollo : AuditableEntity
    {
        public AmbienteDesarrollo()
        {

        }
        [Key]
        public int IDAMBIENTEDESARROLLO { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string VERSIONES { get; set; }

    }
}
