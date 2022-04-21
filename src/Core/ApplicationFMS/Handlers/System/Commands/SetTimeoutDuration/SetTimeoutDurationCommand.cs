using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.System.Commands.SetTimeoutDuration
{
    public class SetTimeoutDurationCommand : IRequest<BaseResponse<string>>
    {
        public int Value { get; set; }
    }
}
