using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.UserHandlers.Commands.ToggleUserAbility
{
    public class ToggleUserAbilityCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }
    }
}
