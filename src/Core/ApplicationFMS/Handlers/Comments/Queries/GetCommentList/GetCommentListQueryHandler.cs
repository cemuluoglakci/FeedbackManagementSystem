using ApplicationFMS.Handlers.Comments.Queries.GetCommentDetail;
using ApplicationFMS.Handlers.System.Queries.GetLogs;
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

namespace ApplicationFMS.Handlers.Comments.Queries.GetCommentList
{
    public class GetCommentListQueryHandler : IRequestHandler<GetCommentListQuery, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser? _currentUser;

        public GetCommentListQueryHandler(IFMSDataContext context, IMapper mapper, ICurrentUser? currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse> Handle(GetCommentListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Comment>? commentQuery = _context.Comment;

            int totalCount = commentQuery.Count();

            if (String.IsNullOrEmpty(request.SortColumn)) request.SortColumn = "Id";
            
            if (!String.IsNullOrEmpty(request.TextQuery))
                commentQuery = commentQuery.Where(x => x.Text.Contains(request.TextQuery));
            
            if (request.IsActive != null) commentQuery = commentQuery.Where(x => x.IsActive == request.IsActive);
            if (request.IsChecked != null) commentQuery = commentQuery.Where(x => x.IsChecked == request.IsChecked);

            var viewModel = new GetCommentListVm
            {
                TotalCount = totalCount,
                FilteredCount = commentQuery.Count(),
                ObjectsPerPage = request.ObjectsPerPage,
                PageNumber = request.PageNumber,
            };

            //Transfering type to Data Transfer Object
            var dtoQuery = commentQuery.ProjectTo<CommentDetailDTO>(_mapper.ConfigurationProvider);

            //Pagination and ordering
            dtoQuery = Tools.ArrangeList(dtoQuery, request.SortColumn, request.IsAscending, request.ObjectsPerPage, request.PageNumber);

            var comments = await dtoQuery.ToListAsync(cancellationToken);
            viewModel.CommentList = comments;

            return new BaseResponse(viewModel);
        }
    }
}
