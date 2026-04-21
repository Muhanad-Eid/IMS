using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.Services.ProductService.DTOs
{
    public class GetProductDto
    {
        public int Id { get; set; } 
        public string Name { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string Category { get; set; }
        public decimal SalePrice { get; set; }
        public int CurrentStock { get; set; }
    }
}
