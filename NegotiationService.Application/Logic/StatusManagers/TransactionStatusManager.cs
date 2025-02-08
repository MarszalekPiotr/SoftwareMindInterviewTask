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


            var offersCount = await _purchaseOfferRepository.GetAll()
                .Where(x => x.ProductId == request.ProductId && x.CustomerEmail == request.CustomerEmail)
                .CountAsync();

            if(offersCount >= TransactionSettings.MaxOffersAmount)
            {
                messages.Add("You have reached the maximum number of offers for this product");
                return false  ; 

            }

            var lastOffer = await _purchaseOfferRepository.GetAll().
                Where(x => x.ProductId == request.ProductId && x.CustomerEmail == request.CustomerEmail)
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync();

            if (DateTimeOffset.UtcNow.Subtract(lastOffer.CreatedDate).Hours > TransactionSettings.MaxAnswerDelayInHours)
            {
                messages.Add("You have reached the maximum time to answer the offer");
                return false;
            }

            if (TransactionSettings.notValidStatusMessages.ContainsKey(lastOffer.Status))
            {
                messages.Add(TransactionSettings.notValidStatusMessages[lastOffer.Status]);
                return false;
            }

            var availableQuantity = await _productStatusManager.GetAvailableQuantity(request.ProductId);
            if (request.Quantity > availableQuantity)
            {
                messages.Add("The quantity you are trying to buy is not available");
                return false;
            }


            return true;


        }
    }
}
