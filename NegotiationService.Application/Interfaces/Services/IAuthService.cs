using NegotiationService.Application.Logic.RequestsDTO;
using NegotiationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Application.Interfaces.Services
{
    public interface IAuthService
    {

        Task<User> GetCurrentUser();
    }
}
