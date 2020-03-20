using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CouplePlanner.Application.Entities;
using CouplePlanner.Application.Interfaces.Repositories;
using CouplePlanner.Application.Interfaces.Services;

namespace CouplePlanner.Application.Services
{
  public class EventApplicationService : IEventApplicationService
  {
    private IEventRepository Repository { get; }

    private IMapper Mapper { get; }

    public EventApplicationService(IEventRepository repository, IMapper mapper)
    {
      Repository = repository;
      Mapper = mapper;
    }

    public IEnumerable<Event> GetAll()
    {
      var events = Repository.GetAll(e => true);
      var mappedEvents = events.Select(e => Mapper.Map<Event>(e));
      return mappedEvents;
    }

    public Guid Create(Event newEvent)
    {
      var mappedEvent = Mapper.Map<Domain.Entities.Event>(newEvent);
      var id = Repository.Add(mappedEvent);
      return id;
    }

    public void Delete(Guid id)
    {
      Repository.Delete(id);
    }
  }
}
