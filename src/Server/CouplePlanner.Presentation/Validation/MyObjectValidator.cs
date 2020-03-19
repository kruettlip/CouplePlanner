using CouplePlanner.Presentation.Entities;
using FluentValidation;

namespace CouplePlanner.Presentation.Validation
{
  /// <summary>
  /// Validator for MyObject
  /// </summary>
  public class MyObjectValidator : AbstractValidator<MyObject>
  {
    /// <summary>
    /// Constructor for validator
    /// </summary>
    public MyObjectValidator()
    {
      RuleFor(o => o.Name).NotEmpty();
      RuleFor(o => o.Name).MinimumLength(5).WithMessage("Name must be at least 5 characters long.");
      RuleFor(o => o.Content).Must(c => c.StartsWith("#")).Must(c => c.EndsWith("#"));
    }
  }
}
