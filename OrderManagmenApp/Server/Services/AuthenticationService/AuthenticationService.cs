using AuthenticationLibriary.Authentication;

namespace OrderManagmen.Server.Services.Authentication
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

        public async Task<(bool success, string message)> CreateUser(UserDTO userDTO)
        {
            if (await _context.Users.AsNoTracking().AnyAsync(u => u.Name == userDTO.Name))
            {
                return (false, "User already exists.");
            }
            AuthenticationLibriary.Authentication.Authentication.CreatePasswordHash(userDTO.Password, out byte[] passwordHash, out byte[] passwordSalt);
            
            var user = new User
            {
                Name = userDTO.Name,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return (true, "User created.");
        }

        public async Task<string> Login(UserDTO userDTO)
        {
            var user = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Name == userDTO.Name);
            if (user == null)
            {
                return null;
            }

            if (!AuthenticationLibriary.Authentication.Authentication.CheckPassword(userDTO.Password, user.PasswordHash, user.PasswordSalt))
            {
                return null;
            }

            var roles = await _context.UserRoles.AsNoTracking().Include(r => r.Role).Where(ur => ur.UserId == user.Id).Select(ur => ur.Role.Name).ToListAsync();

            var jwt = AuthenticationLibriary.Authentication.Authentication.CreateToken(user.Name, _configuration.GetSection("AppSettings:TokenKey").Value, roles);

            return jwt;
        }
    }
}
