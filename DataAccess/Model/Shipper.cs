
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    [Index("UserId", IsUnique = true)]
    public class Shipper : Document
    {
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        public int UserId { get; set; }

     
        public virtual List<Order> Orders { get; set; }
    }
}
