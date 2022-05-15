using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.UpsertFeedback
{
    public class UpsertFeedbackCommand : IRequest<BaseResponse>
    {
        public int? Id { get; set; }
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int ProductId { get; set; }
        public int TypeId { get; set; }
        public int? SubTypeId { get; set; }
        public bool IsAnonym { get; set; } = false;
    }
}
