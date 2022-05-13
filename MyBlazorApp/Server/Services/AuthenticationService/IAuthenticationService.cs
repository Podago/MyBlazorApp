namespace MyBlazorApp.Server.Services.Authentication
{
    public interface IAuthenticationService
    {
        Task<ServiceResponse<bool>> CreateUser(UserDTO userDTO);
        Task<ServiceResponse<string>> Login(UserDTO userDTO);
    }
}
