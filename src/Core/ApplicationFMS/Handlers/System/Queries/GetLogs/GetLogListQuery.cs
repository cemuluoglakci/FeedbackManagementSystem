using ApplicationFMS.Models;
using MediatR;
using System;

namespace ApplicationFMS.Handlers.System.Queries.GetLogs
{
    public class GetLogListQuery : IRequest<BaseResponse>
    {
        public int ObjectsPerPage { get; set; } = 10;
        public int PageNumber { get; set; } = 1;
        public string? SortColumn { get; set; } = "Id";
        public bool? IsAscending { get; set; } = false;
        public string? Query { get; set; } = string.Empty;
        public bool? ResponseStatus { get; set; }
        public DateTime? RequestedBefore { get; set; }
        public DateTime? RequestedAfter { get; set; }

    }
}
