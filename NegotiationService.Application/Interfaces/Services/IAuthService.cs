using NegotiationService.Application.Logic.RequestsDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Application.Interfaces.Services
{
    public interface IAuthService
    {
       
        Task<string> Login(LoginRequestDTO loginRequest);
        Task<string> Register(RegisterRequestDTO registerRequest);
    }
}
