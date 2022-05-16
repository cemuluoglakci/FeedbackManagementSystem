using FluentValidation;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.DeleteFeedback
{
    public class DeleteFeedbackCommandValidator : AbstractValidator<DeleteFeedbackCommand>
    {
        public DeleteFeedbackCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
        }
    }
}
