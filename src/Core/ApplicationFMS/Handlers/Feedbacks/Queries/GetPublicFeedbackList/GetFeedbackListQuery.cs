using ApplicationFMS.Models;
using MediatR;
using System;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList
{
    public class GetFeedbackListQuery : IRequest<BaseResponse<FeedbackListVm>>
    {
        public int ObjectsPerPage { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public string? SortColumn { get; set; } = "Id";
        public bool? IsAscending { get; set; } = true;
        public string? TitleQuery { get; set; } = string.Empty;
        public string? TextQuery { get; set; } = string.Empty;
        public DateTime? CreatedAtBefore { get; set; }
        public DateTime? CreatedAtAfter { get; set; }

        public int? SectorId { get; set; }
        public int? CompanyId { get; set; }
        public int? ProductId { get; set; }
        public int? TypeId { get; set; }
        public int? SubTypeId { get; set; }
        public int? UserId { get; set; }

        //For Admins and Company Users
        public bool? IsDirected { get; set; }
        public bool? IsArchived { get; set; }

        //Only for admins
        public bool? IsAnonym { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsChecked { get; set; }
        public bool? IsReplied { get; set; }
        public bool? IsSolved { get; set; }




    }
}
