using NegotiationService.Application.Interfaces;
using NegotiationService.Application.Interfaces.Services;
using NegotiationService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Application.Logic.PurchaseOffer.StatusUpdate
{

    public class StatusUpdateService :IStatusUpdateService
    {
        private readonly static Dictionary<EnumOfferStatus, StatusUpdateHandler> statusUpdateHandlers
            = new Dictionary<EnumOfferStatus, StatusUpdateHandler>();

        static StatusUpdateService()
        {
            var handlerType = typeof(StatusUpdateHandler);
            var handlers = Assembly.GetExecutingAssembly().GetTypes()
                .Where(t => handlerType.IsAssignableFrom(t) && !t.IsAbstract)
                .Select(t => (StatusUpdateHandler)Activator.CreateInstance(t));

            foreach (var handler in handlers)
            {
                statusUpdateHandlers.Add(handler.EnumOfferStatus, handler);
            }
        }

        public StatusUpdateService()
        {
        }

        public void UpdateStatus(Domain.Entities.PurchaseOffer purchaseOffer, Domain.Entities.Product product,
            EnumOfferStatus status,IGenericRepository<Domain.Entities.PurchaseOffer> repository)
        {
            if (!statusUpdateHandlers.TryGetValue(status, out var handler))
            {
                throw new InvalidOperationException("Handler not found");
            }
         
        
                handler.UpdateStatus(purchaseOffer, product, repository);
            
      
           
        }
    }
}
