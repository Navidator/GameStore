using GameStore.Dtos;
using GameStore.Services.Service_Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace GameStore.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authenticationService;

        public AuthenticationController(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost, Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide all fields");
            }
            return new OkObjectResult(await _authenticationService.Register(dto));
        }

        [HttpPost, Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginUserDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide all fields");
            }

            var user = await _authenticationService.Login(dto);

            if (user != null)
            {
                return new OkObjectResult(user);
            }
            return Unauthorized();
        }
    }
}
