using NegotiationService.Application.Interfaces;
using NegotiationService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Application.Logic.PurchaseOffer.StatusUpdate
{
    public class StatusUpdateHandlerReject : StatusUpdateHandler
    {    
     
        public StatusUpdateHandlerReject()
        {

        }
     
        public override EnumOfferStatus EnumOfferStatus => EnumOfferStatus.Rejected;
        
        public override  async Task UpdateStatus(Domain.Entities.PurchaseOffer purchaseOffer, Domain.Entities.Product product,
            IGenericRepository<Domain.Entities.PurchaseOffer> repository)
        {
            if (purchaseOffer.Status == EnumOfferStatus.Accepted)
                throw new Exception("You can't reject an offer that has already been accepted");

            purchaseOffer.Status = EnumOfferStatus.Rejected;

            await repository.SaveChangesAsync();

        }
    }
}
