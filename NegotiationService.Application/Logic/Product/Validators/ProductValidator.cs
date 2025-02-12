using NegotiationService.Application.Logic.Product.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Application.Logic.Product.Validators
{
    public class ProductValidator
    {
        public ProductValidator()
        {
        }

        public void Validate(CreateProductRequest request)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(request.Name))
            {
                errors.Add("Name is required");
            }
            if (string.IsNullOrEmpty(request.Description))
            {
                errors.Add("Description is required");
            }
            if(request.Price <= 0)
            {
                errors.Add("Price cannot be null or negative");
            }
            if(request.Quantity <= 0)
            {
                errors.Add("Quantity cannot be null or negative");
            }

            if (errors.Any())
            {
                throw new Exception(string.Join(Environment.NewLine, errors));    
            }
        }
    }
}