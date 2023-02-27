using BusinessAccess.Services.Implement;
using BusinessAccess.Services.Interface;
using Common;
using DataAccess.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Security.Authorization;

namespace FStoreAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpPost]
        public async Task<Response<string>> CreateProduct([FromBody] Product data)
        {
            await _productService.addProduct(data);

            Response<string> res = new Response<string>() { Message="product has been created"};
    
            return res;
        }
    }
}
