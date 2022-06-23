using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Comments.Queries.GetCommentDetail
{
    public class GetCommentDetailQuery : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
