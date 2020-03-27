using System;
using CouplePlanner.Domain.Common;

namespace CouplePlanner.Domain.Entities
{
	public class Event : AuditableEntity
	{
		public Guid Id { get; set; }

		public DateTime StartDate { get; set; }

		public DateTime EndDate { get; set; }

		public string Location { get; set; }

		public string Travel { get; set; }
	}
}
