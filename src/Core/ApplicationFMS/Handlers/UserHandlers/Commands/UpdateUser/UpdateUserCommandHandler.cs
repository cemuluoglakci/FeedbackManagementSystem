using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.UserHandlers.Commands.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, BaseResponse<int>>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public UpdateUserCommandHandler(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }
        public async Task<BaseResponse<int>> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            if (_currentUser == null)
            {
                return new BaseResponse<int>(0, "User Identity could not defined.");
            }
            if (_currentUser.UserDetail.Id != request.Id)
            {
                return new BaseResponse<int>(0, "Users are only allowed to update their own account.");
            }

            User entity = await _context.User.FindAsync(request.Id);

            if (!String.IsNullOrEmpty(request.Email))
            {
                entity.Email = request.Email;
            }
            if (!String.IsNullOrEmpty(request.FirstName))
            {
                entity.FirstName = request.FirstName;
            }
            if (!String.IsNullOrEmpty(request.LastName))
            {
                entity.LastName = request.LastName;
            }
            //if (!String.IsNullOrEmpty(request.PhoneCode))
            //{
            //    entity.PhoneCode = request.PhoneCode;
            //}
            if (!String.IsNullOrEmpty(request.Phone))
            {
                entity.Phone = request.Phone;
            }
            if ((request.CityId ?? 0) != 0)
            {
                entity.CityId = request.CityId;
            }
            if ((request.EducationId ?? 0) != 0)
            {
                entity.EducationId = request.EducationId;
            }

            if (!String.IsNullOrEmpty(request.Password))
            {
                string salt = Security.GenerateSalt();
                string hash = Security.SaltAndHashPassword(request.Password, salt);
                entity.Salt = salt;
                entity.Hash = hash;
            }

            await _context.SaveChangesAsync(cancellationToken);
            return new BaseResponse<int>(entity.Id);
        }
    }
}
