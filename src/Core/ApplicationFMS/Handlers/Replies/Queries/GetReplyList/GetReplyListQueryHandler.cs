using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail;
using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreFMS.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Replies.Queries.GetReplyList
{
    public class GetReplyListQueryHandler : IRequestHandler<GetReplyListQuery, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser? _currentUser;

        public GetReplyListQueryHandler(IFMSDataContext context, IMapper mapper, ICurrentUser? currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse> Handle(GetReplyListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Reply>? replyQuery = _context.Reply;

            int totalCount = replyQuery.Count();

            if (String.IsNullOrEmpty(request.SortColumn)) request.SortColumn = "Id";

            if (!String.IsNullOrEmpty(request.TextQuery))
                replyQuery = replyQuery.Where(x => x.Text.Contains(request.TextQuery));

            if (request.IsActive != null) replyQuery = replyQuery.Where(x => x.IsActive == request.IsActive);

            if (request.IsChecked != null) replyQuery = replyQuery.Where(x => x.IsChecked == request.IsChecked);

            var viewModel = new GetReplyListVm
            {
                TotalCount = totalCount,
                FilteredCount = replyQuery.Count(),
                ObjectsPerPage = request.ObjectsPerPage,
                PageNumber = request.PageNumber,
            };

            //Transfering type to Data Transfer Object
            var dtoQuery = replyQuery.ProjectTo<ReplyDto>(_mapper.ConfigurationProvider);

            //Pagination and ordering
            dtoQuery = Tools.ArrangeList(dtoQuery, request.SortColumn, request.IsAscending, request.ObjectsPerPage, request.PageNumber);

            var replies = await dtoQuery.ToListAsync(cancellationToken);
            viewModel.ReplyList = replies;

            return new BaseResponse(viewModel);
        }
    }
}
