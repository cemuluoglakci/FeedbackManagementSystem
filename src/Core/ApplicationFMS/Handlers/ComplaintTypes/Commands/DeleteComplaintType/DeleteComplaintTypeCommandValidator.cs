using FluentValidation;

namespace ApplicationFMS.Handlers.ComplaintTypes.Commands.DeleteComplaintType
{
    public class DeleteComplaintTypeCommandValidator : AbstractValidator<DeleteComplaintTypeCommand>
    {
        public DeleteComplaintTypeCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
        }
    }
}
