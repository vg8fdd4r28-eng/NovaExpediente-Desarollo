using NovaExpediente.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace NovaExpediente.Domain.Entities
{
	[Table("PUESTO", Schema = "NE")]
	public class Puesto : AuditableEntity
	{
		public Puesto()
		{

		}
		[Key]
		public int IDPUESTO { get; set; }

		public string NOMBRE { get; set; }

		public string DESCRIPCION { get; set; }
		public bool ES_DM { get; set; }

	}
}