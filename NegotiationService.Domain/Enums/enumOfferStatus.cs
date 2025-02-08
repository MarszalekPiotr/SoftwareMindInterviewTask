using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Domain.Enums
{
   public enum EnumOfferStatus : int
    {
        Pending = 1,
        Accepted = 2,
        Rejected = 3,
        Canceled = 4
    }
}
