using FluentValidation;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail
{
    public class GetPublicFeedbackDetailQueryValidator : AbstractValidator<GetPublicFeedbackDetailQuery>
    {
        public GetPublicFeedbackDetailQueryValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().GreaterThan(0).LessThan(100000000);
        }
    }
}
