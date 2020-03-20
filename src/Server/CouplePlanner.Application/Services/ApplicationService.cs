using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CouplePlanner.Application.Interfaces.Repositories;
using CouplePlanner.Application.Interfaces.Services;

namespace CouplePlanner.Application.Services
{
  public class ApplicationService<T, TDomain> : IApplicationService<T, TDomain>
  {
    private IRepository<TDomain> Repository { get; }

    private IMapper Mapper { get; }

    public ApplicationService(IRepository<TDomain> repository, IMapper mapper)
    {
      Repository = repository;
      Mapper = mapper;
    }

    public IEnumerable<T> GetAll()
    {
      var entities = Repository.GetAll(e => true);
      var mappedEntities = entities.Select(e => Mapper.Map<T>(e));
      return mappedEntities;
    }

    public Guid Create(T newEntity)
    {
      var mappedEntity = Mapper.Map<TDomain>(newEntity);
      var id = Repository.Add(mappedEntity);
      return id;
    }

    public void Delete(Guid id)
    {
      Repository.Delete(id);
    }
  }
}
