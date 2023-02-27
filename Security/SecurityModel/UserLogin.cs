using System.ComponentModel.DataAnnotations;


namespace Security.SecurityModel
{
    public class UserLogin
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
