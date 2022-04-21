using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Feedbacks.Commands.UpsertFeedback
{
    public class UpsertFeedbackCommandHandler : IRequestHandler<UpsertFeedbackCommand, BaseResponse<int>>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public UpsertFeedbackCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<int>> Handle(UpsertFeedbackCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return new BaseResponse<int>(0, "User Identity could not defined.");
            }
            if (_currentUser.UserDetail.RoleName != Constants.CustomerRole)
            {
                return new BaseResponse<int>(0, "If you want to contribute to the system with feedbacks please create a 'Customer' account with a dedicated E-mail address.");
            }

            var entity = new Feedback
            {
                UserId = _currentUser.UserDetail.Id,
                Title = request.Title,
                Text = request.Text,
                ProductId = request.ProductId,
                TypeId = request.TypeId,
                SubTypeId = request.SubTypeId,
                Shared = 0,
                LikeCount = 0,
                DislikeCount = 0,
                IsAnonym = request.IsAnonym,
                IsActive = true,
                IsChecked = false,
                IsReplied = false,
                IsSolved = false,
                IsArchived = false,
                CreatedAt = DateTime.Now,
                DirectedToEmploteeId = null
            };
            entity.CompanyId = _context.Product.Find(request.ProductId).CompanyId;
            entity.SectorId = _context.Company.Find(entity.CompanyId).SectorId;

            _context.Feedback.Add(entity);
            await _context.SaveChangesAsync(cancellationToken);
            return new BaseResponse<int>(entity.Id);

        }



    }
}
