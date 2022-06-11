using FluentValidation;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.ToggleActive
{
    public class ToggleActiveFeedbackCommandValidator : AbstractValidator<ToggleActiveFeedbackCommand>
    {
        public ToggleActiveFeedbackCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
        }
    }
}
