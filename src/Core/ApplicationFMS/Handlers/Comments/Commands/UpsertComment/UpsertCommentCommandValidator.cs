using FluentValidation;

namespace ApplicationFMS.Handlers.Comments.Commands.UpsertComment
{
    public class UpsertCommentCommandValidator : AbstractValidator<UpsertCommentCommand>
    {
        public UpsertCommentCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0).LessThan(100000000).When(x => x.Id != null);
            RuleFor(x => x.FeedbackId).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
            RuleFor(x => x.ParentCommentId).GreaterThan(0).LessThan(100000000).When(x => x.Id != null);
            RuleFor(x => x.Text).NotNull().NotEmpty().Length(10, 280);
            RuleFor(x => x.IsAnonym).NotNull();
        }
    }
}
