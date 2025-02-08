using Microsoft.EntityFrameworkCore;
using NegotiationService.Application.Interfaces;
using NegotiationService.Application.Logic.PurchaseOffer.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Application.Logic.StatusManagers
{
    public  class TransactionStatusManager
    {
        private readonly IGenericRepository<Domain.Entities.PurchaseOffer> _purchaseOfferRepository;
        private readonly ProductStatusManager _productStatusManager;
        public TransactionStatusManager(IGenericRepository<Domain.Entities.PurchaseOffer> purchaseOfferRepository, ProductStatusManager productStatusManager)
        {
            _purchaseOfferRepository = purchaseOfferRepository;
            _productStatusManager = productStatusManager;
        }


        public async Task<bool> CheckTransactionAvailability(CreatePurchaseOfferRequest request, List<string> messages)
        {    


            var customerOffers = await _purchaseOfferRepository.GetAll().
                Where(x => x.ProductId == request.ProductId && x.CustomerEmail == request.CustomerEmail)
                .ToListAsync();           

            if (customerOffers.Any())
            {
                var lastOffer = customerOffers
                    .OrderByDescending(x => x.CreatedDate)
                    .FirstOrDefault();


                if (TransactionSettings.notValidStatusMessages.ContainsKey(lastOffer.Status))
                {
                    messages.Add(TransactionSettings.notValidStatusMessages[lastOffer.Status]);
                    return false;
                }


                if (customerOffers.Count >= TransactionSettings.MaxOffersAmount)
                {
                    messages.Add("You have reached the maximum number of offers for this product");
                    return false;

                }


                if (DateTimeOffset.UtcNow.Subtract(lastOffer.CreatedDate).Minutes> TransactionSettings.MaxAnswerDelayInMinutes)
                {
                    messages.Add("You have reached the maximum time to answer the offer");
                    return false;
                }

               


            }
            var consistencyResult = await CheckTransactionConsistency(request.ProductId, request.Quantity, messages);
            if (!consistencyResult)
            {    
                messages.Add("The transaction is not possible");
                return false;
            }

                return true;
        }

        public async Task<bool> CheckTransactionConsistency(int productId,int quantity, List<string> messages)
        {
            var availableQuantity = await _productStatusManager.GetAvailableQuantity(productId);
            if (quantity > availableQuantity)
            {
                messages.Add("The quantity exceeds the available product's amount");
                return false;
            }
            return true;

        }
    }
}
