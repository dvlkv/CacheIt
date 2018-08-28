using System;
using System.Security.Authentication.ExtendedProtection;
using System.Threading.Tasks;
using CacheIt.Memory;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace CacheIt.Tests
{
    public class Tests
    {
        private readonly IService _service; 
        public Tests()
        {
            var services = new ServiceCollection();

            services.AddMemoryCache();
            services.AddMemoryCachable();
            services.AddCachable<IService, ServiceImplementation>();

            var provider = services.BuildServiceProvider();

            _service = provider.GetService<IService>();
        }
        
        [Fact]
        public void CachedFunction_ShouldNotThrow()
        {
            _service.CachedFunction();
            Console.WriteLine(_service.CachedFunction());
        }
        
        [Fact]
        public async Task AsyncCachedFunction_ShouldNotThrow()
        {
            await _service.AsyncCachedFunction();
            Console.WriteLine(await _service.AsyncCachedFunction());
        }
    }

    public interface IService
    {
        [Cached]
        string CachedFunction();
        [Cached]
        Task<string> AsyncCachedFunction();
    }

    public class ServiceImplementation : IService
    {    
        public string CachedFunction()
        {
            Task.Delay(1000).Wait(); //very long operation
            return "test";
        }

        public async Task<string> AsyncCachedFunction()
        {
            await Task.Delay(1000); //very long operation
            return "async test";
        }
    }
}