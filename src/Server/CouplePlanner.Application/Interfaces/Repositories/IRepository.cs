using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CouplePlanner.Application.Interfaces.Repositories
{
  public interface IRepository<T>
  {
    IEnumerable<T> GetAll(Expression<Func<T, bool>> filter);

    Guid Add(T newEntity);

    T Get(Guid id);

    void Delete(Guid id);
  }
}
