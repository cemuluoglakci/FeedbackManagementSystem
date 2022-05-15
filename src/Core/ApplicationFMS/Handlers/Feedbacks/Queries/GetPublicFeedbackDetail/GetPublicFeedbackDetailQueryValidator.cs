using FluentValidation;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail
{
    public class GetPublicFeedbackDetailQueryValidator : AbstractValidator<GetPublicFeedbackDetailQuery>
    {
        public GetPublicFeedbackDetailQueryValidator()
        {
            RuleFor(x => x.Id).NotEmpty().GreaterThan(0);
        }
    }
}
