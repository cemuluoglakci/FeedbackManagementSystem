using ApplicationFMS.Models;
using MediatR;
using MediatR.Pipeline;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.NetworkInformation;
using Microsoft.Extensions.Logging;

namespace ApplicationFMS.Behaviours
{
    public class ExceptionBehaviourTest<TRequest, TResponse, TException> : IRequestExceptionHandler<TRequest, TResponse, TException>
    where TRequest : IRequest<TResponse>
    where TException : Exception
    where TResponse : ServiceResponse
    {
        public Task Handle(TRequest request, TException exception, RequestExceptionHandlerState<TResponse> state, CancellationToken cancellationToken)
        {

            var error = CreateExceptionError(exception);

            //logger.LogError(JsonSerializer.Serialize(error));

            var response = ServiceResponse.CreateExceptionError();

            state.SetHandled(response as TResponse);

            return Task.FromResult(response);

            //state.SetHandled(default(TResponse));

            //var code = HttpStatusCode.InternalServerError;

            //var result = string.Empty;

            //if (exception is ValidatorException validatorException)
            //{
            //    code = HttpStatusCode.BadRequest;
            //    var baseResponse = new BaseResponse<IDictionary<string, string[]>>
            //        (validatorException.Failures, validatorException.Message);
            //    result = JsonSerializer.Serialize(baseResponse);
            //}

            ////context.Response.ContentType = "application/json";
            ////context.Response.StatusCode = (int)code;

            //if (result == string.Empty)
            //{
            //    //result = JsonConvert.SerializeObject(new { error = exception.Message });
            //    result = JsonSerializer.Serialize(BaseResponse.Fail(exception.Message));
            //}

            ////SaveExceptionLog(context.Request, result);

            ////return context.Response.WriteAsync(result);
            //return Task.FromResult("test result");
        }


        private static ExceptionError CreateExceptionError(TException exception)
        {
            var methodName = exception.TargetSite?.DeclaringType?.DeclaringType?.FullName;
            var message = exception.Message;
            var innerException = exception.InnerException?.Message;
            var stackTrace = exception.StackTrace;

            return new ExceptionError(methodName, message, innerException, stackTrace);
        }

    }

    public class Pong
    {
        public string Message { get; set; }
    }

    public class ServiceResponse
    {
        public bool Success { get; init; }
        public string? Message { get; init; }
        public object? Payload { get; init; }

        public static ServiceResponse CreateSuccess(string? message)
        {
            return CreateServiceResponse(true, message, null);
        }

        public static ServiceResponse CreateSuccess(string? message, object? data)
        {
            return CreateServiceResponse(true, message, data);
        }

        public static ServiceResponse CreateExceptionError()
        {
            return CreateServiceResponse(false, ResponseMessage.ServerError, null);
        }

        public static ServiceResponse CreateError(string? message)
        {
            if (message == string.Empty)
            {
                message = ResponseMessage.ServerError;
            }

            return CreateServiceResponse(false, message, null);
        }

        private static ServiceResponse CreateServiceResponse(bool success, string? message, object? data)
        {
            return new ServiceResponse { Success = success, Message = message, Payload = data };
        }
    }



    public class ExceptionError
    {
        public string MethodName { get; set; }
        public string Message { get; set; }
        public string InnerException { get; set; }
        public string StackTrace { get; set; }

        public ExceptionError(string? methodName, string message, string? innerException, string? stackTrace)
        {
            MethodName = methodName ?? string.Empty;
            Message = message ?? string.Empty;
            InnerException = innerException ?? string.Empty;
            StackTrace = stackTrace ?? string.Empty;
        }
    }


    public static class ResponseMessage
    {
        public const string ServerError = "Something went wrong. Please try again later.";
        public const string InvalidRequest = "Invalid Request";
        public const string SampleNotFound = "Sample Not Found";
        public const string AddedSuccessfully = "Successfully Added";
        public const string AddedFailed = "Added Failed";
    }

}
