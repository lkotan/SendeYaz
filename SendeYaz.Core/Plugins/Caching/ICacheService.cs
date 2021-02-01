using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SendeYaz.Core.Plugins.Caching
{
    public interface ICacheService
    {
        //object Get<T>(string key);
        //void Add(string key, object data, int duration);
        //void Remove(string key);
        //bool Any(string key);

        bool IsEnabled { get; set; }
        Task<bool> Any(string key, int db = 0);

        Task<T> Get<T>(string key, int db = 0) where T : new();

        Task<string> Get(string key, int db = 0);
        Task Set(string key, string data, int? minute = null, int db = 0);
        Task Remove(string key, int db = 0);
        Task RemoveByPattern(string key, int db = 0);
    }
}
