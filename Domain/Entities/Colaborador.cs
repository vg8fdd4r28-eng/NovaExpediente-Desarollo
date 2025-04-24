using NovaExpediente.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("colaborador", Schema = "NE")]
    public class Colaborador : AuditableEntity
    {
        public Colaborador()
        {

        }
        [Key]
        public int IDCOLABORADOR { get; set; }
        public int IDPUESTO { get; set; }
        public int IDPAISORIGEN { get; set; }
        public int IDPAISRESIDENCIA { get; set; }
        public int IDPAISFERIADO { get; set; }

        public int? IDCANTON { get; set; }
        public int? IDDELIVERYMANAGER { get; set; }
        public int? IDEMPRESACORPORACION { get; set; }

        public string IDENTIFICACION { get; set; }
        public string NOMBRE { get; set; }
        public string PRIMERAPELLIDO { get; set; }
        public string SEGUNDOAPELLIDO { get; set; }
        public DateTime FECHANACIMIENTO { get; set; }
        public char GENERO { get; set; }
        public DateTime FECHAINGRESO { get; set; }
        public string DIRECCION { get; set; }
        public string CIUDADRESIDENCIA { get; set; }
        public string CELULAR { get; set; }
        public string TELEFONO { get; set; }
        public string CORREO { get; set; }
        public string CORREOPERSONAL { get; set; }
        public string RESENAPERFIL { get; set; }
        public string NUMEROCPIC { get; set; }
        public string RAMAESPECIALIDAD { get; set; }
        public bool ACTIVO { get; set; }

        public byte[] FOTO { get; set; }
        public byte[] CERTIFICADOCPIC { get; set; }
        public char ESTADOCIVIL { get; set; }
        public string TELEFONOEMERGENCIA { get; set; }
        public string PERSONALLAMAR { get; set; }
        public int NUMEROHIJOS { get; set; }
        public int? IDAREAORGANIZATIVA { get; set; }

        public int? IDCLIENTE { get; set; }
        public string MIMETYPEFOTO { get; set; }
        public string MIMETYPECPIC { get; set; }

        public bool ESDELIVERYMANAGER { get; set; }


        public int? IDNOMINA { get; set; }

        public bool SOLICITAVACACIONES { get; set; }

        public int? VACACIONESDIASANUALES { get; set; }

        public decimal SALDOVACACIONES { get; set; }


        public decimal? DIASCORTE { get; set; }
        public int? VACACIONESDIASACUMULADO { get; set; }
        public decimal? VACACIONESADICIONALES { get; set; }
        public decimal SALDOVACACIONESACTUAL { get; set; }
        public string CODIGOPOSTAL { get; set; }
        public bool ESSERVICIOPROFESIONAL { get; set; }
        public bool ESNOVASHORE { get; set; }


        [ForeignKey("IDPUESTO")]
        public virtual Puesto Puesto { get; set; }

        [ForeignKey("IDPAISORIGEN")]
        public virtual Pais PaisOrigen { get; set; }

        [ForeignKey("IDPAISRESIDENCIA")]
        public virtual Pais PaisResidencia { get; set; }

        [ForeignKey("IDPAISFERIADO")]
        public virtual Pais PaisFeriado { get; set; }

        [ForeignKey("IDCANTON")]
        public virtual Canton Canton { get; set; }

        [ForeignKey("IDDELIVERYMANAGER")]
        public virtual Colaborador DeliveryManager { get; set; }

        [ForeignKey("IDAREAORGANIZATIVA")]
        public virtual AreaOrganizativa AreaOrganizativa { get; set; }


        [ForeignKey("IDCLIENTE")]
        public virtual Cliente Cliente { get; set; }

        [ForeignKey("IDEMPRESACORPORACION")]
        public virtual EmpresaCorporacion EmpresaCorporacion { get; set; }

        [ForeignKey("IDCOLABORADOR")]
        public virtual ICollection<IdiomaColaborador> IdiomasColaborador { get; set; }

        [ForeignKey("IDCOLABORADOR")]
        public virtual ICollection<CertificacionColaborador> CertificacionesColaborador { get; set; }

        [ForeignKey("IDCOLABORADOR")]
        public virtual ICollection<AfiliacionColaborador> AfiliacionesColaborador { get; set; }

        [ForeignKey("IDCOLABORADOR")]
        public virtual ICollection<VacunacionColaborador> VacunacionColaborador { get; set; }
    }
}
