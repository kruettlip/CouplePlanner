using System.Reflection;
using AutoMapper;
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
      services.AddTransient<IEventApplicationService, EventApplicationService>();
      services.AddTransient<IAbsenceApplicationService, AbsenceApplicationService>();
    }

    public static void AddApplication(this IMvcBuilder mvcBuilder)
    {
      mvcBuilder.AddFluentValidation(o => { o.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()); });
    }
  }
}
