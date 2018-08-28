# CacheIt
CacheIt makes caching in your app better - you don't need to write many lines of boilerplate

## Usage
### Default caching implementation
```c#
public async Task<string> CachedFunction(string params) {
    var entry = _cache.Get(params);
    if (entry == null)
        entry = await _dataSource.Request(params);
    _cache.Set(params);
    
    return entry;
}
```
### Caching implementation with CacheIt
```c#
[Cached]
public async Task<string> CachedFunction(string params) {
    return await _dataSource.Request(params);
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
