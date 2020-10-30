using AutoMapper;
using $ext_safeprojectname$.Common;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Threading.Tasks;
using $ext_safeprojectname$.Application.Infrastructure;

namespace $safeprojectname$.Caching
{
    public class CachingHandler : ICachingHandler
    {
        private readonly IDistributedCache _cacheProvider;
        private readonly IMapper _mapper;

        public CachingHandler(IDistributedCache cacheProvider, IMapper mapper)
        {
            _cacheProvider = cacheProvider;
            _mapper = mapper;
        }

        public async Task<TDestination> GetOrAddWithMappingAsync<TSource, TDestination>(string cacheKey, Func<TSource> newValue, DateTime expiryDateTime)
        {
            try
            {
                var cachedValue = await GetAsync<TDestination>(cacheKey);
                if (cachedValue != null)
                    return cachedValue;

                var newMappedValue = GetDataFromDbWithMapping<TSource, TDestination>(newValue);

                await _cacheProvider.SetStringAsync(cacheKey, JsonHelper.Serialize(newMappedValue), new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = expiryDateTime
                });

                return newMappedValue;

            }

            catch (Exception ex)
            {
                // Utility.LogErrorToFile(ex.ToString());

                return GetDataFromDbWithMapping<TSource, TDestination>(newValue);
            }


        }

        public async Task<T> GetOrAdd<T>(string cacheKey, Func<T> newValue, DateTime expiryDateTime)
        {
            try
            {
                var cachedValue = await GetAsync<T>(cacheKey);
                if (cachedValue != null)
                    return cachedValue;

                var newValueFromDb = newValue();

                await _cacheProvider.SetStringAsync(cacheKey, JsonHelper.Serialize(newValueFromDb), new DistributedCacheEntryOptions
                {
                    AbsoluteExpiration = expiryDateTime
                });

                return newValueFromDb;

            }

            catch (Exception ex)
            {
                // Utility.LogErrorToFile(ex.ToString());

                return newValue();
            }


        }

        public async Task AddAsync<T>(string cacheKey, T value, DateTime expiryDateTime)
        {
            await _cacheProvider.SetStringAsync(cacheKey, JsonHelper.Serialize(value), new DistributedCacheEntryOptions() { AbsoluteExpiration = expiryDateTime });
        }

        public async Task<T> GetAsync<T>(string cacheKey)
        {
            var cachedValue = await _cacheProvider.GetStringAsync(cacheKey);
            return JsonHelper.Deserialize<T>(cachedValue);
        }

        public async Task RemoveAsync(string cacheKey)
        {
            await _cacheProvider.RemoveAsync(cacheKey);
        }

        public async Task UpdateAsync<T>(string cacheKey, T newValue, DateTime expiryDateTime)
        {
            await RemoveAsync(cacheKey);
            await AddAsync<T>(cacheKey, newValue, expiryDateTime);
        }

        private TDestination GetDataFromDbWithMapping<TSource, TDestination>(Func<TSource> newValue)
        {
            TSource dbValue = newValue();
            return _mapper.Map<TSource, TDestination>(dbValue);
        }


    }
}
