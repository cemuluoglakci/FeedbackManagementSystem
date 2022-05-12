using ApplicationFMS.Models;
using MediatR;
using System.Text.Json.Serialization;

namespace ApplicationFMS.Handlers.Account.Queries.UserLogin
{
    public class UserLoginQuery : IRequest<BaseResponse<string>>
    {
        
        public string Email { get; set; } = null!;
        [JsonIgnore]
        public string Password { get; set; } = null!;
    }
}
