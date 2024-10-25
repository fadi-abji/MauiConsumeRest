using PartsClient.Dto.Data;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Parts.Business.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient client;
        private static TokenResponse token;

        public AuthService(HttpClient client)
        {
            this.client = client;
        }

        private async Task<TokenResponse> GetJWTToken(LoginRequest loginRequest)
        {
            try
            {
                var msg = new HttpRequestMessage(HttpMethod.Post, $"api/auth/login");
                msg.Content = JsonContent.Create<LoginRequest>(loginRequest);
                var response = await client.SendAsync(msg);
                response.EnsureSuccessStatusCode();
                var tokenResponse = await response.Content.ReadFromJsonAsync<TokenResponse>();

                return tokenResponse;
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
                //client.DefaultRequestHeaders.Add("Authorization", $"{token?.Token}");
                client.DefaultRequestHeaders.Add("Accept", "application/json");
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token.Token);
                return true;
            }
            return false;
        }

        public async Task LogoutAsync()
        {
            client.DefaultRequestHeaders.Remove("Authorization");
            // Only compile this section if it's a MAUI project
#if NETMAUI
                await SecureStorage.SetAsync("jwtToken", jwtToken);
#endif
        }
    }
}

