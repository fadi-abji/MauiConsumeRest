using PartsClient.Data;

namespace PartsClient.Service
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginRequest loginRequest);
        Task LogoutAsync();

    }
}