

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;


namespace DataAccess.Model
{
    public class User : Document
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Photo { get; set; } = "default.jpg";
        public string? Address { get; set; }
        public string? Phone { get; set; }
        public Role Role { get; set; } = Role.Customer;
        public int IsVerified { get; set; }=0;
    }
}
