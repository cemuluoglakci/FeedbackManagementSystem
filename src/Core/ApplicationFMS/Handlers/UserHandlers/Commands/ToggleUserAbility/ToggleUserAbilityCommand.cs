using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.UserHandlers.Commands.ToggleUserAbility
{
    public class ToggleUserAbilityCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }
    }
}
