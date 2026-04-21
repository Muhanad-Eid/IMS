using Application.Services.TransactionInventoryService;
using Application.Services.TransactionInventoryService.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionInventoryController : ControllerBase
    {
        private readonly ITransactionInventoryService _transactionInventoryService;
        public TransactionInventoryController(ITransactionInventoryService transactionInventoryService)
        {
            _transactionInventoryService = transactionInventoryService;
        }
        [HttpPost("CreateTransaction")]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateInventorytDto input)
        {
            await _transactionInventoryService.CreateTransaction(input);
            return Ok();
        }

        [HttpGet("GetAllTransactions")]
        public async Task<IActionResult> GetAllTransactions()
        {
            var result = await _transactionInventoryService.GetAllTransactions();
            return Ok(result);
        }

        [HttpGet("GetTransactionById")]
        public async Task<IActionResult> GetTransactionById(int id)
        {
            var result = await _transactionInventoryService.GetTransactionById(id);
            return Ok(result);
        }
    }
}
