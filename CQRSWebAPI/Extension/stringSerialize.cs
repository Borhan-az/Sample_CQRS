using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSWebAPI.Extension
{
    public static class stringSerialize
    {
        public static string Serilize(this object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}