using CouplePlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CouplePlanner.Infrastructure.Database.Interfaces
{
	public interface ICouplePlannerDbContext
	{
		DbSet<Event> Events { get; set; }

		DbSet<Absence> Absences { get; set; }

		int SaveChanges();
	}
}
