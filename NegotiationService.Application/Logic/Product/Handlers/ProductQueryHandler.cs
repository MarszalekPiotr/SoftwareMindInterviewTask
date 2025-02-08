using Microsoft.EntityFrameworkCore;
using NegotiationService.Application.DTO;
using NegotiationService.Application.Interfaces;
using NegotiationService.Application.Interfaces.Services;
using NegotiationService.Application.Logic.Abstract;
using NegotiationService.Application.Logic.Product.Requests;
using NegotiationService.Application.Logic.Product.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Application.Logic.Product.Handlers
{
    public class ProductQueryHandler : BaseQueryHandler
    {
        private readonly IGenericRepository<Domain.Entities.Product> _productRepository;
        public ProductQueryHandler(IAuthService authService,
            IGenericRepository<Domain.Entities.Product> productRepository) : base(authService)
        {
            _productRepository = productRepository;
        }

        public async Task<GetProductListResult> Handle(GetProductListRequest request)
        {
               

             var products = await _productRepository.GetAll().Select(p => new ProductDTO()
             {
                 Id = p.Id,
                 Name = p.Name,
                 Description = p.Description,
                 Price = p.Price,
                 Quantity = p.Quantity,
                 Owner = p.User.UserName
             }).ToListAsync();

            return new GetProductListResult()
            {
                products = products
            };


        }

        public async Task<GetProductResult> Handle(GetProductRequest request)
        {
            var product = await _productRepository.GetByIdAsync(request.ProductId);
            if (product == null)
            {
                throw new Exception("Product not found");
            }

            var owner = await _authService.GetUserById(product.UserId);
            var productDto = new ProductDTO
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Quantity = product.Quantity,
                Owner = owner.UserName
            };
            return new GetProductResult()
            {
                ProductDTO = productDto
            };
        }



    }
}
