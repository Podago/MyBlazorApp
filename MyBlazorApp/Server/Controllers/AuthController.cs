using Microsoft.AspNetCore.Mvc;
using MyBlazorApp.Server.Services.Authentication;
using MyBlazorApp.Shared.DTO;
using System.Net;

namespace MyBlazorApp.Server.Controllers
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
            var result = await _authentication.CreateUser(userDTO);

            if (result.success == false)
                return BadRequest(result.message);

            return Created("api/Auth/login", result.message);
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
