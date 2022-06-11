using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.System.Commands.SetOperationalMode
{
    public class SetOperationalModeCommand : IRequest<BaseResponse>
    {
        public int ModeId { get; set; }
    }
}
