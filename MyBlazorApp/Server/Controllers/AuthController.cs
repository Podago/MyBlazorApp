using Microsoft.AspNetCore.Mvc;
using MyBlazorApp.Server.Services.Authentication;
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
            var response = await _authentication.CreateUser(userDTO);

            if (response.StatusCode == HttpStatusCode.Created)
                return Ok(response);

            if (response.StatusCode == HttpStatusCode.UnprocessableEntity)
                return UnprocessableEntity(response);

            return BadRequest(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<UserDTO>>> Login(UserDTO userDTO)
        {
            var response = await _authentication.Login(userDTO);

            if (response.StatusCode == HttpStatusCode.OK)
                return Ok(response);

            return BadRequest(response);
        }
    }
}
