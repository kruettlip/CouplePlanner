using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CouplePlanner.Domain.Entities;

namespace CouplePlanner.Application.Interfaces.Repositories
{
  public interface IEventRepository
  {
    IEnumerable<Event> GetAll(Expression<Func<Event, bool>> filter);

    Guid Add(Event newEvent);

    Event Get(Guid id);

    void Delete(Guid id);
  }
}
