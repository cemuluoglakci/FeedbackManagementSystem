using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Products.Commands.UpsertProduct
{
    public class UpsertProductCommandHandler : IRequestHandler<UpsertProductCommand, BaseResponse<int>>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public UpsertProductCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<BaseResponse<int>> Handle(UpsertProductCommand request, CancellationToken cancellationToken)
        {

            if (_currentUser.NotInRole(Constants.CompanyRepresentativeRole))
            {
                return BaseResponse<int>.Fail("Only company representatives can add products.");
            }

            Product entity;

            if (request.Id > 0)
            {
                entity = await _context.Product.FindAsync(request.Id.Value);

                if (_currentUser.UserDetail.CompanyId != entity.CompanyId)
                {
                    return BaseResponse<int>.Fail("Users can only edit their own company's products.");
                }
            }
            else
            {
                entity = new Product()
                {
                    CompanyId = _currentUser.UserDetail.CompanyId,
                    IsActive = true
                };
                _context.Product.Add(entity);
            }

            entity.ProductName = request.ProductName;

            await _context.SaveChangesAsync(cancellationToken);
            return new BaseResponse<int>(entity.Id);
        }

    }
}