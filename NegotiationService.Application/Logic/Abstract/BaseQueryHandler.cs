using NegotiationService.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Application.Logic.Abstract
{
    public class BaseQueryHandler
    {    
        public readonly IAuthService _authService;
        public BaseQueryHandler(IAuthService authService)
        {
            _authService = authService;
        }
    }
}
