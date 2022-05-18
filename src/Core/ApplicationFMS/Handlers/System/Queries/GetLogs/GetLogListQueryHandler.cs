using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using ApplicationFMS.Handlers.LookUp.LookupMode;
using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreFMS.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.System.Queries.GetLogs
{
    public class GetLogListQueryHandler : IRequestHandler<GetLogListQuery, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;

        public GetLogListQueryHandler(IFMSDataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<BaseResponse> Handle(GetLogListQuery request, CancellationToken cancellationToken)
        {

            IQueryable<Log>? logQuery = _context.Log;

            int totalCount = logQuery.Count();

            if (String.IsNullOrEmpty(request.SortColumn))
            {
                request.SortColumn = "Id";
            }
            if (!String.IsNullOrEmpty(request.Query))
            {
                logQuery = logQuery.Where(x =>
                    x.Ip.Contains(request.Query) &&
                    x.UserEmail.Contains(request.Query) &&
                    x.Request.Contains(request.Query) &&
                    x.EndPoint.Contains(request.Query)
                    );
            }

            if (request.RequestedBefore != null)
            {
                logQuery = logQuery.Where(x => (x.RequestDate) < request.RequestedBefore);
            }
            if (request.RequestedAfter != null)
            {
                logQuery = logQuery.Where(x => (x.RequestDate) > request.RequestedAfter);
            }
            if (request.ResponseStatus != null)
            {
                logQuery = logQuery.Where(x =>
                    (!string.IsNullOrEmpty(x.Response) && !x.Response.Contains("\"SuccessStatus\":false,")) == request.ResponseStatus);
            }

            var viewModel = new LogListVm
            {
                TotalCount = totalCount,
                FilteredCount = logQuery.Count(),
                ObjectsPerPage = request.ObjectsPerPage,
                PageNumber = request.PageNumber,
            };

            var dtoQuery = logQuery.ProjectTo<LogDto>(_mapper.ConfigurationProvider);

            //Pagination and ordering
            dtoQuery = Tools.ArrangeList(dtoQuery, request.SortColumn, request.IsAscending, request.ObjectsPerPage, request.PageNumber);
            var logList = await dtoQuery.ToListAsync(cancellationToken);
            viewModel.LogList = logList;

            return new BaseResponse(viewModel);
        }

    }
}
