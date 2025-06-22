using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Rutils.Extentions;

namespace Rutils.JsonConverters;

public class EnumDescriptionConverter<T> : JsonConverter<T> where T : struct, Enum
{
    public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        bool isNullable = Nullable.GetUnderlyingType(typeToConvert) != null;
        Type enumType = Nullable.GetUnderlyingType(typeToConvert) ?? typeToConvert;

        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Unexpected token parsing enum. Expected String, got {reader.TokenType}.");
        }

        string value = reader.GetString()!;

        // Try parse enum by name, case-insensitive
        if (Enum.TryParse(enumType, value, ignoreCase: true, out object? result))
        {
            return (T)result;
        }

        // Fallback: try parse by description attribute using your custom method
        try
        {
            var enumValue = EnumExtentions.FromDescription<T>(value);
            return enumValue;
        }
        catch
        {
            if (isNullable)
            {
                return default!;
            }
            throw new JsonException($"Unable to convert \"{value}\" to enum {enumType.Name}.");
        }
    }

    public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}
