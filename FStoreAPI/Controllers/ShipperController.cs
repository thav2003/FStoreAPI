using BusinessAccess.Services.Implement;
using BusinessAccess.Services.Interface;
using Common;
using DataAccess.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShipperController : ControllerBase
    {
        public static IWebHostEnvironment _env;

        private readonly IShipperService _shipperService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ShipperController(IShipperService shipperService, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor)
        {
            _env = env;
            _httpContextAccessor = httpContextAccessor;
            _shipperService = shipperService;
        }

        [HttpGet]
        public async Task<Response<Shipper>> GetAllOrder()
        {
            var currentUser = (User)_httpContextAccessor.HttpContext.Items["User"];
           Shipper res = await _shipperService.getOwnOrder(currentUser);
            return new Response<Shipper>(res);

        }
    }
}
