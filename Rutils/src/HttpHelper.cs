using System.Text;
using Newtonsoft.Json;

namespace Rutils;

public static class HttpHelper
{
    public static StringContent ToJsonStringContent<T>(T obj)
    {
        return new StringContent(JsonConvert.SerializeObject(obj, Formatting.Indented), Encoding.UTF8, "application/json");
    }

    /// <summary>
    /// sends the HttpRequestMessage, and parses the response into <typeparamref name="T"/>
    /// </summary>
    /// <exception cref="HttpRequestException">Thrown when request fails</exception>
    /// <exception cref="JsonSerializationException">Thrown when json deserialization fails</exception>
    public static async Task<T> HandleRequestAsync<T>(HttpClient client, HttpRequestMessage req)
    {
        var result = await client.SendAsync(req);

        if (result.IsSuccessStatusCode == false)
        {
            throw new HttpRequestException($"HTTP request failed:\n{result.StatusCode} {result.ReasonPhrase}");
        }

        var strContent = await result.Content.ReadAsStringAsync();
        
        T? returnValue = JsonConvert.DeserializeObject<T>(strContent);

        if (returnValue == null)
        {
            throw new JsonSerializationException($"Failed to deserialize api response body to {typeof(T).Name}, result was null.");
        }

        return returnValue;
    }
}