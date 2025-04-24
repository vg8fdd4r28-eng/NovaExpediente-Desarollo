using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
	[Table("CANTON", Schema = "NE")]
	public class Canton : AuditableEntity
	{
		public Canton()
		{

		}
		[Key]
		public int IDCANTON { get; set; }

		public int IDPROVINCIA { get; set; }

		public string NOMBRE { get; set; }



		[ForeignKey("IDPROVINCIA")]
		public virtual Provincia Provincia { get; set; }

	}
}