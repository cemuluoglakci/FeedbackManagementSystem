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
            if (_currentUser.NotInRole(Constants.CustomerRole))
            {
                return new BaseResponse<int>(0, "If you want to contribute to the system with feedbacks please create a 'Customer' account with a dedicated E-mail address.");
            }

            Feedback entity;

            if (request.Id > 0)
            {
                entity = await _context.Feedback.FindAsync(request.Id.Value);
                if (!_currentUser.HasSameId(entity.UserId))
                {
                    return BaseResponse<int>.Fail("Users can only edit their own posts");
                }
            }
            else
            {
                entity = new Feedback
                {
                    UserId = _currentUser.UserDetail.Id,
                    Shared = 0,
                    LikeCount = 0,
                    DislikeCount = 0,
                    IsActive = true,
                    IsChecked = false,
                    IsReplied = false,
                    IsSolved = false,
                    IsArchived = false,
                    CreatedAt = DateTime.Now,
                    DirectedToEmployeeId = null
                };
                _context.Feedback.Add(entity);
            }

            entity.Title = request.Title;
            entity.Text = request.Text;
            entity.ProductId = request.ProductId;
            entity.TypeId = request.TypeId;
            entity.SubTypeId = request.SubTypeId;
            entity.IsAnonym = request.IsAnonym;

            entity.CompanyId = _context.Product.Find(request.ProductId).CompanyId;
            entity.SectorId = _context.Company.Find(entity.CompanyId).SectorId;

            await _context.SaveChangesAsync(cancellationToken);
            return new BaseResponse<int>(entity.Id);
        }
    }
}
