using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class RedisCacheProvider : ICacheProvider
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer;
        private readonly IDatabase _cache;

        public RedisCacheProvider(string connection)
        {
            _connectionMultiplexer = ConnectionMultiplexer.Connect(connection);
            _cache = _connectionMultiplexer.GetDatabase();
        }
        private bool set(object key, RedisValue value)
        {
            var writeTask = _cache.StringSetAsync(key.ToString(), value);
            return _cache.Wait(writeTask);
        }

        public bool Set(object key, long value)
        {
            return this.set(key, value);
        }

        public bool Set(object key, string value)
        {
            return this.set(key, value);
        }                

        private RedisValue get(object key)
        {
            var readTask = _cache.StringGetAsync(key.ToString());
            return _cache.Wait(readTask);
        }

        public string Get(object key)
        {
            return this.get(key);
        }

        public long? GetLong(object key)
        {
            return (long?)this.get(key);
        }

        public bool Set<T>(object key, T value) where T : class
        {
            return this.set(key, value.ToJson<T>());
        }
        
        public T Get<T>(object key)
        {
            var res = this.get(key);
            if (res.IsNull)
                return default(T);

            return ((byte[])res).FromJson<T>();
        }

        /// <summary>
        /// public bool Remove(object key)
        /// </summary>
        /// <param name="key">object</param>
        /// <returns></returns>
        public bool Remove(object key)
        {
            var deleteTask = _cache.KeyDeleteAsync(key.ToString());
            return _cache.Wait(deleteTask);
        }

        /// <summary>
        /// public void ManualSave()
        /// </summary>
        public void ManualSave()
        {
            /*_server.Save(SaveType.BackgroundSave);*/
        }
    }
}
