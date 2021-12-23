using System;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpinShareLib
{
    class StrObjectToArr : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string[]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            try 
            {
                JArray arr = JArray.Load(reader);
                return arr.ToObject <string[]>();
            }
            catch
            {
                JObject obj = JObject.Load(reader);
                return Array.ConvertAll(obj.PropertyValues().ToArray(), s => (string)s);
            }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}