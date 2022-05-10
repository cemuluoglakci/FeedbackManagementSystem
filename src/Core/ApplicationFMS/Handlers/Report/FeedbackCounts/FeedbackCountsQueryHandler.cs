using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Report.FeedbackCounts
{
    public class FeedbackCountsQueryHandler : IRequestHandler<FeedbackCountsQuery, BaseResponse<FeedbackCountsVm>>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public FeedbackCountsQueryHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<FeedbackCountsVm>> Handle(FeedbackCountsQuery request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return BaseResponse<FeedbackCountsVm>.Fail("User Identity could not defined.");
            }

            if (_currentUser.UserDetail.RoleName != Constants.CompanyManagerRole)
            {
                return BaseResponse<FeedbackCountsVm>.Fail("User role is not authorized.");
            }

            IQueryable<Feedback>? feedbackQuery = _context.Feedback
                .Where(x => x.CompanyId == _currentUser.UserDetail.CompanyId && x.IsActive);

            // Filter accourding to every query
            if (request.ProductId > 0)
            {
                feedbackQuery = feedbackQuery.Where(x => x.ProductId == request.ProductId);
            }
            if (request.TypeId > 0)
            {
                feedbackQuery = feedbackQuery.Where(x => x.TypeId == request.TypeId);
            }

            var viewModel = new FeedbackCountsVm
            {
                TotalFeedbackCount = await feedbackQuery.CountAsync(),
                AnonymFeedbackCount = feedbackQuery.Where(x => x.IsAnonym).Count(),
                DirectedFeedbackCount = feedbackQuery.Where(x => x.DirectedToEmployeeId > 0).Count(),
                RepliedFeedbackCount = feedbackQuery.Where(x => x.IsReplied).Count(),
                SolvedFeedbackCount = feedbackQuery.Where(x => x.IsSolved).Count(),
                ArchivedFeedbackCount = feedbackQuery.Where(x => x.IsArchived).Count(),
                TotalSharedCount = feedbackQuery.Sum(x => x.Shared ?? 0),
                TotalLikeCount = feedbackQuery.Sum(x => x.LikeCount),
                TotalDislikeCount = feedbackQuery.Sum(x => x.DislikeCount),
            };

            viewModel.FeedbacksPerProduct = await feedbackQuery
                .GroupBy(x => new { x.ProductId, x.Product.ProductName })
                .Select(x => new StatisticalSubList
                {
                    Id = (int)x.Key.ProductId,
                    Name = x.Key.ProductName.ToString(),
                    Count = x.Count()
                }).ToListAsync();

            viewModel.FeedbacksPerType = await feedbackQuery
                .GroupBy(x => new { x.TypeId, x.Type.TypeName })
                .Select(x => new StatisticalSubList
                {
                    Id = x.Key.TypeId,
                    Name = x.Key.TypeName.ToString(),
                    Count = x.Count()
                }).ToListAsync();

            return new BaseResponse<FeedbackCountsVm>(viewModel);
        }
    }
}
