using NovaExpediente.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
    [Table("CATALOGOVALOR", Schema = "NE")]
    public class CatalogoValor : AuditableEntity
    {
        public CatalogoValor() { }

        [Key]
        public int IDCATALOGOVALOR { get; set; }

        public string CODCATALOGO { get; set; }

        public string CODCATALOGOVALOR { get; set; }

        public string DETALLE { get; set; }

        public string DESCRIPCION { get; set; }

        public bool ISACTIVO { get; set; }

        public string REFCODCATALOGOVALOR { get; set; }

        public string VALOR1 { get; set; }

        public string VALOR2 { get; set; }

        public string VALOR3 { get; set; }

        public string VALOR4 { get; set; }

        [ForeignKey("IDUSUARIOCREA")]
        public virtual Colaborador ColaboradorCrea { get; set; }

        [ForeignKey("IDUSUARIOMODIFICA")]
        public virtual Colaborador ColaboradorModifica { get; set; }
    }
}
