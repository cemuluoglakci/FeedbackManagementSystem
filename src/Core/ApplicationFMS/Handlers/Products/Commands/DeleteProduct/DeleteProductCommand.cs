using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<BaseResponse>
    {
        public int Id { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, BaseResponse>
        {
            private readonly IFMSDataContext _context;
            private readonly ICurrentUser? _currentUser;

            public DeleteProductCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }
            public async Task<BaseResponse> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Product.FindAsync(request.Id);
                if (entity == null)
                {
                    return BaseResponse.Fail("Related entity was not found.");
                }

                if (_currentUser.NotInRole(Constants.CompanyRepresentativeRole))
                {
                    return BaseResponse.Fail("Only company representatives can delete products.");
                }
                if (_currentUser.UserDetail.CompanyId != entity.CompanyId)
                {
                    return BaseResponse.Fail("Users can only delete their own company's products.");
                }

                entity.IsActive = false;

                await _context.SaveChangesAsync(cancellationToken);

                return new BaseResponse(entity.Id);
            }

        }
    }
}
