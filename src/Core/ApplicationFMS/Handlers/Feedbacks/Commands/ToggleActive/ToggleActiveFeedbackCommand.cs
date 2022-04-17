using ApplicationFMS.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.ToggleActive
{
    public class ToggleActiveFeedbackCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }
    }


}
