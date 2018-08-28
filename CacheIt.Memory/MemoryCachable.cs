using System;
using System.Threading.Tasks;
using CacheIt.Abstractions;
using Microsoft.Extensions.Caching.Memory;

namespace CacheIt.Memory
{
    /// <summary>
    /// Decorator of <typeparamref name="T"/> for memory caching
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MemoryCachable<T> : Cachable<MemoryCacheEntryOptions, T>
    {
        internal IMemoryCache Cache { get; set; }
        internal CachableOptions<MemoryCacheEntryOptions> Options { get; set; }
        
        protected override Task SetAsync(string name, object value)
        {
            Cache.Set(name, value, Options.GetFor(InvocationContext.SerializationType));

            return Task.CompletedTask;
        }

        protected override Task<object> GetAsync(string name)
        {
            return Task.FromResult(Cache.Get(name));
        }

        protected override void Set(string name, object value)
        {
            Cache.Set(name, value, Options.GetFor(InvocationContext.SerializationType));
        }

        protected override object Get(string name)
        {
            return Cache.Get(name);
        }
    }
}