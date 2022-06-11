using FluentValidation;

namespace ApplicationFMS.Handlers.Comments.Commands.ToggleChecked
{
    public class ToggleCheckedCommentCommandValidator : AbstractValidator<ToggleCheckedCommentCommand>
    {
        public ToggleCheckedCommentCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
        }

    }
}
