using Application.Services.ProductService.DTOs;
using Application.Services.TransactionInventoryService.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.TransactionInventoryService
{
    public interface ITransactionInventoryService
    {
        public Task<List<GetInventorytDto>> GetAllTransactions();
        public Task<GetInventorytDto> GetTransactionById(int id);
        public Task CreateTransaction(CreateInventorytDto item);
    }
}
