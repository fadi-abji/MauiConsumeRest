using System.Text.Json.Serialization;

namespace PartsClient.Data
{
    public class TokenResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}

