using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.OrderService.DTOs
{
    public class GetOrderDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string? UserName { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<GetOrderDetailsDto> OrderDetails { get; set; }
    }

    public class GetOrderDetailsDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
