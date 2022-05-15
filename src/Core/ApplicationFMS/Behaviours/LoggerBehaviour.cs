using ApplicationFMS.Interfaces;
using CoreFMS.Entities;
using MediatR;
using System;
using System.Diagnostics;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApplicationFMS.Behaviours
{
    public class LoggerBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly Stopwatch _timer;
        private readonly IFMSDataContext _context;
        private readonly ICurrentUser? _currentUser;

        public LoggerBehaviour(IFMSDataContext context, ICurrentUser? currentUser)
        {
            _timer = new Stopwatch();
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _timer.Start();
            var response = await next();
            _timer.Stop();

            await SaveLogToDatabase(request, response, cancellationToken);

            return response;
        }

        public async Task SaveLogToDatabase(TRequest request, TResponse response, CancellationToken cancellationToken)
        {
            string requestString = JsonSerializer.Serialize(request);
            string responseString = JsonSerializer.Serialize(response);

            Log log = new Log
            {
                Ip = _currentUser?.RequestRemoteIp ?? "0",
                UserEmail = _currentUser?.UserDetail?.Email ?? "guest",
                RequestDate = DateTime.Now,
                RunningDuration = (int)_timer.ElapsedMilliseconds,
                Request = requestString,
                Response = responseString,
                EndPoint = _currentUser?.RequestPath ?? ""
            };

            _context.Log.Add(log);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
