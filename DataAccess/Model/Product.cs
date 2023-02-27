using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Product : Document
    {
        public string Name { get; set; }
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
