using Application.Repositories;
using Application.Services.ProductService.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.ProductService
{
    public class ProductService : IProductService
    {
        private readonly IGenericRepository<Product> _productRepository;
        public ProductService(IGenericRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<List<GetProductDto>> GetProducts()
        {
            var data = _productRepository.GetAll().Include(x => x.Category);
            var products =await  data.Select(p => new GetProductDto
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                CategoryId = p.CategoryId,
                Category = p.Category.Name,
                SalePrice = p.SalePrice,
                CurrentStock = p.CurrentStock,
            }).ToListAsync();
            return products;
        }
        public async Task<GetProductDto> GetProduct(int id)
        {
            var data = await _productRepository.GetAll()
                                               .Include(x => x.Category)
                                               .FirstOrDefaultAsync(p => p.Id == id);

            var product = new GetProductDto
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                CategoryId = data.CategoryId,
                Category = data.Category.Name,
                SalePrice = data.SalePrice,
                CurrentStock = data.CurrentStock,
            };
            return product;
        }

        public async Task AddProduct(AddProductDto item)
        {
            var name = item.Name.Trim();
            var existingProduct = await _productRepository.GetAll().AnyAsync(u => u.Name == name);
            if (existingProduct)
            {
                throw new Exception("A product with the same name already exists.");
            }
            var data = new Product
            {
                Id=0,
                Name = item.Name,
                Description = item.Description,
                CategoryId = item.CategoryId,
                SalePrice = item.SalePrice,
            };
            await _productRepository.CreateAsync(data);
            await _productRepository.SaveChangesAsync();
        }
        public async Task UpdateProduct(int id, AddProductDto item)
        {
            var existingProduct = await _productRepository.GetAll().AnyAsync(u => u.Name.ToLower().Trim() == item.Name.ToLower().Trim() && u.Id != id);
            if (existingProduct)
            {
                throw new Exception("A product with the same name already exists.");
            }
            var data = await _productRepository.GetByIdAsync(id);
            data.Name = item.Name;
            data.Description = item.Description;
            data.CategoryId = item.CategoryId;
            data.SalePrice = item.SalePrice;
            await _productRepository.UpdateAsync(data);
            await _productRepository.SaveChangesAsync();
        }
        public async Task DeleteProduct(int id)
        {
            var data = await _productRepository.GetByIdAsync(id);
            _productRepository.Delete(data);
            await _productRepository.SaveChangesAsync();

        }
    }
}