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
using static ApplicationFMS.Handlers.UserHandlers.Commands.UpdateUser.UpdateUserCommandHandler;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList
{
    public class GetFeedbackListQueryHandler : IRequestHandler<GetFeedbackListQuery, BaseResponse<FeedbackListVm>>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser? _currentUser;

        public GetFeedbackListQueryHandler(IFMSDataContext context, IMapper mapper, ICurrentUser? currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;

        }

        public async Task<BaseResponse<FeedbackListVm>> Handle(GetFeedbackListQuery request, CancellationToken cancellationToken)
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
            if (request.IsReplied != null)
            {
                feedbackQuery = feedbackQuery.Where(x => x.IsReplied == request.IsReplied);
            }
            if (request.IsSolved != null)
            {
                feedbackQuery = feedbackQuery.Where(x => x.IsSolved == request.IsSolved);
            }

            int filteredCount = feedbackQuery.Count();

            var viewModel = new FeedbackListVm
            {
                TotalCount = totalCount,
                FilteredCount = filteredCount,
                ObjectsPerPage = request.ObjectsPerPage,
                PageNumber = request.PageNumber,
            };


            //Adjusting filters and data tpes according to current user's role

            //Apply common filters between Admin and company users
            if (_currentUser.UserDetail.RoleName == PreDefinedTypes._adminRole || PreDefinedTypes._companyRoles.Contains(_currentUser.UserDetail.RoleName))
            {
                if (request.IsArchived != null)
                {
                    feedbackQuery = feedbackQuery.Where(x => x.IsArchived == request.IsArchived);
                }
                if (request.IsDirected != null)
                {
                    if ((bool)request.IsDirected)
                    {
                        feedbackQuery = feedbackQuery.Where(x => x.DirectedToEmploteeId != null);
                    }
                    else
                    {
                        feedbackQuery = feedbackQuery.Where(x => x.DirectedToEmploteeId == null);
                    }

                }
            }

            if (_currentUser.UserDetail.RoleName == PreDefinedTypes._adminRole)
            {
                //Apply extra filters which are only specific to Admins
                if (request.IsActive != null)
                {
                    feedbackQuery = feedbackQuery.Where(x => x.IsActive == request.IsActive);
                }

                if (request.IsChecked != null)
                {
                    feedbackQuery = feedbackQuery.Where(x => x.IsChecked == request.IsChecked);
                }

                if (request.IsReplied != null)
                {
                    feedbackQuery = feedbackQuery.Where(x => x.IsReplied == request.IsReplied);
                }

                if (request.IsArchived != null)
                {
                    feedbackQuery = feedbackQuery.Where(x => x.IsArchived == request.IsArchived);
                }
                viewModel.FilteredCount = feedbackQuery.Count();

                //Project to DTO type
                var dtoQuery = feedbackQuery.ProjectTo<AdminFeedbackDTO>(_mapper.ConfigurationProvider);
                //Pagination and ordering
                dtoQuery = Tools.ArrangeList(dtoQuery, request.SortColumn, request.IsAscending, request.ObjectsPerPage, request.PageNumber);
                var feedbackList = await dtoQuery.ToListAsync();
                viewModel.AdminFeedbackList = feedbackList;
            }
            else if (PreDefinedTypes._companyRoles.Contains(_currentUser.UserDetail.RoleName))
            {
                viewModel.FilteredCount = feedbackQuery.Count();

                //Project to DTO type
                var dtoQuery = feedbackQuery.ProjectTo<CompanyFeedbackDTO>(_mapper.ConfigurationProvider);
                //Pagination and ordering
                dtoQuery = Tools.ArrangeList(dtoQuery, request.SortColumn, request.IsAscending, request.ObjectsPerPage, request.PageNumber);
                var feedbackList = await dtoQuery.ToListAsync();
                viewModel.CompanyFeedbackList = feedbackList;
            }
            else
            {
                var dtoQuery = feedbackQuery.ProjectTo<PublicFeedbackDTO>(_mapper.ConfigurationProvider);
                //Pagination and ordering
                dtoQuery = Tools.ArrangeList(dtoQuery, request.SortColumn, request.IsAscending, request.ObjectsPerPage, request.PageNumber);
                var feedbackList = await dtoQuery.ToListAsync();
                viewModel.PublicFeedbackList = feedbackList;
            }




            return new BaseResponse<FeedbackListVm>(viewModel);
        }

    }
}
