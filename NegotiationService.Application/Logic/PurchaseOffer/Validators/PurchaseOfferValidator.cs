using NegotiationService.Application.Logic.PurchaseOffer.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NegotiationService.Application.Logic.PurchaseOffer.Validators
{
    public class PurchaseOfferValidator
    {
        public PurchaseOfferValidator()
        {
        }
        public void Validate(CreatePurchaseOfferRequest request)
        {
            List<string> errors = new List<string>();

            if (request.ProductId < 0)
            {
                errors.Add("Product Id cannot be null or less than 0");
            }
            if (request.Price <= 0)
            {
                errors.Add("Price cannot be negative or 0");
            }
            if (request.Quantity <= 0)
            {
                errors.Add("Quantity cannot be negative or 0");
            }
            if (string.IsNullOrEmpty(request.CustomerEmail))
            {
                errors.Add("Email is required");
            }
            else if (!Regex.IsMatch(request.CustomerEmail, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errors.Add("Invalid email format");
            }

            if (errors.Any())
            {
                throw new ArgumentException(string.Join(Environment.NewLine, errors));
            }
        }
    }
}
