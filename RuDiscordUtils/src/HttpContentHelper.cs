using System.Text;
using Newtonsoft.Json;

namespace RuDiscordUtils;

public static class HttpContentHelper
{
    public static StringContent ToJsonStringContent<T>(T obj)
    {
        return new StringContent(JsonConvert.SerializeObject(obj, Formatting.Indented), Encoding.UTF8, "application/json");
    }
}