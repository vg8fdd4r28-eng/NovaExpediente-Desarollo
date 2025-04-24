using NovaExpediente.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("CLIENTE", Schema = "NE")]
    public class Cliente : AuditableEntity
    {
        public Cliente()
        {

        }
        [Key]
        public int IDCLIENTE { get; set; }
        public int IDSECTOR { get; set; }
        public int IDPAIS { get; set; }
        public int IDEMPRESACORPORACION { get; set; }
        public string CODIGOCLIENTE { get; set; }
        public string CEDULAJURIDICA { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public string CORREO { get; set; }
        public string TELEFONO { get; set; }
        public bool ACTIVO { get; set; }
        public string TIPOCLIENTE { get; set; }
        public bool ESREFERENCIA { get; set; }

        [ForeignKey("IDSECTOR")]
        public virtual Sector Sector { get; set; }

        [ForeignKey("IDPAIS")]
        public virtual Pais Pais { get; set; }


        [ForeignKey("IDEMPRESACORPORACION")]
        public virtual EmpresaCorporacion EmpresaCorporacion { get; set; }


        public virtual ICollection<Referencia> Referencias { get; set; }


    }
}
