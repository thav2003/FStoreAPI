using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Service  : Document
    {
        public string Name  { get; set; }
        public string Description { get; set; }
        public int Unit { get; set; }
        public double Price { get; set; }
    }
}
