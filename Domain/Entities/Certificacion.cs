using NovaExpediente.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NovaExpediente.Domain.Entities
{
    [Table("CERTIFICACION", Schema = "NE")]
    public class Certificacion : AuditableEntity
    {
        public Certificacion()
        {

        }
        [Key]
        public int IDCERTIFICACION { get; set; }
        public string CODIGOCERTIFICACION { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }


        [ForeignKey("IDCERTIFICACION")]
        public virtual ICollection<CertificacionProveedor>CertificacionProveedores { get; set; }

    }
}

