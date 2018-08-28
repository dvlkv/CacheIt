using System.Threading.Tasks;
using CacheIt.Abstractions;
using Microsoft.Extensions.Caching.Distributed;

namespace CacheIt.DistributedCache
{
    /// <summary>
    /// Decorator of <typeparamref name="T"/> for distributed caching
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DistributedCachable<T> : Cachable<DistributedCacheEntryOptions, T>
    {
        internal IDistributedCache Cache { private get; set; }
        internal IEntrySerializer Serializer { private get; set; }
        internal CachableOptions<DistributedCacheEntryOptions> Options { private get; set; }
        
        /// <inheritdoc />
        protected override async Task SetAsync(string name, object value)
        {
            var payload = Serializer.Serialize(value);
            await Cache.SetAsync(name, payload, Options.GetFor(InvocationContext.SerializationType));
        }

        /// <inheritdoc />
        protected override async Task<object> GetAsync(string name)
        {
            var payload = await Cache.GetAsync(name);
            return Serializer.Deserialize(payload, InvocationContext.SerializationType);
        }

        /// <inheritdoc />
        protected override void Set(string name, object value)
        {
            var payload = Serializer.Serialize(value);

            Cache.Set(name, payload, Options.GetFor(InvocationContext.SerializationType));
        }

        /// <inheritdoc />
        protected override object Get(string name)
        {
            var payload = Cache.Get(name);
            return Serializer.Deserialize(payload, InvocationContext.SerializationType);
        }
    }
}