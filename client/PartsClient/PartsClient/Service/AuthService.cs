using PartsClient.Data;
using System.Net.Http.Json;

namespace PartsClient.Service
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient client;
        private static string token;

        public AuthService(HttpClient client)
        {
            this.client = client;
        }

        private async Task<string> GetJWTToken(LoginRequest loginRequest)
        {
            try
            {
                var msg = new HttpRequestMessage(HttpMethod.Post, $"api/auth/login");
                msg.Content = JsonContent.Create<LoginRequest>(loginRequest);
                var response = await client.SendAsync(msg);
                response.EnsureSuccessStatusCode();
                token = await response.Content.ReadAsStringAsync();
                return token;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<bool> LoginAsync(LoginRequest loginRequest)
        {
            var token = await GetJWTToken(loginRequest);
            if (token != null)
            {
                client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
                return true;
            }
            return false;
        }

        public async Task LogoutAsync()
        {
            client.DefaultRequestHeaders.Remove("Authorization");
            await SecureStorage.SetAsync("jwtToken", string.Empty);
        }

    }
}

