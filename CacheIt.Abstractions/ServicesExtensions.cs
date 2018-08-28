using System;
using CacheIt.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CacheIt
{
    public static class ServicesExtensions
    {
        /// <summary>
        /// Registers cachable decorator of <typeparamref name="TService"/> as <typeparamref name="TInterface"/>
        /// </summary>
        /// <param name="services"></param>
        /// <typeparam name="TInterface"></typeparam>
        /// <typeparam name="TService"></typeparam>
        public static void AddCachable<TInterface, TService>(this IServiceCollection services)
            where TService : class, TInterface
            where TInterface : class
        {     
            services.AddScoped<TService>();
            services.AddScoped<TInterface, TService>(provider =>
                provider.GetService<ICachableFactory>().Create(provider.GetService<TService>())
            );
        }
    }
}