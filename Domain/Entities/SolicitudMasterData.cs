using NovaExpediente.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("SOLICITUDMASTERDATA", Schema = "NE")]
    public class SolicitudMasterData : AuditableEntity
    {
        public SolicitudMasterData()
        {

        }
        [Key]
        public int IDSOLICITUDMASTERDATA { get; set; }
        public int IDTIPOSOLICITUD { get; set; }
        public int? IDCOLABORADOR { get; set; }
        public int IDUSUARIORESPONSABLE { get; set; }
        
        public string ESTADOSOLICITUD { get; set; }
        public string DOCSOLICITUD { get; set; }
        public string RESULTADOACTUALIZA { get; set; }

        [ForeignKey("IDUSUARIOCREA")]
        public virtual Colaborador Autor { get; set; }
        [ForeignKey("IDTIPOSOLICITUD")]
        public virtual TipoSolicitud TipoSolicitud { get; set; }
        [ForeignKey("IDCOLABORADOR")]
        public virtual Colaborador Colaborador { get; set; }
        [ForeignKey("IDUSUARIORESPONSABLE")]
        public virtual Colaborador Responsable { get; set; }
        [ForeignKey("IDSOLICITUDMASTERDATA")]
        public virtual ICollection<SolicitudComentario> Comentarios { get; set; }
    }
}
