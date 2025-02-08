using NegotiationService.Application.Interfaces;
using NegotiationService.Application.Interfaces.Services;
using NegotiationService.Application.Logic.Abstract;
using NegotiationService.Application.Logic.PurchaseOffer.Requests;
using NegotiationService.Application.Logic.PurchaseOffer.Results;
using NegotiationService.Application.Logic.PurchaseOffer.Validators;
using NegotiationService.Application.Logic.StatusManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Application.Logic.PurchaseOffer.Handlers
{
    public class PurchaseOfferCommandHandler : BaseCommandHandler
    {
        private readonly IGenericRepository<Domain.Entities.PurchaseOffer> _purchaseOfferRepository;
        private readonly PurchaseOfferValidator _validator;
        private readonly TransactionStatusManager _transactionStatusManager;
        private readonly ProductStatusManager _productStatusManager;
        public PurchaseOfferCommandHandler(IAuthService authService, IGenericRepository<Domain.Entities.PurchaseOffer> purchaseOfferRepository, PurchaseOfferValidator validator, TransactionStatusManager transactionStatusManager, ProductStatusManager productStatusManager) : base(authService)
        {
            _purchaseOfferRepository = purchaseOfferRepository;
            _validator = validator;
            _transactionStatusManager = transactionStatusManager;
            _productStatusManager = productStatusManager;
        }

        public async Task<CreatePurchaseOfferResult> CreatePurchaseOffer(CreatePurchaseOfferRequest request)
        {
            _validator.Validate(request);

            StringBuilder sb = new StringBuilder();
            List<string> errorMessages = new List<string>();


            var productAvailability = await _productStatusManager.CheckAvailability(request.ProductId, errorMessages);
            if (!productAvailability)
            {
                errorMessages.ForEach(x =>
                {
                    sb.AppendLine(x);
                });

                throw new Exception(sb.ToString());
            }

            var purchaseOfferPossible = await _transactionStatusManager.CheckTransactionAvailability(request, errorMessages);

            if (!purchaseOfferPossible)
            {
                errorMessages.ForEach(x =>
                {
                    sb.AppendLine(x);
                });
                throw new Exception(sb.ToString());
            }

            var purchaseOffer = new Domain.Entities.PurchaseOffer()
            {
                ProductId = request.ProductId,
                Quantity = request.Quantity,
                Price = request.Price,
                CustomerEmail = request.CustomerEmail,
                Status = Domain.Enums.EnumOfferStatus.Pending,
                CreatedDate = DateTimeOffset.Now
                


            };

            _purchaseOfferRepository.Add(purchaseOffer);
            await _purchaseOfferRepository.SaveChangesAsync();

            return new CreatePurchaseOfferResult()
            {
                PurchaseOfferId = purchaseOffer.Id
            };
        }
    }
}
