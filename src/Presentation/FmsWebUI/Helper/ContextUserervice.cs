using System.Linq;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FmsWebUI.Helper
{
    public interface IContextUserService
    {
        ContextUser GetContextUser(int id);
    }

    public class ContextUserService : IContextUserService
    {

        private IFMSDataContext _context;
        private ITokenHelper _tokenHelper;
        private readonly JwtSetting _jwtSetting;
        private readonly IMapper _mapper;

        public ContextUserService(IFMSDataContext context, ITokenHelper tokenHelper, IOptions<JwtSetting> jwtSetting, IMapper mapper)
        {
            _context = context;
            _tokenHelper = tokenHelper;
            _jwtSetting = jwtSetting.Value;
            _mapper = mapper;
        }

        public ContextUser GetContextUser(int id)
        {
            var currentUser = _context.User.Include(u => u.Role).FirstOrDefault(x => x.Id == id && x.IsActive);
            var currentContextUser = _mapper.Map<ContextUser>(currentUser);
            return currentContextUser;
        }
    }
}
