using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPlainFeedback
{
    public class GetPlainFeedbackQuery : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
