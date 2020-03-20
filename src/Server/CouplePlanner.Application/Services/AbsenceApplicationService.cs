using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CouplePlanner.Application.Entities;
using CouplePlanner.Application.Interfaces.Repositories;
using CouplePlanner.Application.Interfaces.Services;

namespace CouplePlanner.Application.Services
{
  public class AbsenceApplicationService : IAbsenceApplicationService
  {
    private IAbsenceRepository Repository { get; }

    private IMapper Mapper { get; }

    public AbsenceApplicationService(IAbsenceRepository repository, IMapper mapper)
    {
      Repository = repository;
      Mapper = mapper;
    }

    public IEnumerable<Absence> GetAll()
    {
      var absences = Repository.GetAll(e => true);
      var mappedAbsences = absences.Select(e => Mapper.Map<Absence>(e));
      return mappedAbsences;
    }

    public Guid Create(Absence newAbsence)
    {
      var mappedAbsence = Mapper.Map<Domain.Entities.Absence>(newAbsence);
      var id = Repository.Add(mappedAbsence);
      return id;
    }

    public void Delete(Guid id)
    {
      Repository.Delete(id);
    }
  }
}
