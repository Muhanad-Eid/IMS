using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.OrderService.DTOs
{
    public class CreateOrderDto
    {
        public string Number { get; set; }
        public int UserId { get; set; }
        public List<CreateOrderDetailsDto> OrderDetails { get; set; }
    }

    public class CreateOrderDetailsDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }

    }
}
