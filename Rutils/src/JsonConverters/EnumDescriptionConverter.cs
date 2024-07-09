using Newtonsoft.Json;

namespace Rutils;

public class EnumDescriptionConverter<T> : JsonConverter<T> where T : Enum
{
    public override T ReadJson(JsonReader reader, Type objectType, T? existingValue, bool hasExistingValue, JsonSerializer serializer)
    {   

        Type? type = Nullable.GetUnderlyingType(objectType);
        
        bool nullable = true;
        if (type == null)
        {
            type = objectType;
            nullable = false;
        }

        string value = existingValue!.ToString();
        object? res;
        if (Enum.TryParse(type, value, true, out res))
        {
            return (T)res;
        }
        
        if (nullable)
        {
            try
            {
                return Rutils.EnumExtentions.FromDescription<T>(value);
            }
            catch
            {
                return default!;
            }
        }
        else
        {
            return Rutils.EnumExtentions.FromDescription<T>(value);
        }
    }

    public override void WriteJson(JsonWriter writer, T? value, JsonSerializer serializer)
    {
        throw new NotImplementedException();
    }
}