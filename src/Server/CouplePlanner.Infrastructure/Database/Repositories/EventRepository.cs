using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using CouplePlanner.Application.Interfaces.Repositories;
using CouplePlanner.Domain.Entities;
using CouplePlanner.Infrastructure.Database.Interfaces;

namespace CouplePlanner.Infrastructure.Database.Repositories
{
  public class EventRepository : IRepository<Event>
  {
    private ICouplePlannerDbContext Db { get; }

    public EventRepository(ICouplePlannerDbContext db)
    {
      Db = db;
    }

    public IEnumerable<Event> GetAll(Expression<Func<Event, bool>> filter)
    {
      return Db.Events.Where(filter);
    }

    public Guid Add(Event newEvent)
    {
      newEvent.Id = Guid.NewGuid();
      var id = Db.Events.Add(newEvent).Entity.Id;
      Db.SaveChanges();
      return id;
    }

    public Event Get(Guid id)
    {
      return Db.Events.First(e => e.Id == id);
    }

    public void Delete(Guid id)
    {
      var entity = Get(id);
      Db.Events.Remove(entity);
      Db.SaveChanges();
    }
  }
}
