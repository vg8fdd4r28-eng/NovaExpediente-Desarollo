using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace NovaExpediente.Domain.Entities
{


    [Table("IDIOMACOLABORADOR", Schema = "NE")]

   public class IdiomaColaborador : AuditableEntity
    {
        public IdiomaColaborador()
        {

        }
        [Key]
        public int IDIDIOMA { get; set; }
        public int IDCOLABORADOR { get; set; }
        public int IDNIVELIDIOMA { get; set; }
        public byte[] CERTIFICADO { get; set; }
        public string MIMETYPE { get; set; }



        [ForeignKey("IDIDIOMA")]
        public virtual Idioma Idioma { get; set; }

        [ForeignKey("IDNIVELIDIOMA")]
        public virtual NivelIdioma NivelIdioma { get; set; }

        

    }
}
