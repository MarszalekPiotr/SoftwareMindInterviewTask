using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using NegotiationService.Application.Interfaces.Services;

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
    public class AuthService : IAuthService
    {    
        private readonly UserManager<User> _userManager;
        private readonly  IHttpContextAccessor _httpContextAccessor;
        public AuthService(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public Task<User> GetCurrentUser()
        {
            var claimUser = _httpContextAccessor.HttpContext.User;
            if(claimUser == null)
            {
                return null;
            }
            return _userManager.FindByNameAsync(claimUser.Identity.Name);
        }
    }
}
