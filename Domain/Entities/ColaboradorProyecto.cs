using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("COLABORADORPROYECTO", Schema = "NE")]
    public class ColaboradorProyecto : AuditableEntity
    {
        public ColaboradorProyecto()
        {

        }
        [Key]
        public int IDCOLABORADORPROYECTO { get; set; }
        public int IDPROYECTO { get; set; }
        public int IDPUESTO { get; set; }
        public int IDCOLABORADOR { get; set; }
        public decimal TARIFADOLARES { get; set; }
        public int TOTALHORAS { get; set; }
        public DateTime FECHAINICIO { get; set; }
        public DateTime FECHAFINAL { get; set; }



        [ForeignKey("IDPROYECTO")]
        public virtual Proyecto Proyecto { get; set; }

        [ForeignKey("IDPUESTO")]
        public virtual Puesto Puesto { get; set; }

        [ForeignKey("IDCOLABORADOR")]
        public virtual Colaborador Colaborador { get; set; }




    }
}
