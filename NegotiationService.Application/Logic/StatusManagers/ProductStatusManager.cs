using Microsoft.EntityFrameworkCore;
using NegotiationService.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Application.Logic.StatusManagers
{
    public class ProductStatusManager
    {   
        private readonly IGenericRepository<Domain.Entities.Product> _productRepository;
        private readonly IGenericRepository<Domain.Entities.PurchaseOffer> _purchaseOfferRepository;
        public ProductStatusManager(IGenericRepository<Domain.Entities.Product> productRepository, IGenericRepository<Domain.Entities.PurchaseOffer> purchaseOfferRepository)
        {
            _productRepository = productRepository;
            _purchaseOfferRepository = purchaseOfferRepository;
        }
        public async Task<bool> CheckAvailability(int productId, List<string> messages)
        {
             Console.WriteLine("");

            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                messages.Add("Product not found");
                return false;
            }

            var availableQuantity =  await GetAvailableQuantity(productId);

            if ((availableQuantity < 0))
            {
                messages.Add("Product is not available");
                return false;
            }
            return true;
            
        }

        public async Task<int> GetAvailableQuantity(int productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            if (product == null)
            {
                return 0;
            }
            

            var soldProductsQuantity = await _purchaseOfferRepository.GetAll().Where(po => po.ProductId == productId)
                .Where(po => po.Status == Domain.Enums.EnumOfferStatus.Accepted)
                .SumAsync(po => po.Quantity);
            return product.Quantity - soldProductsQuantity;
        }
    }
}
