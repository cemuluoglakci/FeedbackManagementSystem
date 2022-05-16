using FluentValidation;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.ToggleSolved
{
    public class ToggleSolvedFeedbackCommandValidator : AbstractValidator<ToggleSolvedFeedbackCommand>
    {
        public ToggleSolvedFeedbackCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
        }
    }
}
