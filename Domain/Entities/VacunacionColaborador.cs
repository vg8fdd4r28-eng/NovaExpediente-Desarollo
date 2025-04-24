using System;
using NovaExpediente.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace NovaExpediente.Domain.Entities
{
    [Table("VACUNACIONCOLABORADOR", Schema = "NE")]
    public class VacunacionColaborador : AuditableEntity
    {
        public VacunacionColaborador()
        {

        }
        [Key]
        public int IdVacunacionColaborador {get; set;}
        
        public int IdColaborador {get; set;}
        
        public byte[] Certificado {get; set;}
        
        public string MimeType {get; set;}
        
        public char Estado {get; set;}
        
        public char? TipoDocumento {get; set;}
        
        [ForeignKey("IDVACUNACIONCOLABORADOR")]
        public virtual ICollection<VacunasColaborador> VacunasColaborador { get; set; }

    }
}
