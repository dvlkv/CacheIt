using CacheIt.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace CacheIt.MessagePack
{
    public static class ServicesExtensions
    {
        public static IServiceCollection AddMessagePackEntrySerializer(this IServiceCollection services)
        {
            services.AddScoped<IEntrySerializer, MessagePackEntrySerializer>();
            return services;
        }
    }
}