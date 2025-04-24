using NovaExpediente.Domain.Common;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NovaExpediente.Domain.Entities
{
	[Table("PAIS", Schema = "NE")]
	public class Pais : AuditableEntity
	{
		public Pais()
		{

		}
		[Key]
		public int IDPAIS { get; set; }

		public string CODPAIS { get; set; }

		public string NOMBRE { get; set; }

	}
}