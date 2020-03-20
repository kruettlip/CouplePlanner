using System;
using System.Collections.Generic;
using CouplePlanner.Application.Entities;

namespace CouplePlanner.Application.Interfaces.Services
{
  public interface IEventApplicationService
  {
    IEnumerable<Event> GetAll();

    Guid Create(Event newEvent);

    void Delete(Guid id);
  }
}
