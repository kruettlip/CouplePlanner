using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CouplePlanner.Application.Entities;
using CouplePlanner.Application.Interfaces.Repositories;
using CouplePlanner.Application.Interfaces.Services;

namespace CouplePlanner.Application.Services
{
  public class AbsenceApplicationService : ApplicationService<Absence, Domain.Entities.Absence>,
    IHappeningApplicationService<Absence, Domain.Entities.Absence>
  {
    private IRepository<Domain.Entities.Absence> Repository { get; }

    private IMapper Mapper { get; }

    public AbsenceApplicationService(IRepository<Domain.Entities.Absence> repository, IMapper mapper) : base(repository, mapper)
    {
      Repository = repository;
      Mapper = mapper;
    }

    public IEnumerable<Absence> GetUpcoming(int take)
    {
      var entities = Repository.GetAll(a => a.EndDate >= DateTime.Today.ToUniversalTime()).Take(take);
      var mappedEntities = entities.Select(a => Mapper.Map<Absence>(a));
      return mappedEntities;
    }
  }
}
