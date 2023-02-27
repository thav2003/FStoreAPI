
using System.Drawing;

namespace DataAccess.Model
{
    public enum Role
    {
        Admin=1,
        Manger=2,
        Customer=3,
        Shipper=4
    }
    public class RoleDto
    {
        public Role Id { get; set; }
        public string Name { get; set; }
    }
}
