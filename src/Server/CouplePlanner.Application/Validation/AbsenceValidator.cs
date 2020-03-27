using CouplePlanner.Application.Entities;
using FluentValidation;

namespace CouplePlanner.Application.Validation
{
	public class AbsenceValidator : AbstractValidator<Absence>
	{
		public AbsenceValidator()
		{
			RuleFor(a => a.AbsenceReason).MinimumLength(5);
			RuleFor(a => a.EndDate).GreaterThan(a => a.StartDate);
		}
	}
}
