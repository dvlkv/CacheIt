# CacheIt
CacheIt makes caching in your app better - you don't need to write many lines of boilerplate

## Usage
### Default caching implementation
```c#
public interface IService
{
    string CachedFunction();
    Task<string> AsyncCachedFunction();
}

public class ServiceImplementation : IService
{    
    public string CachedFunction()
    {
        var entry = _cache.Get(GetKey());
        if (entry != null)
            return entry;
        
        var result = "test";
        _cache.Set(result);
        
        return result;
    }

    public async Task<string> AsyncCachedFunction()
    {
        var entry = _cache.Get(GetKey());
        if (entry != null)
            return entry;
        
        var result = "async test";
        _cache.Set(result);
        
        return result;
    }
}
```
### Caching implementation with CacheIt
```c#
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
        return "test";
   }

   public async Task<string> AsyncCachedFunction()
   {
       return "async test";
   }
}
```
## Setup
Now CacheIt supports IDistributedCache and IMemoryCache
To make it work, you should install CacheIt.DistributedCache or CacheIt.MemoryCache

Now you should add this to your Startup.cs:
```c#
services.AddDistributedCachable();
```
or
```c#
services.AddMemoryCachable();
```

If you use distributed cache, you need to add entry serializer:
```c#
services.AddJsonEntrySerializer();
```
or
```c#
services.AddMessagePackEntrySerializer();
```
#
Then you should decorate your services to enable caching in them, it's easy:
Use
```c#
services.AddCachable<IService, ServiceImplementation>();
```
Instead of
```c#
services.AddScoped<IService, ServiceImplementation>();
```
## Configuration
You can configure cache entries like
```c#
services.AddMemoryCachable(opts => 
{
    opts.ConfigureAll(entryOptions => { ... });
    opts.Configure<TResult>(entryOptions => { ... });
});
```
