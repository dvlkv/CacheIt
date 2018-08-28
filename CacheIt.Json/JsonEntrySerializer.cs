using System;
using System.Text;
using CacheIt.Abstractions;
using Newtonsoft.Json;

namespace CacheIt.Json
{
    public class JsonEntrySerializer : IEntrySerializer
    {
        public object Deserialize(byte[] value, Type type)
        {
            return JsonConvert.DeserializeObject(Encoding.Default.GetString(value));
        }

        public byte[] Serialize(object value)
        {
            return Encoding.Default.GetBytes(JsonConvert.SerializeObject(value));
        }
    }
}