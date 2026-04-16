using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Domain.Entities
{
    public class TransactionInventory
    {
        public int Id { get; set; } 
        public int ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime TransactionDate { get; set; }
        public TransactionType Type { get; set; }
    }
}
public enum TransactionType
{
    StockIn=1,
    StockOut=2
}