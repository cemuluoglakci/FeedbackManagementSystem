using ApplicationFMS.Helpers.Mappings;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CoreFMS.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading;
using System.Threading.Tasks;
using static ApplicationFMS.Handlers.UserHandlers.Commands.UpdateUser.UpdateUserCommandHandler;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList
{
    public class GetPublicFeedbackListQueryHandler : IRequestHandler<GetPublicFeedbackListQuery, BaseResponse<PublicFeedbackListVm>>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser? _currentUser;

        public GetPublicFeedbackListQueryHandler(IFMSDataContext context, IMapper mapper, ICurrentUser? currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;

        }



        public async Task<BaseResponse<PublicFeedbackListVm>> Handle(GetPublicFeedbackListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Feedback>? feedbackQuery = _context.Feedback;

            // Instance count that current user is authorized to display
            int totalCount = feedbackQuery.Count();

            // Filter accourding to every query
            if (!String.IsNullOrEmpty(request.TitleQuery))
            {
                feedbackQuery = feedbackQuery.Where(x => x.Title.Contains(request.TitleQuery));
            }
            if (!String.IsNullOrEmpty(request.TextQuery))
            {
                feedbackQuery = feedbackQuery.Where(x => x.Text.Contains(request.TextQuery));
            }
            if (request.CreatedAtBefore != null)
            {
                feedbackQuery = feedbackQuery.Where(x => (x.CreatedAt) < request.CreatedAtBefore);
            }
            if (request.CreatedAtAfter != null)
            {
                feedbackQuery = feedbackQuery.Where(x => (x.CreatedAt) > request.CreatedAtAfter);
            }
            if ((request.SectorId ?? 0) != 0)
            {
                feedbackQuery = feedbackQuery.Where(x => x.SectorId == request.SectorId);
            }
            if ((request.CompanyId ?? 0) != 0)
            {
                feedbackQuery = feedbackQuery.Where(x => x.CompanyId == request.CompanyId);
            }
            if ((request.ProductId ?? 0) != 0)
            {
                feedbackQuery = feedbackQuery.Where(x => x.ProductId == request.ProductId);
            }
            if ((request.TypeId ?? 0) != 0)
            {
                feedbackQuery = feedbackQuery.Where(x => x.TypeId == request.TypeId);
            }
            if ((request.SubTypeId ?? 0) != 0)
            {
                feedbackQuery = feedbackQuery.Where(x => x.SubTypeId == request.SubTypeId);
            }

            int filteredCount = feedbackQuery.Count();

            //Transfering type to Data Transfer Object
            var dtoQuery = feedbackQuery.ProjectTo<PublicFeedbackDTO>(_mapper.ConfigurationProvider);

            //Ordering
            string sortColumnDirection = request.IsAscending ? "ascending" : "descending";
            dtoQuery = dtoQuery.OrderBy(request.SortColumn + " " + sortColumnDirection);

            //Pagination and Calling the query
            int take = request.ObjectsPerPage;
            int skip = (request.PageNumber - 1) * take;
            var feedbacks = await dtoQuery.Skip(skip).Take(take).ToListAsync(cancellationToken);

            var viewModel = new PublicFeedbackListVm
            {
                PublicFeedbackList = feedbacks,
                TotalCount = totalCount,
                FilteredCount = filteredCount,
                ObjectsPerPage = take,
                PageNumber = request.PageNumber,
            };

            return new BaseResponse<PublicFeedbackListVm>(viewModel);
        }

    }
}
