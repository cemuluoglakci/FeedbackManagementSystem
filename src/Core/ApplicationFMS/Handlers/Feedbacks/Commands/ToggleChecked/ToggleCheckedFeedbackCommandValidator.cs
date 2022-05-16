using FluentValidation;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.ToggleChecked
{
    public class ToggleCheckedFeedbackCommandValidator : AbstractValidator<ToggleCheckedFeedbackCommand>
    {
        public ToggleCheckedFeedbackCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
        }
    }
}
