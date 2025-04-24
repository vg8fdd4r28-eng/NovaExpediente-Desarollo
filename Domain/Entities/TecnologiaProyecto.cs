using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("TECNOLOGIAPROYECTO", Schema = "NE")]
    public class TecnologiaProyecto : AuditableEntity
    {
        public TecnologiaProyecto()
        {

        }
        [Key]
        public int IDTECNOLOGIAPROYECTO { get; set; }
        public int IDEXPERIENCIAPROYECTO { get; set; }
        public string NOMBRE { get; set; }







    }
}
