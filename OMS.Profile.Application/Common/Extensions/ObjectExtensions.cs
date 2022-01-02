using System.Text.Json;
using System.Text.Json.Serialization;

namespace OMS.Profile.Application.Common.Extensions;

public static class ObjectExtensions
{
    public static string ToJsonString(this object obj)
    {
        return JsonSerializer.Serialize(obj);
    }
    
    public static T DeserializeJson<T>(this string json)
    {
        return JsonSerializer.Deserialize<T>(json);
    }
}