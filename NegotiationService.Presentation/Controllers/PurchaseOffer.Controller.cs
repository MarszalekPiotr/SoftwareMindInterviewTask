using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NegotiationService.Application.DTO;
using NegotiationService.Application.Logic.PurchaseOffer.Handlers;
using NegotiationService.Application.Logic.PurchaseOffer.Requests;

namespace NegotiationService.Presentation.Controllers
{
    [Route("api/purchase-offers")]
    [ApiController]
    public class PurchaseOffer : ControllerBase
    {

        private readonly PurchaseOfferCommandHandler _purchaseOfferCommandHandler;

        public PurchaseOffer(PurchaseOfferCommandHandler purchaseOfferCommandHandler)
        {
            _purchaseOfferCommandHandler = purchaseOfferCommandHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePurchaseOffer([FromBody] CreatePurchaseOfferRequest request)
        {
            var result = await _purchaseOfferCommandHandler.Handle(request);
            return Ok(result);
        }

        [Authorize]
        [HttpPatch("{offerId}/status")]
        public async Task<IActionResult> UpdateStatus(int offerId,[FromBody] UpdatePurchaseOfferStatusDTO request)
        {   
            
            var command = new UpdatePurchaseOfferStatusRequest()
            {
                OfferId = offerId,
                Status = request.Status
            };
            var result = await _purchaseOfferCommandHandler.Handle(request);
            return Ok(result);
        }
    }
}
