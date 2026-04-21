using Application.Services.ProductService.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.ProductService
{
    public interface IProductService
    {
        public Task<List<GetProductDto>> GetProducts();
        public Task<GetProductDto> GetProduct(int id);
        public Task AddProduct(AddProductDto item);
        public Task UpdateProduct(int id, AddProductDto item);
        public Task DeleteProduct(int id);
    }
}
