using System.Reflection;
using AutoMapper;
using CouplePlanner.Application.Entities;
using CouplePlanner.Application.Interfaces.Services;
using CouplePlanner.Application.Schema;
using CouplePlanner.Application.Services;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using OpenApi2JsonSchema.DependencyInjection;
using OpenApi2JsonSchema.Configuration;

namespace CouplePlanner.Application
{
	public static class DependencyInjection
	{
		public static void AddApplication(this IServiceCollection services)
		{
			services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddJsonSchemaGenerator(new JsonSchemaGeneratorConfiguration
			{
				OpenApiUrl = "http://localhost:20220/swagger/v1/swagger.json"
			});
			services.AddTransient<ISchemaProvider, SchemaProvider>();
			services.AddTransient<IHappeningApplicationService<Event, Domain.Entities.Event>, EventApplicationService>();
			services.AddTransient<IHappeningApplicationService<Absence, Domain.Entities.Absence>, AbsenceApplicationService>();
		}

		public static void AddApplication(this IMvcBuilder mvcBuilder)
		{
			mvcBuilder.AddFluentValidation(o => { o.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()); });
		}
	}
}
