using Application.Services.OrderService.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.OrderService
{
    public interface IOrderService
    {
        Task CreateOrder(CreateOrderDto order);
        Task<GetOrderDto> GetOrder(int id);
        Task<List<GetListOrderDto>> GetOrders();

    }
}
