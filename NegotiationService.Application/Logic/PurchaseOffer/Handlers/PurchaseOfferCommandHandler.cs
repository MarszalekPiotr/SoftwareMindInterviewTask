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
        IGenericRepository<Domain.Entities.Product> _productRepository;
        private readonly PurchaseOfferValidator _validator;
        private readonly TransactionStatusManager _transactionStatusManager;
        private readonly ProductStatusManager _productStatusManager;
      
        public PurchaseOfferCommandHandler(IAuthService authService, IGenericRepository<Domain.Entities.PurchaseOffer> purchaseOfferRepository, PurchaseOfferValidator validator, TransactionStatusManager transactionStatusManager, ProductStatusManager productStatusManager, IGenericRepository<Domain.Entities.Product> productRepository) : base(authService)
        {
            _purchaseOfferRepository = purchaseOfferRepository;
            _validator = validator;
            _transactionStatusManager = transactionStatusManager;
            _productStatusManager = productStatusManager;
            _productRepository = productRepository;
        }

        public async Task<CreatePurchaseOfferResult> Handle(CreatePurchaseOfferRequest request)
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
                CreatedDate = DateTimeOffset.UtcNow
                


            };

            _purchaseOfferRepository.Add(purchaseOffer);
            await _purchaseOfferRepository.SaveChangesAsync();

            return new CreatePurchaseOfferResult()
            {
                PurchaseOfferId = purchaseOffer.Id
            };
        }

        public async Task<UpdatePurchaseOfferStatusResult> Handle(UpdatePurchaseOfferStatusRequest request)
        {    
            var purchaseOffer = await _purchaseOfferRepository.GetByIdAsync(request.PurchaseOfferId);
            var product = _productRepository.GetAll().FirstOrDefault(p => p.Id == purchaseOffer.ProductId);
            var currentUser = await _authService.GetCurrentUser();
            if (purchaseOffer == null)
            {
                throw new Exception("Purchase offer not found");
            }
            if (product == null) {
                throw new Exception("Product not found");
            }
            if (product.UserId != currentUser.Id )
            {
                throw new AccessViolationException("You are not the owner of this product");    
            }

            var errorMessages = new List<string>();
            if (request.Status == Domain.Enums.EnumOfferStatus.Accepted)
            {

                var transactionPossible = await _transactionStatusManager.CheckTransactionConsistency(product.Id,
                    purchaseOffer.Quantity, errorMessages);
                if (!transactionPossible)
                {
                    StringBuilder sb = new StringBuilder();
                    errorMessages.ForEach(x =>
                    {
                        sb.AppendLine(x);
                    });
                    throw new Exception(sb.ToString());

                }

                purchaseOffer.Status = Domain.Enums.EnumOfferStatus.Accepted;
                // notify customer
            }
            else if (request.Status == Domain.Enums.EnumOfferStatus.Rejected)
            {    
                if(purchaseOffer.Status == Domain.Enums.EnumOfferStatus.Accepted)
                {
                    throw new Exception("You can't reject an offer that hss already been  accepted");
                }
                purchaseOffer.Status = Domain.Enums.EnumOfferStatus.Rejected;
            }
            else
            {
                throw new Exception("Invalid status");
            }

            await _purchaseOfferRepository.SaveChangesAsync();

            return new UpdatePurchaseOfferStatusResult()
            {
                PurchaseOfferId = purchaseOffer.Id
            };
        }
    }
}
