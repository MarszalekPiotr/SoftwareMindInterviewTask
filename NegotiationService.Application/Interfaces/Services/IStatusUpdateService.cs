using NegotiationService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Application.Interfaces.Services
{
    public interface IStatusUpdateService
    {
        void UpdateStatus(Domain.Entities.PurchaseOffer purchaseOffer, Domain.Entities.Product product,
            EnumOfferStatus status, IGenericRepository<Domain.Entities.PurchaseOffer> repository);
    }
}
