using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("AFILIACIONCOLABORADOR", Schema = "NE")]
    public class AfiliacionColaborador : AuditableEntity
    {
        public AfiliacionColaborador()
        {

        }
        [Key]
        public int IDAFILIACIONCOLABORADOR { get; set; }
        public int IDCOLABORADOR { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public DateTime FECHAINICIO { get; set; }
        public string MIMETYPE { get; set; }
        public byte[] CERTIFICADO { get; set; }        

    }
}
