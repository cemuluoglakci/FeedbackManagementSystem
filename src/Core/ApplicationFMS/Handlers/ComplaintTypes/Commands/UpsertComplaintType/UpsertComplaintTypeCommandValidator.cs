using FluentValidation;

namespace ApplicationFMS.Handlers.ComplaintTypes.Commands.UpsertComplaintType
{
    public class UpsertComplaintTypeCommandValidator : AbstractValidator<UpsertComplaintTypeCommand>
    {
        public UpsertComplaintTypeCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0).LessThan(100000000).When(x => x.Id != null);
            RuleFor(x => x.SubTypeName).NotNull().NotEmpty().Length(3, 30);
        }
    }
}
