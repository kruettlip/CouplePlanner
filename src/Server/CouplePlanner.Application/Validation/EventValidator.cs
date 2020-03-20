using CouplePlanner.Application.Entities;
using FluentValidation;

namespace CouplePlanner.Application.Validation
{
  public class EventValidator : AbstractValidator<Event>
  {
    public EventValidator()
    {
      RuleFor(e => e.Location).MinimumLength(3);
      RuleFor(e => e.Travel).MinimumLength(3);
      RuleFor(e => e.EndDate).GreaterThan(e => e.StartDate);
    }
  }
}
