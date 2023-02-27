
using BusinessAccess.Repository.Interface;
using BusinessAccess.Services.Interface;
using DataAccess.Model;

namespace BusinessAccess.Services.Implement
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        public ProductService(IRepository<Product> productRepository) 
        {
            _productRepository = productRepository;
        }
        public async Task addProduct(Product product)
        {
            await _productRepository.Insert(product);
        }
    }
}
