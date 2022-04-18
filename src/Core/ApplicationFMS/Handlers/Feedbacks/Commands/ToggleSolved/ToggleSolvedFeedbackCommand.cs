using ApplicationFMS.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.ToggleSolved
{
    public  class ToggleSolvedFeedbackCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }
    }
}
