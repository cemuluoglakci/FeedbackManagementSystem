using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Account.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, BaseResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly IEmailSender _emailSender;


        public RegisterUserCommandHandler(IFMSDataContext context, IEmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender;
        }

        public async Task<BaseResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            string salt = Security.GenerateSalt();
            string hash = Security.SaltAndHashPassword(request.Password, salt);

            var entity = new User
            {
                BirthDate = request.BirthDate,
                CityId = request.CityId,
                CompanyId = request.CompanyId,
                EducationId = request.EducationId,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                PhoneCode = request.PhoneCode,
                Phone = request.Phone,
                RoleId = request.RoleId,
                Hash = hash,
                Salt = salt,
                IsVerified = false,
                VerificationCode = Guid.NewGuid().ToString()
            };
            entity.RegisteredAt = DateTime.Now;

            if (request.RoleId == 0)
            {
                return BaseResponse.Fail ("User can not register without 'Role' information.");
            }

            string roleName = _context.Role.Find(request.RoleId).RoleName;
            if (roleName == "Customer")
            {
                entity.IsActive = true;
            }
            else { entity.IsActive = false; }

            _context.User.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);
            await _emailSender.SendRegistrationMail(entity);
            return new BaseResponse(entity);
        }


    }
}
