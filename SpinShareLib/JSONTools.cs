using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace SpinShareLib
{
    class StrObjectToArr : JsonConverter
    {
        private Type _type;
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(string[]);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            Console.WriteLine(reader.GetType());
            try 
            {
                JArray arr = JArray.Load(reader);
                return arr.ToObject <string[]>();
            }
            catch
            {
                JObject obj = JObject.Load(reader);
                var temp = new List<string>();
                foreach (string value in obj.PropertyValues())
                {
                    temp.Add(value);
                }
                return temp.ToArray();
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