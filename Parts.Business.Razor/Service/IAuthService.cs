using Parts.Dto.Data;

namespace Parts.Services
{
    public interface IAuthService
    {
        Task<bool> LoginAsync(LoginRequest loginRequest);
        Task LogoutAsync();

    }
}