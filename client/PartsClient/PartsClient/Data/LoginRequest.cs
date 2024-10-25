using System.Text.Json.Serialization;

namespace PartsClient.Data;

public class LoginRequest
{
    [JsonPropertyName("user_name")]
    public string Username { get; set; }
    [JsonPropertyName("password")]
    public string Password { get; set; }
}