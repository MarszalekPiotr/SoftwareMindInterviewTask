using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NegotiationService.Application.Interfaces.Services;
using NegotiationService.Application.Logic.RequestsDTO;
using NegotiationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Infrastructure.Services
{
    public  class AuthService : IAuthService
    {
        private const string UserIdClaim = "UserId";
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        public AuthService(IConfiguration config, UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        private string CreateToken(string userId)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(UserIdClaim, userId),
            
        };

            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> Login(LoginRequestDTO loginRequest)
        {
            var user = await _userManager.FindByEmailAsync(loginRequest.Email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, loginRequest.Password, false);
            if (result.Succeeded)
            {
                return CreateToken(user.Id);
            }
            throw new Exception("Invalid login or password");
        }

        public async Task<string> Register(RegisterRequestDTO registerRequest)
        {
            var user = new User
            {
                UserName = registerRequest.Email,
                Email = registerRequest.Email,
                Name = registerRequest.Name,
                Description = registerRequest.Description
            };
            var result = await _userManager.CreateAsync(user, registerRequest.Password);
            if (result.Succeeded)
            {
                return CreateToken(user.Id);
            }
            throw new Exception("User not created");
        }


    }
}
