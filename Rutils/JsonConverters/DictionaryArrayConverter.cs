using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class DictionaryArrayConverter<KeyType, ValueType> : JsonConverter<Dictionary<KeyType, ValueType>> where KeyType: notnull
{
    public override Dictionary<KeyType, ValueType> ReadJson(JsonReader reader, Type objectType, Dictionary<KeyType, ValueType>? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {
        JArray array = JArray.Load(reader);
        
        var combinedDictionary = new Dictionary<KeyType, ValueType>();

        foreach (var item in array)
        {
            var dictionary = item.ToObject<Dictionary<KeyType, ValueType>>();

            if (dictionary == null)
            {
                continue;
            }

            foreach (var kvp in dictionary)
            {
                combinedDictionary[kvp.Key] = kvp.Value;
            }
        }

        return combinedDictionary;
    }

    public override void WriteJson(JsonWriter writer, Dictionary<KeyType, ValueType>? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}