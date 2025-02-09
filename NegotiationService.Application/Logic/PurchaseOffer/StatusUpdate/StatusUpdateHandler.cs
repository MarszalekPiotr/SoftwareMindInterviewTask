using NegotiationService.Application.Interfaces;
using NegotiationService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Application.Logic.PurchaseOffer.StatusUpdate
{    

    public abstract class StatusUpdateHandler
    {
        public abstract EnumOfferStatus EnumOfferStatus { get; }
        public abstract Task UpdateStatus(Domain.Entities.PurchaseOffer purchaseOffer,Domain.Entities.Product product,
            IGenericRepository<Domain.Entities.PurchaseOffer> repository);
    }
}
