using NegotiationService.Application.Interfaces;
using NegotiationService.Application.Interfaces.Services;
using NegotiationService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Application.Logic.Abstract
{
    public  class BaseCommandHandler
    {    
        public readonly IAuthService _authService;
        ;
        public BaseCommandHandler(IAuthService authService)
        {
            _authService = authService;
            
        }
    }
}
