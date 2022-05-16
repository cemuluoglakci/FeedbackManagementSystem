using FluentValidation;

namespace ApplicationFMS.Handlers.Replies.Commands.UpsertReply
{
    public class UpsertReplyCommandValidator : AbstractValidator<UpsertReplyCommand>
    {
        public UpsertReplyCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0).LessThan(100000000).When(x => x.Id != null);
            RuleFor(x => x.FeedbackId).GreaterThan(0).LessThan(100000000);
            RuleFor(x => x.Text).NotNull().NotEmpty().Length(10, 500);
        }
    }
}
