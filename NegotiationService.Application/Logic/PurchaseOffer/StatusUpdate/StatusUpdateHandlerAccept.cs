using Microsoft.EntityFrameworkCore;
using NegotiationService.Application.Interfaces;
using NegotiationService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Application.Logic.PurchaseOffer.StatusUpdate
{
    public class StatusUpdateHandlerAccept : StatusUpdateHandler
    {    
            
        public StatusUpdateHandlerAccept()
        {
           
        }
        public override EnumOfferStatus EnumOfferStatus => EnumOfferStatus.Accepted;
     
        public override async Task UpdateStatus(Domain.Entities.PurchaseOffer purchaseOffer, Domain.Entities.Product product,
            IGenericRepository<Domain.Entities.PurchaseOffer> repository)
        {
            if(purchaseOffer.Status == EnumOfferStatus.Accepted)
            {
                throw new Exception("offer has been accepted already");
            }

            if(purchaseOffer.Quantity > product.Quantity)
            {
                throw new InvalidOperationException("Not enough products");
            }

              await repository.BeginTransactionAsync();

            try
            {
                var updatedRows = await repository.ExecuteRawSqlAsync(
                 "UPDATE Products SET Quantity = Quantity - {0} WHERE Id = {1} AND Quantity >= {0};",
                  purchaseOffer.Quantity, purchaseOffer.ProductId
);

                if (updatedRows == 0)
                {
                    throw new Exception("Stock was already taken");
                }

                purchaseOffer.Status = EnumOfferStatus.Accepted;
                await repository.SaveChangesAsync();

                await repository.CommitTransactionAsync();



            }
            catch (Exception e)
            {
                await repository.RollbackTransactionAsync();
                throw;
            }
    }
    
    }
}
