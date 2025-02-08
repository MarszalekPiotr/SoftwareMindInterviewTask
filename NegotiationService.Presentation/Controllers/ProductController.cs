using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace NegotiationService.Presentation.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductController : ControllerBase
    {   
        [HttpPost]
        public async Task<IActionResult> CreateProduct()
        {     
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {

            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            return Ok();
        }

    }
}
