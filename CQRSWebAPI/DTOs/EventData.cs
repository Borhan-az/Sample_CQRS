using System;

namespace CQRSWebAPI.DTOs
{
    public class EventData
    {
        public string APIName { get; set; }
        public DateTime Date { get; set; }
        public string Response { get; set; }
        public string Request { get; set; }
    }
}
