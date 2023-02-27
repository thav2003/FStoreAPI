using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.Services.Interface
{
    public interface IProductService
    {
        Task addProduct(Product product);
    }
}
