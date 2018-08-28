using System;
using System.Reflection;

namespace CacheIt.Abstractions
{
    /// <summary>
    /// Invocation context of cached method
    /// </summary>
    public class InvocationContext<TOptions>
    {
        /// <summary>
        /// Invoked method info
        /// </summary>
        public MethodInfo MethodInfo { get; }
        /// <summary>
        /// Arguments
        /// </summary>
        public object[] Arguments { get; }
        /// <summary>
        /// Return type for serialization
        /// </summary>
        public Type SerializationType { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodInfo"></param>
        /// <param name="arguments"></param>
        /// <param name="serializationType"></param>
        internal InvocationContext(MethodInfo methodInfo, object[] arguments, Type serializationType)
        {
            MethodInfo = methodInfo;
            Arguments = arguments;
            SerializationType = serializationType;
        }
    }
}