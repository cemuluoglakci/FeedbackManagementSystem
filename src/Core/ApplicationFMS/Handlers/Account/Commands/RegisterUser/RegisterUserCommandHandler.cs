using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using AutoMapper;
using CoreFMS.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Account.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, User>
    {
        private readonly IFMSDataContext _context;


        public RegisterUserCommandHandler(IFMSDataContext context)
        {
            _context = context;
        }

        public async Task<User> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
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
                Phone = request.Phone,
                RoleId = request.RoleId,
                Hash = hash,
                Salt = salt,
            };

            if (request.RoleId == 2)
            {
                entity.IsActive = 1;
            }

            _context.User.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity;
        }


    }
}
