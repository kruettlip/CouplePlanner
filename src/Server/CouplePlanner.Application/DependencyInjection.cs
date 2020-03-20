using System.Reflection;
using AutoMapper;
using CouplePlanner.Application.Entities;
using CouplePlanner.Application.Interfaces.Services;
using CouplePlanner.Application.Schema;
using CouplePlanner.Application.Services;
using FluentValidation.AspNetCore;
using kruettlip.OpenApi2JsonSchema;
using Microsoft.Extensions.DependencyInjection;

namespace CouplePlanner.Application
{
  public static class DependencyInjection
  {
    public static void AddApplication(this IServiceCollection services)
    {
      services.AddAutoMapper(Assembly.GetExecutingAssembly());
      services.AddTransient<IJsonSchemaGenerator, JsonSchemaGenerator>();
      services.AddTransient<ISchemaProvider, SchemaProvider>();
      services.AddTransient<IApplicationService<Event, Domain.Entities.Event>, ApplicationService<Event, Domain.Entities.Event>>();
      services.AddTransient<IApplicationService<Absence, Domain.Entities.Absence>, ApplicationService<Absence, Domain.Entities.Absence>>();
    }

    public static void AddApplication(this IMvcBuilder mvcBuilder)
    {
      mvcBuilder.AddFluentValidation(o => { o.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()); });
    }
  }
}
