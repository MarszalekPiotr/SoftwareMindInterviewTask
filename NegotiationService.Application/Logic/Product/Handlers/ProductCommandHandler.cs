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
using NegotiationService.Domain.Entities;
using NegotiationService.Application.Logic.Product.Validators;

namespace NegotiationService.Application.Logic.Product.Handlers
{
    public class ProductCommandHandler : BaseCommandHandler
    {
        private readonly IGenericRepository<Domain.Entities.Product> _productRepository;
        private readonly ProductValidator _validator;
        public ProductCommandHandler(IAuthService authService,
            IGenericRepository<Domain.Entities.Product> repository, IGenericRepository<Domain.Entities.Product> productRepository, ProductValidator validator)
            : base(authService)
        {
            _productRepository = productRepository;
            _validator = validator;
        }

        public async Task<CreateProductResult> Handle(CreateProductRequest request)
        {
            _validator.Validate(request);
            var user = await _authService.GetCurrentUser();
            if (user == null)
            {
                throw new Exception("User not found");
            }
            var product = new Domain.Entities.Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Quantity = request.Quantity,
                UserId = user.Id
            };
            _productRepository.Add(product);
            await _productRepository.SaveChangesAsync();
            return new CreateProductResult
            {
                ProductId = product.Id
            };
        }
    }
}
