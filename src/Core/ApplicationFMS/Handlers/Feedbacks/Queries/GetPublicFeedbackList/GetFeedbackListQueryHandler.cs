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

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList
{
    public class GetFeedbackListQueryHandler : IRequestHandler<GetFeedbackListQuery, BaseResponse>
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

        public async Task<BaseResponse> Handle(GetFeedbackListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<Feedback>? feedbackQuery = _context.Feedback;

            // Instance count that current user is authorized to display
            int totalCount = feedbackQuery.Count();
            int activeCount = feedbackQuery.Where(x => x.IsActive == true).Count();
            if (_currentUser.UserDetail.RoleName == Constants.CompanyEmployeeRole)
            {
                activeCount = feedbackQuery.Where(x =>
                x.IsActive == true &&
                x.DirectedToEmployeeId == _currentUser.UserDetail.Id).Count();
            }

            // Filter accourding to every query
            if (String.IsNullOrEmpty(request.SortColumn))
            {
                request.SortColumn = "Id";
            }
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
            if (request.UserId > 0)
            {
                feedbackQuery = feedbackQuery.Where(x => x.UserId == request.UserId);
                if (_currentUser.UserDetail.Id != request.UserId)
                {
                    feedbackQuery = feedbackQuery.Where(x => x.IsAnonym == false);
                }
            }
            if (request.IsReplied != null)
            {
                feedbackQuery = feedbackQuery.Where(x => x.IsReplied == request.IsReplied);
            }
            if (request.IsSolved != null)
            {
                feedbackQuery = feedbackQuery.Where(x => x.IsSolved == request.IsSolved);
            }

            var viewModel = new FeedbackListVm
            {
                TotalCount = activeCount,
                ObjectsPerPage = request.ObjectsPerPage,
                PageNumber = request.PageNumber,
            };

            //Adjusting filters and data types according to current user's role

            //Apply common filters between Admin and company users
            if (_currentUser.UserDetail.RoleName == Constants.AdminRole || Constants.CompanyRoles.Contains(_currentUser.UserDetail.RoleName))
            {
                if (request.IsArchived != null)
                {
                    feedbackQuery = feedbackQuery.Where(x => x.IsArchived == request.IsArchived);
                }
                if (request.IsDirected != null)
                {
                    if ((bool)request.IsDirected)
                    {
                        feedbackQuery = feedbackQuery.Where(x => x.DirectedToEmployeeId != null);
                    }
                    else
                    {
                        feedbackQuery = feedbackQuery.Where(x => x.DirectedToEmployeeId == null);
                    }

                }
            }

            if (_currentUser.UserDetail.RoleName == Constants.AdminRole)
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
                viewModel.TotalCount = totalCount;
                viewModel.FilteredCount = feedbackQuery.Count();

                //Project to DTO type
                var dtoQuery = feedbackQuery.ProjectTo<AdminFeedbackDTO>(_mapper.ConfigurationProvider);
                //Pagination and ordering
                dtoQuery = Tools.ArrangeList(dtoQuery, request.SortColumn, request.IsAscending, request.ObjectsPerPage, request.PageNumber);
                var feedbackList = await dtoQuery.ToListAsync();
                viewModel.AdminFeedbackList = feedbackList;
            }
            else if (Constants.CompanyRoles.Contains(_currentUser.UserDetail.RoleName))
            {
                feedbackQuery = feedbackQuery.Where(x => x.IsActive &&
                    x.CompanyId == _currentUser.UserDetail.CompanyId);

                //Company Employees can only display feedbacks directed to them
                if (_currentUser.UserDetail.RoleName == Constants.CompanyEmployeeRole)
                {
                    feedbackQuery = feedbackQuery.Where(x => x.DirectedToEmployeeId == _currentUser.UserDetail.Id);
                }
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
                feedbackQuery = feedbackQuery.Where(x => x.IsActive == true);

                if (request.IsMine == true && _currentUser?.UserDetail?.Id > 0)
                {
                    feedbackQuery = feedbackQuery.Where(x => x.UserId == _currentUser.UserDetail.Id);
                }

                viewModel.FilteredCount = feedbackQuery.Count();

                var dtoQuery = feedbackQuery.ProjectTo<PublicFeedbackDTO>(_mapper.ConfigurationProvider);

                //Pagination and ordering
                dtoQuery = Tools.ArrangeList(dtoQuery, request.SortColumn, request.IsAscending, request.ObjectsPerPage, request.PageNumber);

                var feedbackList = await dtoQuery.ToListAsync();

                if (_currentUser?.UserDetail?.Id != null)
                {
                    //Add User reaction information of feedbacks
                    var userReactions = _context.ReactionFeedback
                        .Where(x => x.IsActive && x.UserId == _currentUser.UserDetail.Id);
                    var reactedFeedbackIds = userReactions.Select(x => x.FeedbackId).ToList();

                    if (reactedFeedbackIds != null)
                    {
                        var reactedFeedbackList = feedbackList.Where(x => reactedFeedbackIds.Contains(x.Id)).ToList();
                        reactedFeedbackList.ForEach(x => x.UserReaction = userReactions.FirstOrDefault(y => y.FeedbackId == x.Id).Sentiment);
                    }

                    //Add "IsMine" information of feedbacks
                    // ***Refactor later***

                    var userFeedbackIds = _context.Feedback
                        .Where(x => x.UserId == _currentUser.UserDetail.Id)
                        .Select(x => x.Id).ToList();

                    if (userFeedbackIds != null)
                    {
                        var postedFeedbackList = feedbackList.Where(x => userFeedbackIds.Contains(x.Id)).ToList();
                        postedFeedbackList.ForEach(x => x.IsMine = true);
                    }
                }

                viewModel.PublicFeedbackList = feedbackList;
            }

            return new BaseResponse(viewModel);
        }

    }
}
