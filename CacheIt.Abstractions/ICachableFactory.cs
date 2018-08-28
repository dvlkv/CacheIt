namespace CacheIt.Abstractions
{
    public interface ICachableFactory
    {
        /// <summary>
        /// Creates decorator for type
        /// </summary>
        /// <param name="instance">Instance of decorated type</param>
        /// <typeparam name="T">Decorated type</typeparam>
        /// <returns>Cachable</returns>
        T Create<T>(T instance);
    }
}