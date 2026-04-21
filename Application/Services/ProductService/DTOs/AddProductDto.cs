using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.ProductService.DTOs
{
    public class AddProductDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public decimal SalePrice { get; set; }
    }
}
