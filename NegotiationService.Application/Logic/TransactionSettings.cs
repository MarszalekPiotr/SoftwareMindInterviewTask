using NegotiationService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Application.Logic
{
    public static class TransactionSettings
    {
        public static int MaxOffersAmount => 3;
        public static int MaxAnswerDelayInHours => 24 * 7;

        public static Dictionary<EnumOfferStatus, string> notValidStatusMessages = new Dictionary<EnumOfferStatus, string>
        {
            { EnumOfferStatus.Pending, "Yor last offer has not been validated yet" },
            { EnumOfferStatus.Accepted, "Your last offer has already been accepted" }

          };
}
}
