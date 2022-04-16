using ApplicationFMS.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.UserHandlers.Commands.ToggleUserAbility
{
    public class ToggleUserAbilityCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }
    }
}
