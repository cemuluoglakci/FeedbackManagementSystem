using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackDetail;
using ApplicationFMS.Handlers.Feedbacks.Queries.GetPublicFeedbackList;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Feedbacks.Queries.GetPlainFeedback
{
    public class GetPlainFeedbackQueryHandler: IRequestHandler<GetPlainFeedbackQuery, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly IMapper _mapper;
        private readonly ICurrentUser? _currentUser;

        public GetPlainFeedbackQueryHandler(IFMSDataContext context, IMapper mapper, ICurrentUser? currentUser)
        {
            _context = context;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse> Handle(GetPlainFeedbackQuery request, CancellationToken cancellationToken)
        {
            var vmQuery = _context.Feedback.Where(e => e.Id == request.Id && e.IsActive);

            var vm = await vmQuery.ProjectTo<PublicFeedbackDTO>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync(cancellationToken);

            if (vm == null)
            {
                return BaseResponse.Fail("No active feedback found.");
            }

            if (_currentUser?.UserDetail?.Id != null)
            {
                var userReaction = _context.ReactionFeedback
                    .FirstOrDefault(x => x.IsActive && 
                    x.UserId == _currentUser.UserDetail.Id &&
                    x.FeedbackId == vm.Id);

                vm.UserReaction = userReaction?.Sentiment;

                if(_currentUser.UserDetail.Id == vmQuery.First().UserId)
                {
                    vm.IsMine = true;
                }
            }

            return new BaseResponse(vm);

        }

    }
}

