using System;

namespace CouplePlanner.Application.Entities
{
	public class Absence
	{
		public Guid Id { get; set; }

		public DateTimeOffset StartDate { get; set; }

		public DateTimeOffset EndDate { get; set; }

		public string AbsenceReason { get; set; }
	}
}
