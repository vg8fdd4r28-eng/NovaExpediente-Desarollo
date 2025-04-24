using NovaExpediente.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace NovaExpediente.Domain.Entities
{
	[Table("SECTOR", Schema = "NE")]
	public class Sector : AuditableEntity
	{
		public Sector()
		{

		}
		[Key]
		public int IdSector { get; set; }

		public string Nombre { get; set; }

		public string Descripcion { get; set; }


	}
}
