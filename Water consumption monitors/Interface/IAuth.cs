using Water_consumption_monitors.Models;

namespace Water_consumption_monitors.Interface
{
    public interface IAuth
    {
        Task<Auth> RegisterAsync(Register model);
        Task<Auth> GetTokenAsync(TokenRequest model);
        Task<string> AddRoleAsync(AddRole model);
        Task<Auth> RefreshTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string token);
    }
}
