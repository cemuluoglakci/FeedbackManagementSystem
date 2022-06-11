using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.ComplaintTypes.Commands.UpsertComplaintType
{
    public class UpsertComplaintTypeCommand : IRequest<BaseResponse>
    {
        public int? Id { get; set; }
        public string SubTypeName { get; set; }
    }
}
