using BusinessAccess.DTO;
using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Interface
{
    public interface IOrderService
    {
        Task<Order> addOrder(CreateOrderDTO orderDTO);
        Task<Order> getById(int id);

        Task addOrderDetail(Order order, List<OrderDetailDTO> data);

        Task<List<Order>> getPending();
        Task<List<Order>> getDone();
        Task<List<Order>> getAll();

        Task<Order> updateOrder(Order order);
    }
}
