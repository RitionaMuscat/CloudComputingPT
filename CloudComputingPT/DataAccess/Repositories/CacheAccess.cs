using CloudComputingPT.DataAccess.Interfaces;
using CloudComputingPT.Models;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace CloudComputingPT.DataAccess.Repositories
{
    public class CacheAccess : ICacheAccess
    {
        private StackExchange.Redis.IDatabase db;
        public CacheAccess(IConfiguration config)
        {
            string connectionString = config.GetSection("cacheConnection").Value;
            db = ConnectionMultiplexer.Connect(connectionString).GetDatabase();
        }
        public string FetchData(string key)
        {
            return db.StringGet(key);
        }
        public void SaveData(PricesDictionary item)
        {
            db.StringSet(item.Id.ToString(), item.Value);
        }
    }
}