using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using ApplicationFMS.Models.Exceptions;
using CoreFMS.Entities;
using MediatR;
using System;
using System.Linq;
using System.Linq.Dynamic.Core;
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

            User? entity = null;

            if (_context.User.Any(x => x.Email == request.Email))
            {
                entity = _context.User.FirstOrDefault(x => x.Email == request.Email);
                if (entity.IsActive & entity.IsVerified)
                {
                    return BaseResponse.Fail("This email is already registered to the system");
                }
            }
            
            if(entity == null)
            {
                entity = new User
                {
                    Email = request.Email,
                };
                _context.User.Add(entity);
            }

            entity.BirthDate = request.BirthDate;
            entity.CityId = request.CityId;
            entity.CompanyId = request.CompanyId;
            entity.EducationId = request.EducationId;
            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;
            entity.PhoneCode = request.PhoneCode;
            entity.Phone = request.Phone;
            entity.RoleId = request.RoleId;
            entity.Hash = hash;
            entity.Salt = salt;
            entity.IsVerified = false;
            entity.VerificationCode = Guid.NewGuid().ToString();
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

            
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("UNIQUE"))
                {
                    throw new DatabaseDataNotFoundException("message");
                }
                string exceptionMessage = string.Empty;
                exceptionMessage = ex?.InnerException?.Message;
                var newException = new Exception ("can not save");
                throw newException;
            }

            await _emailSender.SendRegistrationMailJetMail(entity);
            return new BaseResponse(entity.Id);
        }
    }
}
