namespace OrderManagmen.Server.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<(bool success, string message)> CreateUser(UserDTO userDTO);
        Task<string> Login(UserDTO userDTO);
    }
}
