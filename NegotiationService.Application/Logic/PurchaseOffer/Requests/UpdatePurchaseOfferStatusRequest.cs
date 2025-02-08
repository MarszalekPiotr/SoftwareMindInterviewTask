using NegotiationService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Application.Logic.PurchaseOffer.Requests
{
    public class UpdatePurchaseOfferStatusRequest
    {
        public int PurchaseOfferId { get; set; }
        public EnumOfferStatus Status { get; set; }
    }
}
