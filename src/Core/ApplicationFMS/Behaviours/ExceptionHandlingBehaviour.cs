using ApplicationFMS.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ApplicationFMS.Behaviours
{
    public class ExceptionHandlingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : BaseResponse
        where TRequest : MediatR.IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response;
            try
            {
                response = await next();
            }
            catch (DbUpdateException ex)
            {
                response = (TResponse)await HandleExceptionAsync(ex);
            }
            catch (Exception ex)
            {
                response = (TResponse)await HandleExceptionAsync(ex);
            }
            return response;
        }

        private async Task<BaseResponse> HandleExceptionAsync(Exception exception)
        {
            BaseResponse baseResponse = BaseResponse.Fail("");

            if (exception is ValidatorException validatorException)
            {
                baseResponse = new BaseResponse(validatorException.Failures, validatorException.Message);
            }

            if (baseResponse.Meta.Message == string.Empty)
            {
                baseResponse.Meta.Message = exception.Message;
                if (exception.InnerException != null)
                {
                    baseResponse.Meta.Message = baseResponse.Meta.Message + ": " + exception.InnerException.Message;
                }
            }

            return await Task.FromResult(baseResponse);
        }
    }
}
