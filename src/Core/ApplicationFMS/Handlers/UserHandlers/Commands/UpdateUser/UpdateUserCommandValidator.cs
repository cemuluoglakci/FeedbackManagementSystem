using FluentValidation;

namespace ApplicationFMS.Handlers.UserHandlers.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0).LessThanOrEqualTo(100000000);
            RuleFor(x => x.Email).EmailAddress().When(x => !string.IsNullOrEmpty(x.Email));
            RuleFor(x => x.Phone)
                .Length(9, 12).WithMessage("PhoneNumber must not be between 9-12 characters.").When(x => !string.IsNullOrEmpty(x.Phone))
                .Matches(@"\d+").When(x => !string.IsNullOrEmpty(x.Phone));
            RuleFor(x => x.Password).NotEmpty().MinimumLength(8).MaximumLength(16)
                .Matches(@"[A-Z]+").Matches(@"[a-z]+").Matches(@"[0-9]+").When(x => !string.IsNullOrEmpty(x.Password));
            RuleFor(x => x.FirstName).Length(2, 20).When(x => !string.IsNullOrEmpty(x.FirstName));
            RuleFor(x => x.LastName).Length(2, 20).When(x => !string.IsNullOrEmpty(x.LastName));

            RuleFor(x => x.CityId).GreaterThanOrEqualTo(0).LessThan(100000000).When(x => x.CityId != null);
            RuleFor(x => x.EducationId).GreaterThanOrEqualTo(0).LessThan(100000000).When(x => x.EducationId != null);
        }
    }
}
