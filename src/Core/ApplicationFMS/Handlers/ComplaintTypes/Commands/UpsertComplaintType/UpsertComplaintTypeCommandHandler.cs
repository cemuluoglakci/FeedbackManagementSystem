using ApplicationFMS.Handlers.Products.Commands.UpsertProduct;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System.Threading.Tasks;
using System.Threading;
using ApplicationFMS.Helpers;

namespace ApplicationFMS.Handlers.ComplaintTypes.Commands.UpsertComplaintType
{
    public class UpsertComplaintTypeCommandHandler : IRequestHandler<UpsertComplaintTypeCommand, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public UpsertComplaintTypeCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse> Handle(UpsertComplaintTypeCommand request, CancellationToken cancellationToken)
        {

            if (_currentUser.NotInRole(Constants.AdminRole))
            {
                return BaseResponse.Fail("Only administrators can add complaint types.");
            }

            FeedbackSubType entity;

            if (request.Id > 0)
            {
                entity = await _context.FeedbackSubType.FindAsync(request.Id.Value);
            }
            else
            {
                entity = new FeedbackSubType()
                {
                    IsActive = true
                };
                _context.FeedbackSubType.Add(entity);
            }

            entity.SubTypeName = request.SubTypeName;

            await _context.SaveChangesAsync(cancellationToken);
            return new BaseResponse(entity.Id);
        }

    }
}