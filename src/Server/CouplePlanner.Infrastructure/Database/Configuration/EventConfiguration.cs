using System;
using CouplePlanner.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CouplePlanner.Infrastructure.Database.Configuration
{
  public class EventConfiguration : IEntityTypeConfiguration<Event>
  {
    public void Configure(EntityTypeBuilder<Event> entity)
    {
      entity.Property(e => e.Id).HasDefaultValue(Guid.NewGuid()).ValueGeneratedOnAdd();
    }
  }
}
