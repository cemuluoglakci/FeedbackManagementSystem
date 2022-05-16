using FluentValidation;

namespace ApplicationFMS.Handlers.Products.Commands.UpsertProduct
{
    public class UpsertProductCommandValidator : AbstractValidator<UpsertProductCommand>
    {
        public UpsertProductCommandValidator()
        {
            RuleFor(x => x.Id).GreaterThanOrEqualTo(0).LessThan(100000000).When(x => x.Id != null);
            RuleFor(x => x.ProductName).NotNull().NotEmpty().Length(3, 30);
        }
    }
}
