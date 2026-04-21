using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.OrderService.DTOs
{
    public class GetListOrderDto
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public string? UserName { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
