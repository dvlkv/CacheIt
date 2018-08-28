using CacheIt.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CacheIt.Json
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddJsonEntrySerializer(this IServiceCollection services)
        {
            services.AddScoped<IEntrySerializer, JsonEntrySerializer>();
            return services;
        }
    }
}