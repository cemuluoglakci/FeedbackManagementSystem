using FluentValidation;
using System;

namespace ApplicationFMS.Handlers.System.Queries.GetLogs
{
    public class GetLogListQueryValidator : AbstractValidator<GetLogListQuery>
    {
        public GetLogListQueryValidator()
        {
            RuleFor(x => x.ObjectsPerPage).GreaterThan(2).LessThanOrEqualTo(1000).When(x => x.ObjectsPerPage != 0);
            RuleFor(x => x.PageNumber).GreaterThanOrEqualTo(1).LessThan(10000000).When(x => x.PageNumber != 0);
            RuleFor(v => v.RequestedBefore).LessThanOrEqualTo(DateTime.Now).When(v => v.RequestedBefore.HasValue);
            RuleFor(v => v.RequestedAfter).LessThan(DateTime.Now).When(v => v.RequestedAfter.HasValue);
            RuleFor(v => v.RequestedAfter).LessThan(x => x.RequestedBefore)
                .When(v => v.RequestedAfter.HasValue && v.RequestedBefore.HasValue);
        }
    }
}