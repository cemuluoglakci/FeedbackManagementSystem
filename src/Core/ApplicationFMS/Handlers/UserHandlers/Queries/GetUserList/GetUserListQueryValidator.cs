using ApplicationFMS.Helpers;
using FluentValidation;
using System;

namespace ApplicationFMS.Handlers.UserHandlers.Queries.GetUserList
{
    public class GetUserListQueryValidator : AbstractValidator<GetUserListQuery>
    {
        public GetUserListQueryValidator()
        {
            RuleFor(x => x.ObjectsPerPage).GreaterThan(2).LessThanOrEqualTo(1000).When(x => x.ObjectsPerPage != 0);
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1).LessThan(10000000).When(x => x.PageNumber != 0);
            
            RuleFor(v => v.RegisteredAtBefore).LessThanOrEqualTo(DateTime.Now).When(v => v.RegisteredAtBefore.HasValue);
            RuleFor(v => v.RegisteredAtAfter).LessThan(DateTime.Now).When(v => v.RegisteredAtAfter.HasValue);
            RuleFor(v => v.RegisteredAtAfter).LessThan(x => x.RegisteredAtBefore)
                .When(v => v.RegisteredAtAfter.HasValue && v.RegisteredAtBefore.HasValue);

            RuleFor(v => v.BirthDateBefore).Must(Tools.BeAValidAge).When(v => v.BirthDateBefore.HasValue);
            RuleFor(v => v.BirthDateAfter).Must(Tools.BeAValidAge).When(v => v.BirthDateAfter.HasValue);
            RuleFor(v => v.BirthDateAfter).LessThan(x => x.BirthDateBefore)
                .When(v => v.BirthDateAfter.HasValue && v.BirthDateBefore.HasValue);

            RuleFor(x => x.CityId).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000).When(x => x.CityId != null);
            RuleFor(x => x.CompanyId).NotNull().NotEmpty().GreaterThan(0).LessThan(100000).When(x => x.CompanyId != null);
            RuleFor(x => x.EducationId).NotNull().NotEmpty().GreaterThan(0).LessThan(20).When(x => x.EducationId != null);
            RuleFor(x => x.RoleId).NotNull().NotEmpty().GreaterThan(0).LessThan(6).When(x => x.RoleId != null);

        }
    }
}
