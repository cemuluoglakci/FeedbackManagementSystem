using ApplicationFMS.Models;
using MediatR;
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
            catch (Exception ex)
            {
                response = (TResponse)await HandleExceptionAsync(ex);
                //response = (TResponse)BaseResponse.Fail(errorMessage);
                //responseBase = new BaseResponseTest()
            }



            return response;
        }

        private async Task<BaseResponse> HandleExceptionAsync(Exception exception)
        {
            var resultMessage = string.Empty;

            BaseResponse baseResponse = new BaseResponse("");

            if (exception is ValidatorException validatorException)
            {
                resultMessage = validatorException.Message;
                baseResponse = new BaseResponse(validatorException.Failures, validatorException.Message);
                //var baseResponse = new BaseResponse<IDictionary<string, string[]>>
                //    (validatorException.Failures, validatorException.Message);
                //result = JsonSerializer.Serialize(validatorException.Failures);
            }



            if (resultMessage == string.Empty)
            {
                baseResponse = BaseResponse.Fail(exception.Message);
                //result = JsonConvert.SerializeObject(new { error = exception.Message });
                //result = JsonSerializer.Serialize(BaseResponse.Fail(exception.Message));
            }


            return baseResponse;
        }
    }
}
