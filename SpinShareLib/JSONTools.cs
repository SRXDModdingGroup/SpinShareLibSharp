using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SpinShareLib
{
    class DateTimeParse : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return DateTime.Parse(reader.GetString());
        }
        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            //Don't implement this unless you're going to use the custom converter for serialization too
            throw new NotImplementedException();
        }
    }

    class StrObjectToArr : JsonConverter<string[]>
    {
        public override string[] Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            List<string> returnArr = new List<string>();
            if (reader.TokenType == JsonTokenType.StartArray || reader.TokenType == JsonTokenType.StartObject)
            {
                while (reader.Read())
                {
                    switch (reader.TokenType)
                    {
                        case JsonTokenType.EndObject:
                        case JsonTokenType.EndArray:
                            return returnArr.ToArray();
                        case JsonTokenType.PropertyName:
                            reader.Read();
                            returnArr.Add(reader.GetString());
                            break;
                        default:
                            returnArr.Add(reader.GetString());
                            break;
                    }
                }
            }
            throw new JsonException();
        }
        public override void Write(
            Utf8JsonWriter writer,
            string[] value,
            JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}