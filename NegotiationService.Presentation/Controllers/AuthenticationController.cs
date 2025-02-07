using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NegotiationService.Application.Interfaces.Services;
using NegotiationService.Application.Logic.RequestsDTO;
using System.Security.Claims;

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
            return Ok(new { result });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequestDTO registerRequest)
        {
            Console.WriteLine(User.Identity.IsAuthenticated);
            var result = await _authenticationService.Register(registerRequest);
            
            return Ok(new { result });
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetProfile()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Ok( new { message = "You are authenticated!", userId = User.Identity.Name });
        }
    }
}
