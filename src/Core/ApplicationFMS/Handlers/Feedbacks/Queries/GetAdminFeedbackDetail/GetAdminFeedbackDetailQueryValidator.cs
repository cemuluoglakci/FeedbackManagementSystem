using FluentValidation;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetAdminFeedbackDetail
{
    public class GetAdminFeedbackDetailQueryValidator : AbstractValidator<GetAdminFeedbackDetailQuery>
    {
        public GetAdminFeedbackDetailQueryValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
        }
    }
}
