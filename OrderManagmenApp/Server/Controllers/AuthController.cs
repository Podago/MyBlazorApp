namespace OrderManagmen.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthenticationService _authentication;

        public AuthController(IAuthenticationService authentication)
        {
            _authentication = authentication;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<UserDTO>>> Register(UserDTO userDTO)
        {
            var (success, message) = await _authentication.CreateUser(userDTO);

            if (success == false)
                return BadRequest(message);

            return Created("api/Auth/login", message);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<UserDTO>>> Login(UserDTO userDTO)
        {
            var jwt = await _authentication.Login(userDTO);

            if (jwt == null)
                return Unauthorized();

            var result = new ServiceResponse<string>
            {
                Data = jwt
            };

            return Ok(result);
        }
    }
}
