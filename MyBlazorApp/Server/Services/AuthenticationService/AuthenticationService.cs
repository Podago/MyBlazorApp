using MyBlazorApp.Server.Authentication;
using System.Net;

namespace MyBlazorApp.Server.Services.Authentication
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthenticationService(DataContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<bool>> CreateUser(UserDTO userDTO)
        {
            if(_context.Users.Any(u=>u.Name ==userDTO.Name))
            {
                return new ServiceResponse<bool>
                {
                    Data = false,
                    Success = false,
                    Message = "User already exists.",
                    StatusCode = HttpStatusCode.UnprocessableEntity
                };
            }
            Auth.CreatePasswordHash(userDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Name = userDTO.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new ServiceResponse<bool>
            {
                Data = true,
                Message = "User created.",
                StatusCode = HttpStatusCode.Created
            };
        }

        public async Task<ServiceResponse<string>> Login(UserDTO userDTO)
        {
            string incorrectLogin = "User or password are incorrect.";

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Name == userDTO.Name);
            if (user == null)
            {
                return new ServiceResponse<string>
                {
                    Data = string.Empty,
                    Success = false,
                    Message = incorrectLogin,
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            if (!Auth.CheckPassword(userDTO.Password, user.PasswordHash, user.PasswordSalt))
            {
                return new ServiceResponse<string>
                {
                    Data = string.Empty,
                    Success = false,
                    Message = incorrectLogin,
                    StatusCode = HttpStatusCode.NotFound
                };
            }

            var jwt = Auth.CreateToken(user, _configuration.GetSection("AppSettings:TokenKey").Value);

            return new ServiceResponse<string>
            {
                Data = jwt,
                StatusCode = HttpStatusCode.OK
            };
        }
    }
}
