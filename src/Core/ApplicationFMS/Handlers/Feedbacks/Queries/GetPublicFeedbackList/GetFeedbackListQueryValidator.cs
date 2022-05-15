using FluentValidation;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList
{
    public class GetFeedbackListQueryValidator : AbstractValidator<GetFeedbackListQuery>
    {
        public GetFeedbackListQueryValidator()
        {
            RuleFor(v => v.UserId).NotEmpty();
            RuleFor(v => v.ObjectsPerPage).NotEmpty().GreaterThan(300500);
        }
    }
}
