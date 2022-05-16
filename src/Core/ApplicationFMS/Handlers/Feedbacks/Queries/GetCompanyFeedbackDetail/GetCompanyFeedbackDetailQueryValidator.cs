using FluentValidation;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetCompanyFeedbackDetail
{
    public class GetCompanyFeedbackDetailQueryValidator : AbstractValidator<GetCompanyFeedbackDetailQuery>
    {
        public GetCompanyFeedbackDetailQueryValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
        }
    }
}
