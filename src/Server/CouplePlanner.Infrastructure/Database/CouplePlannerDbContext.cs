using System;
using System.Reflection;
using CouplePlanner.Domain.Common;
using CouplePlanner.Domain.Entities;
using CouplePlanner.Infrastructure.Database.Configuration;
using CouplePlanner.Infrastructure.Database.ExtensionMethods;
using CouplePlanner.Infrastructure.Database.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CouplePlanner.Infrastructure.Database
{
	public class CouplePlannerDbContext : DbContext, ICouplePlannerDbContext
	{
		public DbSet<Event> Events { get; set; }

		public DbSet<Absence> Absences { get; set; }

		public CouplePlannerDbContext(DbContextOptions options) : base(options) { }

		public override int SaveChanges()
		{
			foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
			{
				switch (entry.State)
				{
					case EntityState.Added:
						entry.Entity.CreatedBy = "";
						entry.Entity.Created = DateTime.Now.ToUniversalTime();
						break;
					case EntityState.Modified:
						entry.Entity.LastModifiedBy = "";
						entry.Entity.LastModified = DateTime.Now.ToUniversalTime();
						break;
				}
			}

			return base.SaveChanges();
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration(new EventConfiguration());
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
			modelBuilder.Seeding();
			base.OnModelCreating(modelBuilder);
		}
	}
}
