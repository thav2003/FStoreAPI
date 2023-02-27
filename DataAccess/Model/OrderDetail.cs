using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class OrderDetail : Document
    {
        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
        public int? OrderId { get; set; }
        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }
    }
}
