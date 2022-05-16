using FluentValidation;
using System;
using System.Text.RegularExpressions;

namespace ApplicationFMS.Handlers.Account.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Phone)
                .Length(9, 12).WithMessage("PhoneNumber must not be between 9-12 characters.").When(x => !string.IsNullOrEmpty(x.Phone))
                .Matches(@"\d+").When(x => !string.IsNullOrEmpty(x.Phone));
            RuleFor(x => x.Password).NotEmpty().WithMessage("Your password cannot be empty")
                .MinimumLength(8).WithMessage("Your password length must be at least 8.")
                .MaximumLength(16).WithMessage("Your password length must not exceed 16.")
                .Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
                .Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
                .Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().Length(2, 20);
            RuleFor(x => x.LastName).NotNull().NotEmpty().Length(2, 20);
            RuleFor(x => x.RoleId).NotNull().NotEmpty().GreaterThan(0).LessThan(6);

            RuleFor(x => x.BirthDate).Must(BeAValidAge).WithMessage("Invalid {PropertyName}").When(x => x.BirthDate != null);
            RuleFor(x => x.CityId).GreaterThanOrEqualTo(0).LessThan(100000000).When(x => x.CityId !=null);
            RuleFor(x => x.EducationId).GreaterThanOrEqualTo(0).LessThan(100000000).When(x => x.EducationId != null);
            RuleFor(x => x.CompanyId).GreaterThanOrEqualTo(0).LessThan(100000000).When(x => x.CompanyId != null);
        }



        protected bool BeAValidAge(DateTime? date)
        {
            if(date == null)
            {
                return true;
            }

            int currentYear = DateTime.Now.Year;
            int dobYear = (int)date?.Year;

            if (dobYear <= currentYear - 6 && dobYear > (currentYear - 120))
            {
                return true;
            }

            return false;
        }

    }
}
