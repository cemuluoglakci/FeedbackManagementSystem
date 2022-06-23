using ApplicationFMS.Handlers.Comments.Queries.GetCommentDetail;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreFMS.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Comments.Queries.GetOwnCommentList
{
    public class GetOwnCommentListQueryHandler : IRequestHandler<GetOwnCommentListQuery, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser? _currentUser;

        public GetOwnCommentListQueryHandler(IFMSDataContext context, IMapper mapper, ICurrentUser? currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse> Handle(GetOwnCommentListQuery request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return BaseResponse.Fail("User Identity could not defined.");
            }
            IQueryable<Comment>? commentQuery = _context.Comment.Where(x => x.UserId == _currentUser.UserDetail.Id && x.IsActive);

            //Transfering type to Data Transfer Object
            var dtoQuery = commentQuery.ProjectTo<CommentDetailDTO>(_mapper.ConfigurationProvider);

            //Ordering
            dtoQuery = dtoQuery.OrderBy("Id descending");

            
            var comments = await dtoQuery.ToListAsync(cancellationToken);
            int commentCount = comments.Count();

            var viewModel = new GetOwnCommentListVm
            {
                Count = commentCount,
                CommentList = comments
            };

            return new BaseResponse(viewModel);
        }
    }
}
