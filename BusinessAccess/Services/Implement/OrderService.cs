using BusinessAccess.DTO;
using BusinessAccess.Repository.Implement;
using BusinessAccess.Repository.Interface;
using BusinessAccess.Services.Interface;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Implement
{
    public class OrderService : IOrderService
    {
        private readonly IRepository<Order> _orderRepository;
        private readonly IRepository<OrderDetail> _orderDetailRepository;
        public OrderService(IRepository<Order> orderRepository, IRepository<OrderDetail> orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _orderDetailRepository = orderDetailRepository;

        }
        public async Task<Order> addOrder(CreateOrderDTO orderDTO)
        {

            Order order = new Order
            {
               UserId = orderDTO.User.Id,
               OrderDate = orderDTO.OrderDate,
               Price = orderDTO.Price,
            };
            await _orderRepository.Insert(order);

            /*await _orderDetail.createDeatil(data);*/
            return order;
        }

        public async Task addOrderDetail(Order order, List<OrderDetailDTO> data)
        {
            OrderDetail[] listDetail = new OrderDetail[data.Count];
            for (int i = 0; i < data.Count; i++) {
                listDetail[i] = new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = data[i].ProductId,
                    Quantity = data[i].Quantity,
                    Price = data[i].Price * data[i].Quantity,
                };
            }
            await _orderDetailRepository.InsertMany(listDetail);
        }

        public async Task<Order> getById(int id)
        {
            return await _orderRepository.FindOne(o => o.Id == id);
        }

        public async Task<List<Order>> getPending()
        {
            return await _orderRepository.FindAll(o => o.Status == 0);
        }
        public async Task<List<Order>> getDone()
        {
            return await _orderRepository.FindAll(o => o.Status == 1);
        }
        public async Task<List<Order>> getAll()
        {
            return await _orderRepository.FindAll(o => true);
        }

        public async Task<Order> updateOrder(Order order)
        {
            await _orderRepository.Update(order);
            return order;
        }
    }
}
