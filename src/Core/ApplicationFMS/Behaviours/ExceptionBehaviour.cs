using ApplicationFMS.Interfaces;
using ApplicationFMS.Models;
using CoreFMS.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Text.Json;
using System.Threading;

namespace ApplicationFMS.Behaviours
{
    internal class ExceptionBehaviour
    {
        private readonly RequestDelegate _next;
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public ExceptionBehaviour(RequestDelegate next/*, IFMSDataContext context, ICurrentUser? currentUser*/)
        {
            _next = next;
            //_context = context;
            //_currentUser = currentUser;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError;

            var result = string.Empty;

            if (exception is ValidatorException validatorException)
            {
                code = HttpStatusCode.BadRequest;
                var baseResponse = new BaseResponse
                    (validatorException.Failures, validatorException.Message);
                result = JsonSerializer.Serialize(baseResponse);
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;

            if (result == string.Empty)
            {
                //result = JsonConvert.SerializeObject(new { error = exception.Message });
                result = JsonSerializer.Serialize(BaseResponse.Fail( exception.Message) );
}

            SaveExceptionLog(context.Request, result);

            return context.Response.WriteAsync(result);
        }

        public async Task SaveExceptionLog(HttpRequest request, string responseString)
        {
            string requestString = JsonSerializer.Serialize(request);

            Log log = new Log
            {
                Ip = _currentUser?.RequestRemoteIp ?? "0",
                UserEmail = _currentUser?.UserDetail?.Email ?? "guest",
                RequestDate = DateTime.Now,
                Request = requestString,
                Response = responseString,
                EndPoint = _currentUser?.RequestPath ?? ""
            };

            _context.Log.Add(log);
            await _context.SaveChangesAsync(default(CancellationToken));
        }

    }

    public static class ExceptionBehaviourExtensions
    {
        public static IApplicationBuilder UseExceptionBehaviourHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionBehaviour>();
        }
    }



}
