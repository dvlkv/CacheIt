using System;

namespace CacheIt.Abstractions
{
    /// <summary>
    /// Serializer for cache entries
    /// </summary>
    public interface IEntrySerializer
    {
        /// <summary>
        /// Deserializes object
        /// </summary>
        /// <param name="value">Serialized object</param>
        /// <param name="type">Object type</param>
        /// <returns></returns>
        object Deserialize(byte[] value, Type type);
        /// <summary>
        /// Serializes object
        /// </summary>
        /// <param name="value">Object</param>
        /// <returns>Serialized object</returns>
        byte[] Serialize(object value);
    }
}