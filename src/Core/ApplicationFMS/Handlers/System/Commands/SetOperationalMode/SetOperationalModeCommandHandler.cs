using ApplicationFMS.Helpers;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.System.Commands.SetOperationalMode
{
    public class SetOperationalModeCommandHandler : IRequestHandler<SetOperationalModeCommand, BaseResponse>
    {
        private readonly IFMSDataContext _context;

        public SetOperationalModeCommandHandler(IFMSDataContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse> Handle(SetOperationalModeCommand request, CancellationToken cancellationToken)
        {
            OperationMode? operationalMode = _context.OperationMode.Find(request.ModeId);

            if (operationalMode == null)
            {
                return BaseResponse.Fail("Operation mode not found!");
            }

            CoreFMS.Entities.System? systemVariable = _context.System.FirstOrDefault(x => x.SystemVariable == Constants.SystemVariableModeName);

            if (systemVariable == null)
            {
                return BaseResponse.Fail("System error!");
            }

            systemVariable.Value = request.ModeId;

            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse("Operational mode set as " + operationalMode.ModeName);
        }
    }
}
