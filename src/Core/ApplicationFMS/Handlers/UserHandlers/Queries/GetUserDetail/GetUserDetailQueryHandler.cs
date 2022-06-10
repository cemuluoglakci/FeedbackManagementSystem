using ApplicationFMS.Handlers.UserHandlers.Queries.GetUserList;
using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ApplicationFMS.Models.Exceptions;

namespace ApplicationFMS.Handlers.UserHandlers.Queries.GetUserDetail
{
    public class GetUserDetailQueryHandler : IRequestHandler<GetUserDetailQuery, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser? _currentUser;
        private int _searchUserId;

        public GetUserDetailQueryHandler(IFMSDataContext context, IMapper mapper, ICurrentUser? currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse> Handle(GetUserDetailQuery request, CancellationToken cancellationToken)
        {
            AssignSearchUserId(request);

            var vmQuery = _context.User.Where(e => e.Id == _searchUserId && e.IsActive);
            var vm = await vmQuery.ProjectTo<UserDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (vm == null) throw new NotFoundException("Entity not found.");

            CheckCurrentUserEligibility();

            return new BaseResponse(vm);

        }

        private void AssignSearchUserId(GetUserDetailQuery request)
        {
            if (request.Id > 0)
                _searchUserId = (int)request.Id;
            else
                _searchUserId = _currentUser.UserDetail.Id;
        }

        private void CheckCurrentUserEligibility()
        {
            if (_currentUser.NotInRole(Constants.AdminRole) && _currentUser.UserDetail.Id != _searchUserId)
                throw new UnauthorizedException();
        }
    }
}