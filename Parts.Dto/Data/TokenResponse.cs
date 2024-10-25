using System.Text.Json.Serialization;

namespace PartsClient.Dto.Data
{
    public class TokenResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}

