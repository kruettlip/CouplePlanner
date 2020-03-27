using System;
using System.Collections.Generic;
using System.Linq;
using CouplePlanner.Application.Interfaces.Repositories;
using CouplePlanner.Domain.Entities;
using CouplePlanner.Infrastructure.Database.Interfaces;

namespace CouplePlanner.Infrastructure.Database.Repositories
{
	public class AbsenceRepository : IRepository<Absence>
	{
		private ICouplePlannerDbContext Db { get; }

		public AbsenceRepository(ICouplePlannerDbContext db)
		{
			Db = db;
		}

		public IEnumerable<Absence> GetAll(Func<Absence, bool> filter)
		{
			return Db.Absences.Where(filter);
		}

		public Guid Add(Absence newAbsence)
		{
			newAbsence.Id = Guid.NewGuid();
			var id = Db.Absences.Add(newAbsence).Entity.Id;
			Db.SaveChanges();
			return id;
		}

		public Absence Get(Guid id)
		{
			return Db.Absences.First(e => e.Id == id);
		}

		public void Delete(Guid id)
		{
			var entity = Get(id);
			Db.Absences.Remove(entity);
			Db.SaveChanges();
		}
	}
}
