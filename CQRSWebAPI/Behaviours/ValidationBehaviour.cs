using CQRSWebAPI.DTOs;
using CQRSWebAPI.Validation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CQRSWebAPI.Behaviours
{
    public class ValidationBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TResponse : ResponseDto, new()
    {
        private readonly ILogger<ValidationBehaviour<TRequest, TResponse>> logger;
        private readonly IValidationHandler<TRequest> validationHandler;

        public ValidationBehaviour(ILogger<ValidationBehaviour<TRequest, TResponse>> logger)
        {
            this.logger = logger;
        }
        public ValidationBehaviour(ILogger<ValidationBehaviour<TRequest, TResponse>> logger, IValidationHandler<TRequest> validationHandler)
        {
            this.logger = logger;
            this.validationHandler = validationHandler;
        }
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            var reqName = request.GetType();
            if (validationHandler == null)
            {
                logger.LogInformation("{Request} does not have a validation Handler configured", reqName);
                return await next();
            }
            var result = await validationHandler.Validation(request);
            if (!result.IsSuccessful)
            {
                logger.LogWarning("Validation failed for {Request} Error {Error}", reqName, result.Error);
                return new TResponse
                {
                    HttpStatusCode = System.Net.HttpStatusCode.BadRequest,
                    ErrorMessaging = result.Error
                };
            }
            logger.LogInformation("Validation successful for {Request}", reqName);
            return await next();

        }
    }
}