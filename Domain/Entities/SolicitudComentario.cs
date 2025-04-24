using NovaExpediente.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Domain.Entities
{
    [Table("SOLICITUDCOMENTARIO", Schema = "NE")]
    public class SolicitudComentario : AuditableEntity
    {
        public SolicitudComentario()
        {

        }

        [Key]
        public int IDSOLICITUDCOMENTARIO { get; set; }

        public int IDSOLICITUDMASTERDATA { get; set; }
        public string COMENTARIO { get; set; }

        public string ACCION { get; set; }

        [ForeignKey("IDUSUARIOCREA")]
        public virtual Colaborador Responsable { get; set; }

        
    }
}
