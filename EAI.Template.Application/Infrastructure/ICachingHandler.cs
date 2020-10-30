using System;
using System.Threading.Tasks;

namespace $safeprojectname$.Infrastructure
{
    public interface ICachingHandler
    {
        Task AddAsync<T>(string cacheKey, T value, DateTime expiryDateTime);
        Task<T> GetAsync<T>(string cacheKey);
        Task<T> GetOrAdd<T>(string cacheKey, Func<T> newValue, DateTime expiryDateTime);
        Task<TDestination> GetOrAddWithMappingAsync<TSource, TDestination>(string cacheKey, Func<TSource> newValue, DateTime expiryDateTime);
        Task RemoveAsync(string cacheKey);
        Task UpdateAsync<T>(string cacheKey, T newValue, DateTime expiryDateTime);
    }
}
