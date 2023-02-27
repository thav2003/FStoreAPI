using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Model
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        public int IsDeleted { get; set; }=0;
        public DateTime? updatedAt { get; set; }
        public DateTime? createdAt { get; set; }
    }
}
