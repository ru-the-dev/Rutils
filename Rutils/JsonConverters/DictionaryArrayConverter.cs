using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DictionaryArrayConverter<KeyType, ValueType> : JsonConverter<Dictionary<KeyType, ValueType>> where KeyType : notnull
{
    public override Dictionary<KeyType, ValueType> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.StartArray)
        {
            throw new JsonException("Expected start of array.");
        }

        var combinedDictionary = new Dictionary<KeyType, ValueType>();

        while (reader.Read())
        {
            if (reader.TokenType == JsonTokenType.EndArray)
                return combinedDictionary;

            // Deserialize each element as Dictionary<KeyType, ValueType>
            var dict = JsonSerializer.Deserialize<Dictionary<KeyType, ValueType>>(ref reader, options);

            if (dict != null)
            {
                foreach (var kvp in dict)
                {
                    combinedDictionary[kvp.Key] = kvp.Value;
                }
            }
        }

        throw new JsonException("Unexpected end of JSON array.");
    }

    public override void Write(Utf8JsonWriter writer, Dictionary<KeyType, ValueType> value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
