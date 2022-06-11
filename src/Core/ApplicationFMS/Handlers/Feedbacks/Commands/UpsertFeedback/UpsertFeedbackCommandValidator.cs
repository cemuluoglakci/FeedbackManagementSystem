using FluentValidation;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.UpsertFeedback
{
    public class UpsertFeedbackCommandValidator : AbstractValidator<UpsertFeedbackCommand>
    {
        public UpsertFeedbackCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0).LessThan(100000000).When(x => x.Id != null);
            RuleFor(x => x.Title).NotNull().NotEmpty().Length(3, 20);
            RuleFor(x => x.Text).NotNull().NotEmpty().Length(10, 500);
            RuleFor(x => x.ProductId).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
            RuleFor(x => x.TypeId).NotNull().NotEmpty().GreaterThan(0).LessThan(10);
            RuleFor(x => x.SubTypeId).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000).When(x => x.TypeId == 1);
            RuleFor(x => x.IsAnonym).NotNull();
        }
    }
}
