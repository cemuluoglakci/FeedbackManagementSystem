using FluentValidation;

namespace ApplicationFMS.Handlers.System.Commands.SetOperationalMode
{
    public class SetOperationalModeCommandValidator : AbstractValidator<SetOperationalModeCommand>
    {
        public SetOperationalModeCommandValidator()
        {
            RuleFor(x => x.ModeId).NotNull().NotEmpty().GreaterThan(0).LessThanOrEqualTo(3);
        }
    }
}
