using System.Reflection;
using CacheIt.Abstractions;
using Microsoft.Extensions.Caching.Distributed;

namespace CacheIt.DistributedCache
{
    public class DistributedCachableFactory : ICachableFactory
    {
        private readonly IDistributedCache _cache;
        private readonly IEntrySerializer _serializer;
        private readonly CachableOptions<DistributedCacheEntryOptions> _options;

        public DistributedCachableFactory(IDistributedCache cache,
            IEntrySerializer serializer, CachableOptions<DistributedCacheEntryOptions> options)
        {
            _cache = cache;
            _serializer = serializer;
            _options = options;
        }
        
        public T Create<T>(T instance)
        {
            var proxiedInstance = DispatchProxy.Create<T, DistributedCachable<T>>();
            
            var proxy = proxiedInstance as DistributedCachable<T>;
            proxy.Instance = instance;
            proxy.Cache = _cache;
            proxy.Serializer = _serializer;
            proxy.Options = _options;

            return proxiedInstance;
        }
    }
}