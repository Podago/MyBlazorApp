using MyBlazorApp.Shared.DTO;

namespace MyBlazorApp.Server.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<(bool success, string message)> CreateUser(UserDTO userDTO);
        Task<string> Login(UserDTO userDTO);
    }
}
