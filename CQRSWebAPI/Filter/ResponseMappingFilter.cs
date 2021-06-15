using CQRSWebAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSWebAPI.Filter
{
    public class ResponseMappingFilter : IActionFilter
    {
        /// <summary>
        /// This Filter is about maps the status Code to http status code automatically
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Result is ObjectResult objectResult && objectResult.Value is ResponseDto response && response.HttpStatusCode != System.Net.HttpStatusCode.OK)
                context.Result = new ObjectResult(new { response.ErrorMessaging }) { StatusCode = (int)response.HttpStatusCode };
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}