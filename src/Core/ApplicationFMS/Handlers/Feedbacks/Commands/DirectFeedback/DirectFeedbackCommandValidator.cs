using FluentValidation;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.DirectFeedback
{
    public class DirectFeedbackCommandValidator : AbstractValidator<DirectFeedbackCommand>
    {
        public DirectFeedbackCommandValidator()
        {
            RuleFor(x => x.FeedbackId).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
            RuleFor(x => x.EmployeeId).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
        }
    }
}
