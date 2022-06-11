using ApplicationFMS.Models;
using MediatR;

namespace ApplicationFMS.Handlers.Account.Commands.VerifyEmail
{
    public class VerifyEmailCommand : IRequest<BaseResponse>
    {
        public string Email { get; set; } = null!;
        public string VerificationCode { get; set; } = null!;
    }
}
