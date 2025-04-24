using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("CURRICULUMVITAE", Schema = "NE")]
    public class CurriculumVitae : AuditableEntity
    {
        public CurriculumVitae() { }

        [Key]
        public int IDCURRICULUMVITAE { get; set; }
        public int IDCOLABORADOR { get; set; }
        public string NOMBREDOCUMENTO { get; set; }
        public byte[] DOCUMENTO {  get; set; }
        public string MIMETYPE {  get; set; }
        public string MENSAJE { get; set; }
        public string ESTADO { get; set; }
        public string UUID { get; set; }
    }

}
