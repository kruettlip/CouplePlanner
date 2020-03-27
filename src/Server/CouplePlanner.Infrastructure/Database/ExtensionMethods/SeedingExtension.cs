using System;
using System.Collections.Generic;
using System.Linq;
using CouplePlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CouplePlanner.Infrastructure.Database.ExtensionMethods
{
	public static class SeedingExtension
	{
		public static void Seeding(this ModelBuilder modelBuilder)
		{
			var events = CreateEvents();
			var absences = CreateAbsences();

			modelBuilder.Entity<Event>().HasData(events.ToArray());
			modelBuilder.Entity<Absence>().HasData(absences.ToArray());
		}

		private static IEnumerable<Event> CreateEvents()
		{
			var events = new List<Event>
	  {
		new Event
		{
		  StartDate = DateTime.Now.AddDays(2),
		  EndDate = DateTime.Now.AddDays(3).AddHours(5),
		  Travel = "Zug",
		  Location = "bei Meli Zuhause"
		},
		new Event
		{
		  StartDate = DateTime.Now.AddDays(10),
		  EndDate = DateTime.Now.AddDays(10).AddHours(5),
		  Travel = "Auto",
		  Location = "bei Phippu Zuhause"
		},
		new Event
		{
		  StartDate = DateTime.Now.AddDays(19),
		  EndDate = DateTime.Now.AddDays(20).AddHours(-9),
		  Travel = "Auto",
		  Location = "bei Meli Zuhause"
		},
		new Event
		{
		  StartDate = DateTime.Now.AddDays(45),
		  EndDate = DateTime.Now.AddDays(45).AddHours(7),
		  Travel = "Zug",
		  Location = "bei Phippu Zuhause"
		}
	  };
			events.ForEach(e => e.Id = Guid.NewGuid());
			return events;
		}

		private static IEnumerable<Absence> CreateAbsences()
		{
			var absences = new List<Absence>
	  {
		new Absence
		{
		  StartDate = DateTime.Now.AddDays(5),
		  EndDate = DateTime.Now.AddDays(6).AddHours(5),
		  AbsenceReason = "Konzert"
		},
		new Absence
		{
		  StartDate = DateTime.Now.AddDays(12),
		  EndDate = DateTime.Now.AddDays(12).AddHours(5),
		  AbsenceReason = "Auslandsaufenthalt"
		},
		new Absence
		{
		  StartDate = DateTime.Now.AddDays(23),
		  EndDate = DateTime.Now.AddDays(24).AddHours(-9),
		  AbsenceReason = "Geburtstagsfest"
		},
		new Absence
		{
		  StartDate = DateTime.Now.AddDays(41),
		  EndDate = DateTime.Now.AddDays(41).AddHours(7),
		  AbsenceReason = "Ferien"
		}
	  };
			absences.ForEach(a => a.Id = Guid.NewGuid());
			return absences;
		}
	}
}
