using System.Reflection;
using CacheIt.Abstractions;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;

namespace CacheIt.Memory
{
    public class MemoryCachableFactory : ICachableFactory
    {
        private readonly IMemoryCache _cache;
        private readonly CachableOptions<MemoryCacheEntryOptions> _options;

        public MemoryCachableFactory(IMemoryCache cache, IOptions<CachableOptions<MemoryCacheEntryOptions>> options)
        {
            _cache = cache;
            _options = options.Value;
        }
        
        public T Create<T>(T instance)
        {
            var proxiedInstance = DispatchProxy.Create<T, MemoryCachable<T>>();
            
            var proxy = proxiedInstance as MemoryCachable<T>;
            proxy.Instance = instance;
            proxy.Cache = _cache;
            proxy.Options = _options;

            return proxiedInstance;
        }
    }
}