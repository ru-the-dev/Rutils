using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace Rutils;

public static class HttpHelper
{
    public static StringContent ToJsonStringContent<T>(T obj)
    {
        return new StringContent(JsonSerializer.Serialize(obj, JsonSerializerOptions.Web), Encoding.UTF8, "application/json");
    }

    public static async Task<T> HandleRequestAsync<T>(HttpClient client, HttpRequestMessage req, string? jsonPath = null)
    {
        var result = await client.SendAsync(req);

        if (!result.IsSuccessStatusCode)
        {
            throw new HttpRequestException($"HTTP request failed:\n{result.StatusCode} {result.ReasonPhrase}");
        }

        var strContent = await result.Content.ReadAsStringAsync();

        JsonNode? jsonNode;
        try
        {
            jsonNode = JsonNode.Parse(strContent);
        }
        catch (JsonException ex)
        {
            throw new JsonException("Failed to parse JSON response.", ex);
        }

        if (jsonNode == null)
        {
            throw new JsonException("Response JSON is null or invalid.");
        }

        JsonNode? targetNode;

        if (string.IsNullOrEmpty(jsonPath))
        {
            targetNode = jsonNode;
        }
        else
        {
            targetNode = jsonNode;

            foreach (var segment in jsonPath.Split('.'))
            {
                if (targetNode is JsonObject obj && obj.TryGetPropertyValue(segment, out JsonNode? child))
                {
                    targetNode = child;
                }
                else
                {
                    targetNode = null;
                    break;
                }
            }
        }

        if (targetNode == null)
        {
            throw new JsonException($"Could not find a JSON node at path: \"{jsonPath}\"");
        }

        try
        {
            // Deserialize the target node to T
            var returnValue = targetNode.Deserialize<T>();

            if (returnValue == null)
            {
                throw new JsonException($"Failed to deserialize JSON node to {typeof(T).Name}, result was null.");
            }

            return returnValue;
        }
        catch (JsonException ex)
        {
            throw new JsonException($"Failed to deserialize JSON to {typeof(T).Name}", ex);
        }
    }
}