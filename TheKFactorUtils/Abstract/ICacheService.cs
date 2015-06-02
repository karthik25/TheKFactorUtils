using System;

namespace TheKFactorUtils.Abstract
{
    public interface ICacheService
    {
        T Get<T>(string cacheId, Func<T> getItemCallback, int duration) where T : class;
        void Set(string cacheId, object data, int duration);
    }
}