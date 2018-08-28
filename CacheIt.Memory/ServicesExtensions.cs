using System;
using CacheIt.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.DependencyInjection;

namespace CacheIt.Memory
{
    public static class ServicesExtensions
    {
        public static void AddMemoryCachable(this IServiceCollection services, Action<CachableOptions<DistributedCacheEntryOptions>> optionsConfigurator = null)
        {
            services.AddScoped<ICachableFactory, MemoryCachableFactory>();
            services.Configure(optionsConfigurator);
        } 
    }
}