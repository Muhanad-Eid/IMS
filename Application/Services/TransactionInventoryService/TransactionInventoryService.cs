using Application.Repositories;
using Application.Services.TransactionInventoryService.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.TransactionInventoryService
{
    public class TransactionInventoryService : ITransactionInventoryService
    {
        private readonly IGenericRepository<TransactionInventory> _transactionRepository;
        private readonly IGenericRepository<Product> _productRepository;
        public TransactionInventoryService(IGenericRepository<TransactionInventory> transactionRepository, IGenericRepository<Product> productRepository)
        {
            _transactionRepository = transactionRepository;
            _productRepository = productRepository;
        }
        public async Task CreateTransaction(CreateInventorytDto input)
        {
            if (input.Quantity <= 0)
            {
                throw new Exception("CurrentStock should be graeter than quantity");
            }

            var product = await _productRepository.GetByIdAsync(input.ProductId);

            if (input.Type == TransactionType.StockIn)
            {
                product.CurrentStock += input.Quantity;
                await _productRepository.UpdateAsync(product);
            }
            else
            {
                if (product.CurrentStock < input.Quantity)
                {
                    throw new Exception("CurrentStock should be graeter than quantity");
                }
                product.CurrentStock -= input.Quantity;
                await _productRepository.UpdateAsync(product);
            }

            var data = new TransactionInventory
            {
                ProductId = input.ProductId,
                Quantity = input.Quantity,
                Type = input.Type,
                TransactionDate = DateTime.UtcNow,
            };

            await _transactionRepository.CreateAsync(data);
            await _transactionRepository.SaveChangesAsync();
        }


        public async Task<List<GetInventorytDto>> GetAllTransactions()
        {
            var data = await _transactionRepository.GetAll()
                .Include(x => x.Product)
                .Select(x => new GetInventorytDto
                {
                    Id = x.Id,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    CreatedOn = x.TransactionDate,
                    Type = x.Type
                }).ToListAsync();

            return data;
        }

        public async Task<GetInventorytDto> GetTransactionById(int id)
        {
            var data = await _transactionRepository.GetAll()
                            .Include(x => x.Product)
                            .FirstOrDefaultAsync(x => x.Id == id); 
            var transaction = new GetInventorytDto
            {
                Id = data.Id,
                ProductName=data.Product.Name,
                Quantity = data.Quantity,
                CreatedOn = data.TransactionDate,
                Type = data.Type

            };
            return transaction;
        }
    }
}
