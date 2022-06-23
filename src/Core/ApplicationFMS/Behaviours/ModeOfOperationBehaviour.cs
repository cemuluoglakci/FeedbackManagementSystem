using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models.Exceptions;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Behaviours
{
    public class ModeOfOperationBehaviour <TRequest, TResponse> : 
        IPipelineBehavior<TRequest, TResponse>
        where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public ModeOfOperationBehaviour(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var modeOfOperation = _context.System.FirstOrDefault(x => x.SystemVariable == Constants.SystemVariableModeName);
            string modeOfOperationName = _context.OperationMode.FirstOrDefault(x => x.Id == modeOfOperation.Value).ModeName;
            
            if ((_currentUser?.RequestPath == Constants.LoginRequestPath) || 
                (modeOfOperationName == Constants.OperationalModeName) ||
                _currentUser.IsInRole(Constants.AdminRole)
                )
            {
                return await next();
            }

            if (modeOfOperationName == Constants.MaintananceModeName)
            {
                if(_currentUser?.UserDetail?.RoleName is null)
                {
                    return await next();
                }
                else
                {
                    throw new ModeOfOperationException("Feedback Management System is currently in Maintenance Mode. Please try again later. Meanwhile you can still display feedback after logging out.");
                }
            }
            else
            {
                throw new ModeOfOperationException("Feedback Management System is currently in Degraded Mode. We are working hard to make our services available soon. Please try again later.");
            }
        }
    }
}
