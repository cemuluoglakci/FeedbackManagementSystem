using ApplicationFMS.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Replies.Commands.ReplyFeedback
{
    public class ReplyFeedbackCommand : IRequest<BaseResponse<int>>
    {
        public int FeedbackId { get; set; }
        public string Text { get; set; } = null!;
    }
}
