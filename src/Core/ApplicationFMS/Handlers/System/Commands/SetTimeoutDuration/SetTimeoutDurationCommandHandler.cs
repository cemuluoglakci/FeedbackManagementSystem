using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.System.Commands.SetTimeoutDuration
{
    public class SetTimeoutDurationCommandHandler : IRequestHandler<SetTimeoutDurationCommand, BaseResponse>
    {
        private readonly IFMSDataContext _context;

        public SetTimeoutDurationCommandHandler(IFMSDataContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse> Handle(SetTimeoutDurationCommand request, CancellationToken cancellationToken)
        {
            CoreFMS.Entities.System? systemVariable = _context.System.FirstOrDefault(x => x.SystemVariable == Constants.SystemVariableTimeoutName);

            if (systemVariable == null)
            {
                return BaseResponse.Fail("System error!");
            }

            systemVariable.Value = request.Value;

            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse("Timeout duration set as " + Convert.ToString(systemVariable.Value));
        }
    }
}
