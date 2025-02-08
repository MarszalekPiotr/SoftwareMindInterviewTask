using NegotiationService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Domain.Entities
{
    public class PurchaseOffer : BaseEntity
    {
        public decimal Price { get; set; } 
        public int Quantity { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public string CustomerEmail { get; set; }
        public EnumOfferStatus Status { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
    }
}
