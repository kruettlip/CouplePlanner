using System;
using System.IO;
using System.Reflection;
using CouplePlanner.Application;
using CouplePlanner.Infrastructure;
using CouplePlanner.Infrastructure.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using OpenApi2JsonSchema.DependencyInjection;
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
      services.AddApplication();
      services.AddInfrastructure();

      services.AddControllers().AddApplication();

      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "CouplePlanner.Server", Version = "v1" });
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
        c.AddFluentValidationRules();
      });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, CouplePlannerDbContext dbContext)
    {
      dbContext.Database.EnsureCreated();

      app.UseOpenApi2JsonSchemaGenerator();
      app.UseSwagger();

      if (env.IsDevelopment())
      {
        app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "CouplePlanner.Server v1"); });
      }

      app.Use(async (context, next) =>
      {
        await next();
        var path = context.Request.Path.Value;
        if (context.Response.StatusCode == 404 &&
            !path.StartsWith("/api/"))
        {
          context.Request.Path = "/index.html";
          await next();
        }
      });
      app.UseStaticFiles();
      app.UseDefaultFiles();

      app.UseRouting();
      app.UseAuthorization();
      app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
  }
}
