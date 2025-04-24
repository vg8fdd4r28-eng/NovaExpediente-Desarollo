using System;
using NovaExpediente.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("VACUNASCOLABORADOR", Schema = "NE")]
    public class VacunasColaborador : AuditableEntity
    {
        public VacunasColaborador()
        {

        }
        [Key]
        public int IdVacunasColaborador {get; set;}
        
        public int IdVacunacionColaborador {get; set;}
        
        public int IdVacuna {get; set;}
        
        public DateTime Fecha {get; set;}

        [ForeignKey("IDVACUNA")]
        public virtual Vacuna Vacuna { get; set; }

    }
}
