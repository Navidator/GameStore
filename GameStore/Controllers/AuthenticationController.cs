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

        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserDto dto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest("Please provide all the fields");
            }

            return new OkObjectResult(await _authenticationService.Register(dto));
        }
    }
}
