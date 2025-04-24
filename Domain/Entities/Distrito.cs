using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
	[Table("DISTRITO", Schema = "NE")]
	public class Distrito : AuditableEntity
	{
		public Distrito()
		{

		}
		[Key]
		public int IDDISTRITO { get; set; }
		public int IDCANTON { get; set; }
		public string NOMBRE { get; set; }



		[ForeignKey("IDCANTON")]
		public virtual Canton Canton { get; set; }

	}
}