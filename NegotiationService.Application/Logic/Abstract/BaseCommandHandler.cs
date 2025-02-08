using NegotiationService.Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Application.Logic.Abstract
{
    public  class BaseCommandHandler
    {    
        private readonly IAuthService authService;
        public BaseCommandHandler()
        {
        }
    }
}
