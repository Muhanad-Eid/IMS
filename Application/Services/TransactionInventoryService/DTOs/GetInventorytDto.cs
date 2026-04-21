using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Application.Services.TransactionInventoryService.DTOs
{
    public class GetInventorytDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedOn { get; set; }
        public TransactionType Type { get; set; }
        public string TypeStr { get 
            {
                return Type.ToString();
            }
        }
    }
}
