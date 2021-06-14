using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSWebAPI.Behaviours
{
    public class LoggingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehaviour<TRequest, TResponse>> logger;

        public LoggingBehaviour(ILogger<LoggingBehaviour<TRequest,TResponse>> logger)
        {
            this.logger = logger;
        }


        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {

            // Pre Logic 
            logger.LogInformation("{request} is Starting" , request.GetType().Name);
            
            var timeStamp = Stopwatch.StartNew();            
            
            var res = next();
            timeStamp.Stop();
            // Post Logic 
            logger.LogInformation("{request} has finished in {Time}ms", request.GetType().Name,timeStamp.ElapsedMilliseconds);

            return res;
        }
    }
}