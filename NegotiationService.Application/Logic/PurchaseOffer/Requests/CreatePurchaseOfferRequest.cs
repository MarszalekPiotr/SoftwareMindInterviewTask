using NegotiationService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Application.Logic.PurchaseOffer.Requests
{
    public  class CreatePurchaseOfferRequest
    {
        public string CustomerEmail { get; set; } = string.Empty;
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        
    }
}
