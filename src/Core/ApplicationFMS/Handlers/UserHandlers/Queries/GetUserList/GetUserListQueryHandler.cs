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

namespace ApplicationFMS.Handlers.UserHandlers.Queries.GetUserList
{
    public class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, BaseResponse<UserListVm>>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser? _currentUser;

        public GetUserListQueryHandler(IFMSDataContext context, IMapper mapper, ICurrentUser? currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<UserListVm>> Handle(GetUserListQuery request, CancellationToken cancellationToken)
        {
            IQueryable<User>? userQuery = _context.User;

            if (_currentUser == null)
            {
                return new BaseResponse<UserListVm>(null, "User Identity could not defined.");
            }

            //Company representatives will be allowed to display only users related to their company.
            if (_currentUser.UserDetail.RoleName == "Company Representative")
            {
                userQuery = userQuery.Where(x => x.CompanyId == _currentUser.UserDetail.CompanyId);
            }
            else if (_currentUser.UserDetail.RoleName != "System Administrator")
            {
                return new BaseResponse<UserListVm>(null, "User role is not authorized.");
            }

            // Instance count that current user is authorized to display
            int totalCount = userQuery.Count();

            // Filter accourding to every query
            if (!String.IsNullOrEmpty(request.EmailQuery))
            {
                userQuery = userQuery.Where(x => x.Email.Contains(request.EmailQuery));
            }
            if (!String.IsNullOrEmpty(request.FirstNameQuery))
            {
                userQuery = userQuery.Where(x => (x.FirstName ?? "").Contains(request.FirstNameQuery));
            }
            if (!String.IsNullOrEmpty(request.LastNameQuery))
            {
                userQuery = userQuery.Where(x => (x.LastName ?? "").Contains(request.LastNameQuery));
            }
            if (request.BirthDateBefore != null)
            {
                userQuery = userQuery.Where(x => (x.BirthDate ?? null) < request.BirthDateBefore);
            }
            if (request.BirthDateAfter != null)
            {
                userQuery = userQuery.Where(x => (x.BirthDate ?? null) > request.BirthDateAfter);
            }
            if (request.RegisteredAtBefore != null)
            {
                userQuery = userQuery.Where(x => (x.RegisteredAt ?? null) < request.RegisteredAtBefore);
            }
            if (request.RegisteredAtAfter != null)
            {
                userQuery = userQuery.Where(x => (x.RegisteredAt ?? null) > request.RegisteredAtAfter);
            }
            if ((request.CityId ?? 0) != 0)
            {
                userQuery = userQuery.Where(x => x.CityId == request.CityId);
            }
            if ((request.EducationId ?? 0) != 0)
            {
                userQuery = userQuery.Where(x => x.EducationId == request.EducationId);
            }
            if ((request.CompanyId ?? 0) != 0)
            {
                userQuery = userQuery.Where(x => x.CompanyId == request.CompanyId);
            }
            if (request.IsActive != null)
            {
                userQuery = userQuery.Where(x => x.IsActive == request.IsActive);
            }
            if (request.IsTwoFactorAuth != null)
            {
                userQuery = userQuery.Where(x => x.IsTwoFactorAuth == request.IsTwoFactorAuth);
            }

            int filteredCount = userQuery.Count();

            //Transfering type to Data Transfer Object
            var dtoQuery = userQuery.ProjectTo<UserDTO>(_mapper.ConfigurationProvider);

            //Ordering
            string sortColumnDirection = request.IsAscending ? "ascending" : "descending";
            dtoQuery = dtoQuery.OrderBy(request.SortColumn + " " + sortColumnDirection);

            //Pagination and Calling the query
            int take = request.ObjectsPerPage;
            int skip = (request.PageNumber - 1) * take;
            var users = await dtoQuery.Skip(skip).Take(take).ToListAsync(cancellationToken);

            var viewModel = new UserListVm
            {
                UserList = users,
                TotalCount = totalCount,
                FilteredCount = filteredCount,
                ObjectsPerPage = take,
                PageNumber = request.PageNumber,
            };

            return new BaseResponse<UserListVm>(viewModel);
        }
    }
}
