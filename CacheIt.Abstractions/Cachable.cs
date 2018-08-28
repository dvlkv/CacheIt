using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace CacheIt.Abstractions
{
    /// <summary>
    /// Decorator for caching
    /// </summary>
    /// <typeparam name="T">Decorated type</typeparam>
    /// <typeparam name="TEntryOptions">Type of entry options</typeparam>
    public abstract class Cachable<TEntryOptions, T> : DispatchProxy 
    {
        /// <summary>
        /// Instance of decorated type
        /// </summary>
        public T Instance { get; set; }  
        
        /// <summary>
        /// Invocation Context
        /// <remarks>For use in derived types</remarks>
        /// </summary>
        protected InvocationContext<TEntryOptions> InvocationContext { get; private set; }
        
        
        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            var attrbute = targetMethod.GetCustomAttribute<CachedAttribute>();
            if (attrbute != null)
            {
                var entryName = BuildEntryName(targetMethod, args);
                if (typeof(Task).IsAssignableFrom(targetMethod.ReturnType))
                {
                    InvocationContext = new InvocationContext<TEntryOptions>(targetMethod, args, targetMethod.ReturnType.GetGenericArguments()[0]);

                    return typeof(Cachable<TEntryOptions, T>).GetMethod("SetOrGetAsync", BindingFlags.NonPublic | BindingFlags.Instance)
                        .MakeGenericMethod(InvocationContext.SerializationType)
                        .Invoke(this, new object[] { entryName, targetMethod, args });
                }
                else
                {
                    InvocationContext = new InvocationContext<TEntryOptions>(targetMethod, args, targetMethod.ReturnType);
                    
                    var entry = Get(entryName);
                    if (entry != null)
                        return entry;

                    var result = targetMethod.Invoke(Instance, args);
                    Set(entryName, result);
                    return result;
                }
            }

            return targetMethod.Invoke(Instance, args);
        }
        
        private async Task<TResult> SetOrGetAsync<TResult>(string entryName, MethodInfo targetMethod, object[] args)
        {
            var entry = await GetAsync(entryName);
            if (entry != null)
                return (TResult)entry;

            var resultTask = (Task) targetMethod.Invoke(Instance, args);
            await resultTask;
            var result = targetMethod.ReturnType.GetProperty("Result").GetValue(resultTask);
            await SetAsync(entryName, result);

            return (TResult)result;
        }
        
        private string BuildEntryName(MethodInfo targetMethod, object[] args)
        {
            return $"{typeof(T).FullName}_{targetMethod.Name}_${string.Join("_", args)}";
        }

        /// <summary>
        /// Creates entry in cache
        /// </summary>
        /// <param name="name">Entry name</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        protected abstract Task SetAsync(string name, object value);
        /// <summary>
        /// Gets entry from cache
        /// </summary>
        /// <param name="name">Entry name</param>
        /// <returns>Value</returns>
        protected abstract Task<object> GetAsync(string name);
        
        /// <summary>
        /// Creates entry in cache
        /// </summary>
        /// <param name="name">Entry name</param>
        /// <param name="value">Value</param>
        /// <returns></returns>
        protected abstract void Set(string name, object value);
        /// <summary>
        /// Gets entry from cache
        /// </summary>
        /// <param name="name">Entry name</param>
        /// <returns>Value</returns>
        protected abstract object Get(string name);
    }
}