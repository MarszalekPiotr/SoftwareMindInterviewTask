using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NegotiationService.Application.Logic.Product.Handlers;
using NegotiationService.Application.Logic.Product.Requests;

namespace NegotiationService.Presentation.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductCommandHandler _productCommandHandler;
        private readonly ProductQueryHandler _productQueryHandler;
        public ProductController(ProductCommandHandler productCommandHandler, ProductQueryHandler productQueryHandler)
        {
            _productCommandHandler = productCommandHandler;
            _productQueryHandler = productQueryHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest request)
        {
            var result = await _productCommandHandler.Handle(request);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var result = _productQueryHandler.Handle(new GetProductRequest() { ProductId = id });
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery]GetProductListRequest request)
        {
            var result = await _productQueryHandler.Handle(request);
            return Ok(result); 
        }

    }
}
