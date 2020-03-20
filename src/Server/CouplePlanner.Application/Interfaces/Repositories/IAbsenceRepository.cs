using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using CouplePlanner.Domain.Entities;

namespace CouplePlanner.Application.Interfaces.Repositories
{
  public interface IAbsenceRepository
  {
    IEnumerable<Absence> GetAll(Expression<Func<Absence, bool>> filter);

    Guid Add(Absence newAbsence);

    Absence Get(Guid id);

    void Delete(Guid id);
  }
}
