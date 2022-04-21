using ApplicationFMS.Handlers.Comments.Commands.ToggleActive;
using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ApplicationFMS.Helpers;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Handlers.System.Commands.SetOperationalMode
{
    public class SetOperationalModeCommandHandler : IRequestHandler<SetOperationalModeCommand, BaseResponse<string>>
    {
        private readonly IFMSDataContext _context;

        public SetOperationalModeCommandHandler(IFMSDataContext context)
        {
            _context = context;
        }

        public async Task<BaseResponse<string>> Handle(SetOperationalModeCommand request, CancellationToken cancellationToken)
        {
            string operationalModeName = _context.OperationMode.Find(request.Value).ModeName;
            
            if (string.IsNullOrEmpty(operationalModeName))
            {
                return BaseResponse<string>.ReturnFailureResponse("Operation mode not found!");
            }

            CoreFMS.Entities.System? systemVariable = _context.System.FirstOrDefault(x => x.SystemVariable == Constants.SystemVariableModeName);

            if (systemVariable == null)
            {
                return BaseResponse<string>.ReturnFailureResponse("System error!");
            }

            systemVariable.Value= request.Value;

            await _context.SaveChangesAsync(cancellationToken);

            return new BaseResponse<string>("Operational mode set as " + operationalModeName);
        }
    }
}
