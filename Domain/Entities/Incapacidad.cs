using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("INCAPACIDAD", Schema = "NE")]
  public  class Incapacidad : AuditableEntity
    {
        public Incapacidad()
        {

        }
        [Key]
        public int IDINCAPACIDAD { get; set; }
        public int IDCOLABORADOR { get; set; }
        public int? IDCARGAINCAPACIDADES { get; set; }
        public int? IDSOLICITUDMASTERDATA { get; set; }
        
        public DateTime FECHAINICIO { get; set; }
        public DateTime FECHAFINAL { get; set; }
        public string NUMBOLETA { get; set; }
        public int CANTIDADDIAS { get; set; }
        public byte[] BOLETA { get; set; }
        public string MIMETYPE { get; set; }

        public char? INDTIPOREGISTRO { get; set; }
        public char TIPOINCAPACIDAD { get; set; }


        [ForeignKey("IDCOLABORADOR")]
        public virtual Colaborador Colaborador { get; set; }
        [ForeignKey("IDSOLICITUDMASTERDATA")]
        public virtual SolicitudMasterData SolicitudMasterData { get; set; }

    }

}
