using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSWebAPI.Caching
{
    public interface ICacheable
    {
        public string CacheKey { get; }
    }
}
