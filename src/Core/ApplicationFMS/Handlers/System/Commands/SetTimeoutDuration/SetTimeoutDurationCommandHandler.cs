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
    public class SetTimeoutDurationCommandHandler : IRequestHandler<SetTimeoutDurationCommand, BaseResponse<string>>
    {
        private readonly IFMSDataContext _context;

        public SetTimeoutDurationCommandHandler(IFMSDataContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<string>> Handle(SetTimeoutDurationCommand request, CancellationToken cancellationToken)
        {
            CoreFMS.Entities.System? systemVariable = _context.System.FirstOrDefault(x => x.SystemVariable == Constants.SystemVariableTimeoutName);

            if (systemVariable == null)
            {
                return BaseResponse<string>.Fail("System error!");
            }

            systemVariable.Value = request.Value;

            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<string>("Timeout duration set as " + Convert.ToString(systemVariable.Value));
        }
    }
}
