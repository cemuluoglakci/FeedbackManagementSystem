using FluentValidation;

namespace ApplicationFMS.Handlers.Reactions.Commands.ReactFeedback
{
    public class ReactFeedbackCommandValidator : AbstractValidator<ReactFeedbackCommand>
    {
        public ReactFeedbackCommandValidator()
        {
            RuleFor(x => x.FeedbackId).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
            RuleFor(x => x.Sentiment).NotNull().NotEmpty();
        }
    }
}
