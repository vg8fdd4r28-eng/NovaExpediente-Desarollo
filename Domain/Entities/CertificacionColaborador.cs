using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NovaExpediente.Domain.Entities
{
    [Table("CERTIFICACIONCOLABORADOR", Schema = "NE")]
    public class CertificacionColaborador : AuditableEntity
    {
        public CertificacionColaborador()
        {

        }
        [Key]
        public int IDCERTIFICACIONCOLABORADOR { get; set; }
        public int IDCOLABORADOR { get; set; }
        public int IDCERTIFICACIONPROVEEDOR { get; set; }
        public int IDCERTIFICACION { get; set; }
        public string DESCRIPCION { get; set; }
        public int ANIO { get; set; }
        public DateTime? VENCE { get; set; }
       // public Certificacion CertificacionNombre { get; set; }
        public string MIMETYPE { get; set; }
        public byte[] CERTIFICADO { get; set; }


        [ForeignKey("IDCERTIFICACION")]
        public virtual Certificacion Certificacion { get; set; }
        [ForeignKey("IDCERTIFICACIONPROVEEDOR")]
        public virtual CertificacionProveedor CertificacionProveedor { get; set; }
    }
}

