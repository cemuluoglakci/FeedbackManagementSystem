using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.System.Commands.SetTimeoutDuration
{
    public class SetTimeoutDurationCommand : IRequest<BaseResponse>
    {
        public int Value { get; set; }
    }
}
