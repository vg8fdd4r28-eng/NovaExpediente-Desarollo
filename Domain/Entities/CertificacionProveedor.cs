using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("CERTIFICACIONPROVEEDOR", Schema = "NE")]
    public class CertificacionProveedor : AuditableEntity
    {
        public CertificacionProveedor()
        {

        }
        [Key]
        public int IDCERTIFICACIONPROVEEDOR { get; set; }
        public int IDPROVEEDORCERTIFICACION { get; set; }
        public int IDCERTIFICACION { get; set; }



        [ForeignKey("IDPROVEEDORCERTIFICACION")]
        public virtual ProveedorCertificacion ProveedorCertificacion { get; set; }

        //[ForeignKey("IDCERTIFICACION")]
        //public virtual Certificacion Certificacion { get; set; }



    }
}
