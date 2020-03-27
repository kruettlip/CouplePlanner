using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CouplePlanner.Application.Entities;
using CouplePlanner.Application.Interfaces.Repositories;
using CouplePlanner.Application.Interfaces.Services;

namespace CouplePlanner.Application.Services
{
  public class EventApplicationService : ApplicationService<Event, Domain.Entities.Event>,
    IHappeningApplicationService<Event, Domain.Entities.Event>
  {
    private IRepository<Domain.Entities.Event> Repository { get; }

    private IMapper Mapper { get; }

    public EventApplicationService(IRepository<Domain.Entities.Event> repository, IMapper mapper) : base(repository, mapper)
    {
      Repository = repository;
      Mapper = mapper;
    }

    public IEnumerable<Event> GetUpcoming(int take)
    {
      var entities = Repository.GetAll(e => e.StartDate >= DateTime.Today.ToUniversalTime()).Take(take);
      var mappedEntities = entities.Select(e => Mapper.Map<Event>(e));
      return mappedEntities;
    }
  }
}
