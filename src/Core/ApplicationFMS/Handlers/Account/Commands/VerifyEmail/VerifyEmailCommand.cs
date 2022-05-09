using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.Account.Commands.VerifyEmail
{
    public class VerifyEmailCommand : IRequest<string>
    {
        public string Email { get; set; } = null!;
        public string VerificationCode { get; set; } = null!;
    }
}
