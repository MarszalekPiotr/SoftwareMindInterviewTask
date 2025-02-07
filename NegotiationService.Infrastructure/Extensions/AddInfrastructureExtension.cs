using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NegotiationService.Application.Interfaces.Services;
using NegotiationService.Domain.Entities;
using NegotiationService.Infrastructure.Persistance;
using NegotiationService.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace NegotiationService.Infrastructure.Extensions
{
    public static class AddInfrastructureExtension
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            
           

          

            services.AddTransient<IAuthService, AuthService>();
        }
    }
}
