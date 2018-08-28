using System;
using CacheIt.Abstractions;
using MessagePack;

namespace CacheIt.MessagePack
{
    public class MessagePackEntrySerializer : IEntrySerializer
    {
        public object Deserialize(byte[] value, Type type)
        {
            return MessagePackSerializer.NonGeneric.Deserialize(type, value);
        }

        public byte[] Serialize(object value)
        {
            return MessagePackSerializer.NonGeneric.Serialize(value.GetType(), value);
        }
    }
}