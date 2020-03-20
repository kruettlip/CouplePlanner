using System;

namespace CouplePlanner.Application.Entities
{
  public class Event
  {
    public Guid Id { get; set; }

    public DateTimeOffset StartDate { get; set; }

    public DateTimeOffset EndDate { get; set; }

    public string Location { get; set; }

    public string Travel { get; set; }
  }
}
