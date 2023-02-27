using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Order : Document
    {
        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public int? ShipperId { get; set; }
        [ForeignKey("ShipperId")]
        [InverseProperty("Orders")]
        public virtual Shipper? Shipper { get; set; }
        public int Status { get; set; } = 0; // 0 pending, 1 done , 2 cancel
        [Column(TypeName ="Money")]
        public decimal Price { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
