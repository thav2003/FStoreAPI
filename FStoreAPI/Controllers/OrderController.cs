using BusinessAccess.DTO;
using BusinessAccess.Repository.Interface;
using BusinessAccess.Services.Interface;
using Common;
using DataAccess.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Security.Authorization;

namespace FStoreAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        public static IWebHostEnvironment _env;

        private readonly IOrderService _orderService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public OrderController(IOrderService orderService, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            _orderService = orderService;
        }

/*        [Authorize(Role.Customer, Role.Manger, Role.Admin)]*/
        [HttpPost]
        public async Task<Response<Order>> CreateOrder([FromBody] CreateOrderDetailDTO dataDTO)
        {
            var currentUser = (User)_httpContextAccessor.HttpContext.Items["User"];

            if (dataDTO.data.IsNullOrEmpty())
            {
                throw new NotFoundError("Not found data");
            }

            CreateOrderDTO createOrderDTO = new CreateOrderDTO
            {
                User = currentUser,
                Price = dataDTO.data.Sum(x => x.Price * x.Quantity),
                OrderDate = new DateTime()
            };

            Order newOrder = await _orderService.addOrder(createOrderDTO);

            await _orderService.addOrderDetail(newOrder, dataDTO.data);


            return new Response<Order>(newOrder) { Message = "Create bill successful !!" };
        }

       /* [Authorize(Role.Shipper, Role.Manger, Role.Admin)]*/
        [HttpGet("pending")]
        public async Task<Response<List<Order>>> GetAllPendingOrder()
        {
            List<Order> res = await _orderService.getPending();
            return new Response<List<Order>>(res);

        }

/*        [Authorize(Role.Shipper, Role.Manger, Role.Admin)]*/
        [HttpGet("done")]
        public async Task<Response<List<Order>>> GetAllDoneOrder()
        {
            List<Order> res = await _orderService.getDone();
            return new Response<List<Order>>(res);

        }
/*
        [Authorize(Role.Shipper, Role.Manger, Role.Admin)]*/
        [HttpGet]
        public async Task<Response<List<Order>>> GetAllOrder()
        {
            List<Order> res = await _orderService.getAll();
            return new Response<List<Order>>(res);

        }

/*        [Authorize(Role.Manger, Role.Admin)]*/
        [HttpPut]
        public async Task<Response<Order>> UpdateOrderShipper([FromBody] UpdateOrderShipper data)
        {
            Order order = await _orderService.getById(data.OrderId);

            order.ShipperId = data.ShipperId;

            await _orderService.updateOrder(order);

            Response<Order> res = new Response<Order>(order);
            res.Message = "update successful !!";
            return res;
        }


    }
}
