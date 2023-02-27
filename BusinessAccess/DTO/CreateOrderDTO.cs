using DataAccess.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessAccess.DTO
{
    public class CreateOrderDTO
    {
        public User User { get; set; }
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
