using CQRSWebAPI.DTOs;
using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSWebAPI.Caching
{
    public class CachingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICacheable where TResponse : ResponseDto
    {
        private readonly IMemoryCache memoryCache;
        private readonly ILogger<CachingBehaviour<TRequest, TResponse>> logger;

        public CachingBehaviour(IMemoryCache memoryCache, ILogger<CachingBehaviour<TRequest, TResponse>> logger)
        {
            this.memoryCache = memoryCache;
            this.logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var reqName = request.GetType();
            logger.LogInformation("{Request} is configured for caching.", reqName);

            TResponse response;
            if (memoryCache.TryGetValue(request.CacheKey, out response))
            {
                logger.LogInformation("Returning cached value for {Request}.", reqName);
                return response;
            }

            logger.LogInformation("{Request} CacheKey: {Key} is not inside the cache , executing request.", reqName, request.CacheKey);
            response = await next();
            memoryCache.Set(request.CacheKey, response);
            return response;
        }
    }
}