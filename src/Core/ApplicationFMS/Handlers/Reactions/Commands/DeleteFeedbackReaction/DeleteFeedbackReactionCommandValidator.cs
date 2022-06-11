using FluentValidation;

namespace ApplicationFMS.Handlers.Reactions.Commands.DeleteFeedbackReaction
{
    public class DeleteFeedbackReactionCommandValidator : AbstractValidator<DeleteFeedbackReactionCommand>
    {
        public DeleteFeedbackReactionCommandValidator()
        {
            RuleFor(x => x.FeedbackId).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
        }
    }
}
