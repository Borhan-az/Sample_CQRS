using CQRSWebAPI.DTOs;
using CQRSWebAPI.Extension;
using CQRSWebAPI.Redis;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSWebAPI.Behaviors
{
    public class EventSourceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<EventSourceBehaviour<TRequest, TResponse>> logger;
        private readonly IRedisManager redisManager;

        public EventSourceBehaviour(ILogger<EventSourceBehaviour<TRequest,TResponse>> logger,IRedisManager redisManager)
        {
            this.logger = logger;
            this.redisManager = redisManager;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            logger.LogInformation("this is in Event Sourcing");
            var response = await next();

            if (request.GetType().Name.Contains("Command"))
            {

                var eventData = new EventData
                {
                    APIName = request.GetType().Name,
                    Date = DateTime.Now,
                    Request = request.Serilize(),
                    Response = response.Serilize()
                };
                logger.LogInformation("event sourcing successful");
                if (!redisManager.CreateEventSourcing(eventData))
                    logger.LogError("Event Sorcing Error Save!", eventData.Serilize());
            }

            return response;
        }
    }
}