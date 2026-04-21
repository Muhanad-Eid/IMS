using Application.Repositories;
using Application.Services.OrderService.DTOs;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services.OrderService
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _orderRepository ;
        private readonly IGenericRepository<OrderDetail> _orderDetailRepository ;
        private readonly IGenericRepository<Product> _productRepository ;
        public OrderService(IGenericRepository<Order> orderRepository, IGenericRepository<OrderDetail> orderDetailRepository, IGenericRepository<Product> productRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;
            _productRepository = productRepository;
        }
        public async Task CreateOrder(CreateOrderDto input)
        {
            var order = new Order
            {
                Number = input.Number,
                UserId = input.UserId,
            };

            await _orderRepository.CreateAsync(order);
            await _orderRepository.SaveChangesAsync();


            var orderDetails = new List<OrderDetail>();
            foreach (var item in input.OrderDetails)
            {
                var unitPrice = (await _productRepository.GetByIdAsync(item.ProductId)).SalePrice;
                orderDetails.Add(new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                    UnitPrice = unitPrice
                });
            }


            await _orderDetailRepository.CreateRangeAsync(orderDetails);
            await _orderDetailRepository.SaveChangesAsync();
        }
        public async Task<GetOrderDto> GetOrder(int id)
        {
            var data = await _orderRepository.GetAll()
                .Include(x => x.User)
                .Include(x => x.OrderDetails).ThenInclude(x => x.Product)
                .FirstOrDefaultAsync(x => x.Id == id);

            var result = new GetOrderDto
            {
                Id = data.Id,
                UserName = data.User.Name,
                Number = data.Number,
                TotalAmount = data.TotalAmount,
                CreatedOn = data.CreatedOn,
                OrderDetails = data.OrderDetails.Select(x => new GetOrderDetailsDto
                {
                    Id = x.Id,
                    ProductName = x.Product.Name,
                    Quantity = x.Quantity,
                    UnitPrice = x.UnitPrice
                }).ToList()
            };

            return result;
        }

        public async Task<List<GetListOrderDto>> GetOrders()
        {
            var data = await _orderRepository.GetAll()
                .Include(x => x.User)
                .Select(x => new GetListOrderDto
                {
                    Id = x.Id,
                    UserName = x.User.Name,
                    Number = x.Number,
                    TotalAmount = x.TotalAmount,
                    CreatedOn = x.CreatedOn,
                }).ToListAsync();

            return data;
        }
    }
}
