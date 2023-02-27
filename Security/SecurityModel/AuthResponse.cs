
using DataAccess.Model;
using System.ComponentModel.DataAnnotations;

namespace Security.SecurityModel
{
    public class AuthResponse
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string Email { get; set; }
        public Role Role { get; set; }
        public string Token { get; set; }

        public AuthResponse(User user,string token)
        {
            Id = user.Id;
            FullName = user.FullName;
            Email = user.Email;
            Role=user.Role;
            Token = token;
        }
    }
}
