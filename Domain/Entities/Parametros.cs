using NovaExpediente.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Domain.Entities
{

    [Table("PARAMETROS", Schema = "NE")]
    public class Parametros : AuditableEntity
    {
        public Parametros()
        {

        }
        [Key]
        public int IDPARAMETROS { get; set; }

        public Int16 INDICADOR_MASTERDATA_VERDE { get; set; }
        public Int16 INDICADOR_MASTERDATA_AMARILLO { get; set; }
        public Int16 INDICADOR_MASTERDATA_ROJO { get; set; }
        public Int16 INDICADOR_EXPEDIENTE_VERDE { get; set; }
        public Int16 INDICADOR_EXPEDIENTE_AMARILLO { get; set; }
        public Int16 INDICADOR_EXPEDIENTE_ROJO { get; set; }

    } 
}