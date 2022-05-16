using FluentValidation;
using System;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList
{
    public class GetFeedbackListQueryValidator : AbstractValidator<GetFeedbackListQuery>
    {
        public GetFeedbackListQueryValidator()
        {
            RuleFor( x => x.ObjectsPerPage ).GreaterThan(2).LessThanOrEqualTo(1000).When(x => x.ObjectsPerPage != 0);
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1).LessThan(10000000).When(x => x.PageNumber != 0);
            RuleFor(v => v.CreatedAtBefore).LessThanOrEqualTo(DateTime.Now).When(v => v.CreatedAtBefore.HasValue);
            RuleFor(v => v.CreatedAtAfter).LessThan(DateTime.Now).When(v => v.CreatedAtAfter.HasValue);
            RuleFor(v => v.CreatedAtAfter).LessThan(x => x.CreatedAtBefore)
                .When(v => v.CreatedAtAfter.HasValue && v.CreatedAtBefore.HasValue);
            RuleFor(x => x.SectorId).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000).When(x => x.SectorId != null);
            RuleFor(x => x.CompanyId).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000).When(x => x.CompanyId != null);
            RuleFor(x => x.ProductId).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000).When(x => x.ProductId != null);
            RuleFor(x => x.TypeId).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000).When(x => x.TypeId != null);
            RuleFor(x => x.SubTypeId).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000).When(x => x.SubTypeId != null);
            RuleFor(x => x.UserId).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000).When(x => x.UserId != null);

        }
    }
}
