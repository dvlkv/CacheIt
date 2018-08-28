using System;
using CacheIt.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace CacheIt.DistributedCache
{
    public static class ServicesExtensions
    {
        public static void AddDistributedCachable(this IServiceCollection services, Action<CachableOptions<DistributedCacheEntryOptions>> optionsConfigurator = null)
        {
            services.AddScoped<ICachableFactory, DistributedCachableFactory>();
            services.Configure(optionsConfigurator);
        }
    }
}