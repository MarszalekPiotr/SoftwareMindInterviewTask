using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NegotiationService.Application.Interfaces.Services;
using NegotiationService.Application.Logic.RequestsDTO;

namespace NegotiationService.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthService _authenticationService;
        public AuthenticationController(IAuthService authenticationService)
        {
            _authenticationService = authenticationService;
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
        {
            var result = await _authenticationService.Login(loginRequest);
            if (result == null)
            {
                return Unauthorized();
            }
            return Ok(result);
        }
    }
}
