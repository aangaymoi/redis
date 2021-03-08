using Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppRedistTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //var a = new Product() { Id = 3, Name = "333" };
            //var b = new Product() { Id = 2, Name = "2" };

            //{
            //    ICacheProvider _cacheProvider;
            //    _cacheProvider = new RedisCacheProvider("localhost", 6379);

            //    var bb = _cacheProvider.Remove(a.Id);
            //    bb = _cacheProvider.Remove(b.Id);

            //    _cacheProvider.Set<Product>(a.Id, a);
            //    _cacheProvider.Set<Product>(b.Id, b);

            //    var a1 = _cacheProvider.Get<Product>(a.Id);
            //    var b1 = _cacheProvider.Get<Product>(b.Id);
            //}

            ICacheProvider cached = new RedisCacheProvider("localhost:6380,allowAdmin=true,keepAlive=180,syncTimeout=3000");

            //var t1 = new HistoryItem() { ID = 10, TraceDataList = new List<TraceData>() };
            //t1.ToJson<HistoryItem>();

            var test6 = cached.Get<HistoryItem>(0);
            var test5 = cached.Get<HistoryItem>("111aaaaa");

            var kaka = cached.Set("111a", long.MaxValue);
            var test2 = cached.Get("111a");
            var test3 = cached.Get("112a");
            var test4 = cached.Get("111a");

            var p100000000000 = cached.Get<HistoryItem>(1000000000000);

            var before = DateTime.Now;

            for (int i = 0; i < 500000; i++)
            {
                var hs = new HistoryItem() { ID = i, TraceDataList = new List<TraceData>() };
                var traceData = new TraceData() { Date = DateTime.UtcNow };

                traceData.TraceItems = new List<TraceItem>();
                traceData.TraceItems.Add(new TraceItem() { Host = "", Online = DateTime.UtcNow.AddHours(-1), Offline = DateTime.UtcNow.AddHours(+1) });

                hs.TraceDataList.Add(traceData);

                var a = cached.Set<HistoryItem>(i, hs);

                //var res1 = cached.Get<HistoryItem>(i);
                //cached.Set<HistoryItem>(i, hs);
                //var res2 = cached.Get<HistoryItem>(i);
                //cached.Remove(i);
                //var res3 = cached.Get<HistoryItem>(i);
            }

            var after = DateTime.Now;

            Console.WriteLine((after - before).TotalMilliseconds);

            var p500000 = cached.Get<HistoryItem>(500000);
            var p499999 = cached.Get<HistoryItem>(499999);

            cached.Remove(2);
            var p2 = cached.Get<HistoryItem>(2);
            var p10 = cached.Get<HistoryItem>(10);

            /*cached.ManualSave();*/

            //using (var redis = new RedisClient("localhost", 6379))
            //{
            //    var a12 = redis.Get<Product>(a.Id.ToString());

            //    var st = redis.Get<string>("value");

            //    var bb = redis.Remove(a.Id.ToString());
            //    bb = redis.Remove(b.Id.ToString());

            //    redis.Set<Product>(a.Id.ToString(), a);
            //    redis.Set<Product>(b.Id.ToString(), b);

            //    var allProducts = redis.GetAll<Product>();

            //    var a1 = redis.Get<Product>(a.Id.ToString());
            //    var b1 = redis.Get<Product>(b.Id.ToString());

            //    /*dump db to file*/
            //    /*redis.Save();*/
            //}
        }
    }

    public class HistoryItem
    {
        public long ID { get; set; }
        public List<TraceData> TraceDataList { get; set; }
    }

    public class TraceData
    {
        public DateTime Date { get; set; }
        public List<TraceItem> TraceItems { get; set; }
    }

    public class TraceItem
    {
        public string Host { get; set; }
        public DateTime Online { get; set; }
        public DateTime Offline { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() == obj.GetType())
            {
                return false;
            }

            return (obj as Product).Id == this.Id;
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
