using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NegotiationService.Application.Interfaces.Services;
using NegotiationService.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
