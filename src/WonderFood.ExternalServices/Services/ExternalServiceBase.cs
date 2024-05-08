using System.Text;
using System.Text.Json;

namespace WonderFood.ExternalServices.Services;

public abstract class ExternalServiceBase
{
    protected static StringContent CreateStringContent<T>(T obj)
    {
        var json = JsonSerializer.Serialize(obj, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        return new StringContent(json, Encoding.UTF8, "application/json");
    }
    
    protected static async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
    {
        var json = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(json);
    }
}