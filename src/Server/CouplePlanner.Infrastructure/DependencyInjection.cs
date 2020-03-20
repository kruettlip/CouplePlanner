using CouplePlanner.Application.Interfaces.Repositories;
using CouplePlanner.Domain.Entities;
using CouplePlanner.Infrastructure.Database;
using CouplePlanner.Infrastructure.Database.Interfaces;
using CouplePlanner.Infrastructure.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CouplePlanner.Infrastructure
{
  public static class DependencyInjection
  {
    public static void AddInfrastructure(this IServiceCollection services)
    {
      services.AddDbContext<CouplePlannerDbContext>(options =>
        {
          options.UseNpgsql("Server=localhost;Port=5432;Database=CouplePlanner;User Id=dev;Password=dev;");
        });
      services.AddScoped<ICouplePlannerDbContext, CouplePlannerDbContext>();
      services.AddTransient<IRepository<Event>, EventRepository>();
      services.AddTransient<IRepository<Absence>, AbsenceRepository>();
    }
  }
}
