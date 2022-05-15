using FluentValidation;

namespace ApplicationFMS.Handlers.System.Commands.SetTimeoutDuration
{
    public class SetTimeoutDurationCommandValidator : AbstractValidator<SetTimeoutDurationCommand>
    {
        public SetTimeoutDurationCommandValidator()
        {
            RuleFor(v => v.Value).NotNull().GreaterThan(4).LessThan(3000);
        }
    }
}
