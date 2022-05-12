using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Product.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<BaseResponse<int>>
    {
        public int Id { get; set; }

        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, BaseResponse<int>>
        {
            private readonly IFMSDataContext _context;
            private readonly ICurrentUser? _currentUser;

            public DeleteProductCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
            {
                _context = context;
                _currentUser = currentUser;
            }
            public async Task<BaseResponse<int>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Product.FindAsync(request.Id);
                if (entity == null)
                {
                    return BaseResponse<int>.Fail("Related entity was not found.");
                }

                if (_currentUser.NotInRole(Constants.CompanyRepresentativeRole))
                {
                    return BaseResponse<int>.Fail("Only company representatives can delete products.");
                }
                if (_currentUser.UserDetail.CompanyId != entity.CompanyId)
                {
                    return BaseResponse<int>.Fail("Users can only delete their own company's products.");
                }

                entity.IsActive = false;

                await _context.SaveChangesAsync(cancellationToken);

                return new BaseResponse<int>(entity.Id);
            }

        }
    }
}
