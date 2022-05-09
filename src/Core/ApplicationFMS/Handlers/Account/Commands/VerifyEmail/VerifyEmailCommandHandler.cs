using ApplicationFMS.Handlers.Account.Commands.RegisterUser;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Account.Commands.VerifyEmail
{
    public class VerifyEmailCommandHandler : IRequestHandler<VerifyEmailCommand, string>
    {
        private readonly IFMSDataContext _context;

        public VerifyEmailCommandHandler(IFMSDataContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(VerifyEmailCommand request, CancellationToken cancellationToken)
        {
            User? user = _context.User.FirstOrDefault(x => 
                x.Email == request.Email && !x.IsVerified);

            if (user == null)
            {
                return "No active account found waiting for verification with given E-mail address";
            }
            if (user.VerificationCode == request.VerificationCode)
            {
                user.IsVerified = true;
                await _context.SaveChangesAsync(cancellationToken);
                return String.Format("E-mail address {0} verified. We wish you will enjoy your experience with FMS!", user.Email);
            }
            else
            {
                return String.Format("E-mail address {0} was not verified", user.Email);
            }
        }
    }
}
