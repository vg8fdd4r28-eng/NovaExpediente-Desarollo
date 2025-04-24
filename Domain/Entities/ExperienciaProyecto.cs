using NovaExpediente.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NovaExpediente.Domain.Entities
{
    [Table("EXPERIENCIAPROYECTO", Schema = "NE")]
    public class ExperienciaProyecto : AuditableEntity
    {
        public ExperienciaProyecto()
        {

        }
        [Key]
        public int IDEXPERIENCIAPROYECTO { get; set; }
        public int IDCOLABORADOR { get; set; }
        public string NOMBREPROYECTO { get; set; }
        public string DESCRIPCION { get; set; }
        public string NOMBRECLIENTE { get; set; }
        public DateTime? FECHAINICIO { get; set; }
        public DateTime? FECHAFINAL { get; set; }
        public string INICIO { get; set; }
        public string FIN { get; set; }
        public string ROLPROYECTO { get; set; }
        public string LICITACION { get; set; }

        public string NOMBREEMPRESA { get; set; }
        public int? IDEMPRESACORPORACION { get; set; }
        public int? IDCLIENTE { get; set; }




        [ForeignKey("IDCOLABORADOR")]
        public virtual Colaborador Colaborador { get; set; }

        [ForeignKey("IDEXPERIENCIAPROYECTO")]
        public virtual ICollection<TecnologiaProyecto> TecnologiaProyecto { get; set; }

        [ForeignKey("IDEXPERIENCIAPROYECTO")]
        public virtual ICollection<HerramientaProyecto> HerramientaProyecto { get; set; }

        [ForeignKey("IDEMPRESACORPORACION")]
        public virtual EmpresaCorporacion EmpresaCorporacion { get; set; }

        [ForeignKey("IDCLIENTE")]
        public virtual Cliente  Cliente { get; set; }

        [ForeignKey("IDEXPERIENCIAPROYECTO")]
        public virtual ICollection<ExperienciaProyectoContacto> Contactos { get; set; }
    }
}
