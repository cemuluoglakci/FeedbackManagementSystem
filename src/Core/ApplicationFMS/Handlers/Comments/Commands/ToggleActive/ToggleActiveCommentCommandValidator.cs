using FluentValidation;

namespace ApplicationFMS.Handlers.Comments.Commands.ToggleActive
{
    public class ToggleActiveCommentCommandValidator : AbstractValidator<ToggleActiveCommentCommand>
    {
        public ToggleActiveCommentCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
        }

    }
}
