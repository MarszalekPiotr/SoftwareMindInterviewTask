using NegotiationService.Application.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NegotiationService.Application.Logic.Product.Results
{
    public  class GetProductListResult
    {
        public List<ProductDTO> products { get; set; }
    }
}
