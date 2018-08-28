using System;
using CacheIt.Abstractions;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace CacheIt.Memory
{
    public static class ServicesExtensions
    {
        public static void AddMemoryCachable(this IServiceCollection services, Action<CachableOptions<MemoryCacheEntryOptions>> optionsConfigurator = null)
        {
            services.AddScoped<ICachableFactory, MemoryCachableFactory>();
            
            var options = new CachableOptions<MemoryCacheEntryOptions>();
            optionsConfigurator?.Invoke(options);
            services.AddSingleton(options);
        } 
    }
}