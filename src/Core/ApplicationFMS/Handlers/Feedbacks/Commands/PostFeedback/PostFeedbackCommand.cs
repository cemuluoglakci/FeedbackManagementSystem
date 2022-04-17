using ApplicationFMS.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.PostFeedback
{
    public class PostFeedbackCommand : IRequest<BaseResponse<int>>
    {
        public string Title { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int ProductId { get; set; }
        public int TypeId { get; set; }
        public int? SubTypeId { get; set; }
        public bool IsAnonym { get; set; } = false;
    }
}
