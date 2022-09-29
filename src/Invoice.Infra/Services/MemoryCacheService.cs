using Invoice.Application.Common.Interfaces.Persistance;
using Invoice.Infra.Configuration;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System;

namespace Invoice.Infra.Services
{
    public class MemoryCacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;
        private readonly CacheConfig _cacheConfig;
        private MemoryCacheEntryOptions _cacheOptions;
        private List<string> CachedKeys { get; set; }

        public MemoryCacheService(IMemoryCache memoryCache, IOptions<CacheConfig> cacheConfig)
        {
            _memoryCache = memoryCache;
            _cacheConfig = cacheConfig.Value;
            if (_cacheConfig != null)
            {
                _cacheOptions = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddHours(_cacheConfig.AbsoluteExpirationInHours),
                    Priority = CacheItemPriority.High,
                    SlidingExpiration = TimeSpan.FromMinutes(_cacheConfig.SlidingExpirationInMinutes)
                };
            }
            CachedKeys = new List<string>();
        }

        public bool TryGet<T>(string cacheKey, out T value)
        {
            _memoryCache.TryGetValue(cacheKey, out value);

            if (value == null)
                return false;

            return true;
        }

        public T Set<T>(string cacheKey, T value)
        {
            CachedKeys.Add(cacheKey);
            return _memoryCache.Set(cacheKey, value, _cacheOptions);
        }

        public void Remove(string cacheKey)
        {
            if (cacheKey.Contains("*"))
            {
                cacheKey = cacheKey.Replace("*","");
                var cachedKeys = CachedKeys.Where(c => c.Contains(cacheKey)).ToList();
                foreach (var cachedKey in cachedKeys)
                {
                    CachedKeys.Remove(cachedKey);
                    _memoryCache.Remove(cachedKey);        
                }
            }
            else
                _memoryCache.Remove(cacheKey);
        }

    }
}