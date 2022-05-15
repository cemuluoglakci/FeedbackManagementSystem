using ApplicationFMS.Handlers.Products.Commands.DeleteProduct;
using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.ComplaintTypes.Commands.DeleteComplaintType
{
    public class DeleteComplaintTypeCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }

        public class DeleteComplaintTypeCommandHandler : IRequestHandler<DeleteComplaintTypeCommand, BaseResponse>
        {
            private readonly IFMSDataContext _context;
            private readonly ICurrentUser? _currentUser;

            public DeleteComplaintTypeCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }
            public async Task<BaseResponse> Handle(DeleteComplaintTypeCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.FeedbackSubType.FindAsync(request.Id);
                if (entity == null)
                {
                    return BaseResponse.Fail("Related entity was not found.");
                }

                if (_currentUser.NotInRole(Constants.AdminRole))
                {
                    return BaseResponse.Fail("Only company representatives can delete products.");
                }

                entity.IsActive = false;

                await _context.SaveChangesAsync(cancellationToken);

                return new BaseResponse(entity.Id);
            }

        }
    }
}
