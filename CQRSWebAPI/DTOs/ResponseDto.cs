using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace CQRSWebAPI.DTOs
{
    public class ResponseDto
    {
        public HttpStatusCode HttpStatusCode { get; init; } = HttpStatusCode.OK;
        public string ErrorMessaging { get; init; }
    }
}