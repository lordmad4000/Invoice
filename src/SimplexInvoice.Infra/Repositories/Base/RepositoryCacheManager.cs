using SimplexInvoice.Application.Common.Interfaces.Persistance;
using SimplexInvoice.Domain.Base;
using SimplexInvoice.Infra.Exceptions;
using System;

namespace SimplexInvoice.Infra.Repositories
{
    public class RepositoryCacheManager<T> : IRepositoryCacheManager<T> 
        where T : Entity
    {
        protected readonly string _cacheKey = $"{typeof(T)}";
        private readonly ICacheService _cacheService;
        private readonly ICustomLogger _logger;
        public RepositoryCacheManager(ICacheService cacheService, ICustomLogger logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        public bool TryGetCache<Ty>(string cacheKey, out Ty value)
        {
            value = default;
            try
            {
                if (_cacheService != null)
                {
                    if (_cacheService.TryGet(cacheKey, out value))
                    {
                        _logger.Debug($"{cacheKey} cached data readed.");
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new CacheException(ex.InnerException.Message);
            }

            return false;
        }

        public bool TryRemoveCache(string cacheKey)
        {
            try
            {
                if (_cacheService != null)
                {
                    _cacheService.Remove($"{cacheKey}*");
                    _logger.Debug($"{cacheKey} cached removed.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new CacheException(ex.InnerException.Message);
            }

            return false;
        }

        public bool TrySetCache<Ty>(string cacheKey, Ty value)
        {
            try
            {
                if (_cacheService != null)
                {
                    _cacheService.Set(cacheKey, value);
                    _logger.Debug($"{cacheKey} cached set.");
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw new CacheException(ex.InnerException.Message);
            }

            return false;
        }

    }
}