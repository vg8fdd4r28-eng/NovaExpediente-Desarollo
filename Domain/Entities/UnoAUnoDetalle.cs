using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("UNOAUNODETALLE", Schema = "NE")]
    public class UnoAUnoDetalle : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IDUNOAUNODETALLE { get; set; }

        [Required]
        public int IDUNOAUNO { get; set; }

        [Required]
        public int IDUNOAUNOESTADO { get; set; }

        [Required]
        public int IDUNOAUNOCATEGORIA { get; set; }

        [Required]
        public string DETALLE { get; set; }

        [Required]
        public string PLANACCION { get; set; }

        public int? PORCENTAJEAVANCE { get; set; }

        public DateTime? FECHARESOLUCION { get; set; }
        public DateTime? FECHAREALRESOLUCION { get; set; }

        [Required]
        public bool ACTIVO { get; set; } = true;

        [ForeignKey("IDUNOAUNOESTADO")]
        public virtual UnoAUnoEstado UnoAUnoEstado { get; set; }

        [ForeignKey("IDUNOAUNOCATEGORIA")]
        public virtual UnoAUnoCategoria UnoAUnoCategoria { get; set; }

        [ForeignKey("IDUSUARIOCREA")]
        public virtual Colaborador UsuarioCrea { get; set; }

        [ForeignKey("IDUSUARIOMODIFICA")]
        public virtual Colaborador UsuarioModifica { get; set; }
    }
}
