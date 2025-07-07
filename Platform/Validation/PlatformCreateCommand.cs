using FluentValidation;
using PlatformService.Commands;

namespace PlatformService.Validation
{
    public class PlatformCreateCommandValidator : AbstractValidator<PlatformCreateCommand>
    {
        public PlatformCreateCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name must not exceed 100 characters.");

            RuleFor(x => x.Publisher)
                .NotEmpty().WithMessage("Publisher is required.")
                .MaximumLength(100);

      RuleFor(x => x.Cost)
          .NotEmpty().WithMessage("Cost is required.");
        }
    }
}
