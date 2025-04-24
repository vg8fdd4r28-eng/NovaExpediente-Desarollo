using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("EXPERIENCIAAMBIENTEDESARROLLO", Schema = "NE")]
    public class ExperienciaAmbienteDesarrollo : AuditableEntity
    {
        public ExperienciaAmbienteDesarrollo()
        {

        }
        [Key]
        public int IDCOLABORADOR { get; set; }
        public int IDAMBIENTEDESARROLLO { get; set; }
        public int IDNIVELCONOCIMIENTO { get; set; }
        public int MESESEXPERIENCIA { get; set; }


        [ForeignKey("IDAMBIENTEDESARROLLO")]
        public virtual AmbienteDesarrollo AmbienteDesarrollo { get; set; }

        [ForeignKey("IDNIVELCONOCIMIENTO")]
        public virtual NivelConocimiento NivelConocimiento { get; set; }



    }
}
