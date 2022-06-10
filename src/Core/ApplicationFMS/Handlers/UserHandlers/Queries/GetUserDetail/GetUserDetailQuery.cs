using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.UserHandlers.Queries.GetUserDetail
{
    public class GetUserDetailQuery  : IRequest<BaseResponse>
    {
        public int ?Id { get; set; }
    }
}
