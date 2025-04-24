using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("CURSOCOLABORADOR", Schema = "NE")]
    public class CursoColaborador : AuditableEntity
    {

        public CursoColaborador()
        {

        }
        [Key]
        public int IDCURSOCOLABORADOR { get; set; }
        public int IDCOLABORADOR { get; set; }
        public string NOMBRE { get; set; }
        public string NOMBREENTIDAD { get; set; }
        public decimal? CANTIDADHORAS { get; set; }
        public DateTime? FECHAFINAL { get; set; }
        public byte[] CERTIFICADO { get; set; }
        public string MIMETYPE { get; set; }



    }
}