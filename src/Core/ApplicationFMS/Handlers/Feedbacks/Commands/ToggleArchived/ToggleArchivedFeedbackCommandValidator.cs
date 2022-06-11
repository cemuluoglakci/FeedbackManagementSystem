using FluentValidation;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.ToggleArchived
{
    public class ToggleArchivedFeedbackCommandValidator : AbstractValidator<ToggleArchivedFeedbackCommand>
    {
        public ToggleArchivedFeedbackCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
        }
    }
}
