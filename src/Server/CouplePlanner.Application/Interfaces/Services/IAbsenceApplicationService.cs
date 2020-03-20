using System;
using System.Collections.Generic;
using CouplePlanner.Application.Entities;

namespace CouplePlanner.Application.Interfaces.Services
{
  public interface IAbsenceApplicationService
  {
    IEnumerable<Absence> GetAll();

    Guid Create(Absence newAbsence);

    void Delete(Guid id);
  }
}
