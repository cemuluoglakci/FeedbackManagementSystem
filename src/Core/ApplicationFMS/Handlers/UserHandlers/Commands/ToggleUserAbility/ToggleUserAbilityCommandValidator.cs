using FluentValidation;

namespace ApplicationFMS.Handlers.UserHandlers.Commands.ToggleUserAbility
{
    public class ToggleUserAbilityCommandValidator : AbstractValidator<ToggleUserAbilityCommand>
    {
        public ToggleUserAbilityCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
        }
    }
}
