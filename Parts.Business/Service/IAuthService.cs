using PartsClient.Dto.Data;

namespace Parts.Business.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginRequest loginRequest);
        Task LogoutAsync();

    }
}