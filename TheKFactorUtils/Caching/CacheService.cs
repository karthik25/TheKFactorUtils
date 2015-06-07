using System;
using System.Web;
using System.Web.Caching;
using TheKFactorUtils.Abstract;

namespace TheKFactorUtils.Caching
{
    public class CacheService : ICacheService
    {
        public T Get<T>(string cacheId, Func<T> getItemCallback, int duration = 5) where T : class
        {
            var item = HttpRuntime.Cache.Get(cacheId) as T;
            if (item == null)
            {
                item = getItemCallback();
                this.Set(cacheId, item, duration);
            }
            return item;
        }

        public void Set(string cacheId, object data, int duration = 5)
        {
            HttpContext.Current.Cache.Insert(cacheId,
                                             data,
                                             null,
                                             DateTime.Now.AddMinutes(duration),
                                             Cache.NoSlidingExpiration);
        }
    }
}