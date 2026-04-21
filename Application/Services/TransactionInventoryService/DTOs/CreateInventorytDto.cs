using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.Services.TransactionInventoryService.DTOs
{
    public class CreateInventorytDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public TransactionType Type { get; set; }
    }
}
