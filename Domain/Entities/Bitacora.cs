using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NovaExpediente.Domain.Entities
{
    [Table("BITACORA", Schema = "NE")]
    public class Bitacora
    {
        [Key]
        public int IDBITACORA { get; set; }
        public string CORREO { get; set; }
        public string NOMBREUSUARIO { get; set; }
        public string OPCION { get; set; }
        public string NOMBREOPCION { get; set; }
        public string PARAMETROS { get; set; }
        public DateTime FECHAVISITA { get; set; }
    }
}
