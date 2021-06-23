using CQRSWebAPI.DTOs;
using CQRSWebAPI.Extension;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CQRSWebAPI.Redis
{
    public class RedisManager : IRedisManager
    {
        private readonly IRedisClient redisClient;
        private HashSet<string> _keys;
        public RedisManager(IRedisClientsManager clientsManager)
        {
            using (IRedisClient redisClient = clientsManager.GetClient())
                this.redisClient = redisClient;
            var res = redisClient.Custom(Commands.Keys, "*");
            _keys = res.Children.Select(x => x.Text).ToHashSet<string>();
            if (_keys == null)
                _keys = new HashSet<string>();
        }
        public bool Create<T>(string cacheKey, T value)
        {
            var result = redisClient.Set(cacheKey, value);
            if (result)
                _keys.Add(cacheKey);
            return result;
        }

        public bool CreateEventSourcing(EventData data)
        {
            string date = DateTime.Now.ToString() + DateTime.Now.Ticks.ToString();
            date = date.Replace("AM", "").Replace("PM", "").Replace(" ", "-");
            string key = "event_" + data.APIName + "_" + date;
            return Create(key, data.Serilize());
        }

        public void DeleteApiCache(Type inputModelType)
        {
            var toDeleteKeys = _keys.Where(x => x.Contains(inputModelType.Name)).AsEnumerable();
            redisClient.RemoveAll(toDeleteKeys);
            _keys.RemoveWhere(x => x.Contains(inputModelType.Name));
        }

        public T Get<T>(string key)
        {
            try
            {
                var result = redisClient.Get<string>(key);
                return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(result);
            }
            catch { return default(T); }
        }

        public string GetString(string key)
        {
            try
            {
                return redisClient.Get<string>(key);
            }
            catch { return null; }
        }
    }
}
