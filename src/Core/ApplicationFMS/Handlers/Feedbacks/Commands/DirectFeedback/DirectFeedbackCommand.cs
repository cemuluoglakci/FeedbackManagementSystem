using ApplicationFMS.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.DirectFeedback
{
    public class DirectFeedbackCommand : IRequest<BaseResponse<int>>
    {
        public int FeedbackId { get; set; }
        public int EmployeeId { get; set; }
    }
}
