using System;
using System.IO;
using System.Reflection;
using CouplePlanner.Presentation.Schema;
using FluentValidation.AspNetCore;
using kruettlip.OpenApi2JsonSchema;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

namespace CouplePlanner.Presentation
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddTransient<IJsonSchemaGenerator, JsonSchemaGenerator>();
      services.AddTransient<ISchemaProvider, SchemaProvider>();

      services.AddControllers().AddFluentValidation(o => { o.RegisterValidatorsFromAssemblyContaining<Startup>(); });

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo {Title = "CouplePlanner.Server", Version = "v1"});
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
        c.AddFluentValidationRules();
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (File.Exists("swagger.json"))
        File.Delete("swagger.json");
      app.UseSwagger();

      if (env.IsDevelopment())
      {
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "CouplePlanner.Server v1"); });
      }

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
  }
}
