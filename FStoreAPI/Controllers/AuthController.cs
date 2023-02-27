using BusinessAccess.Services.Interface;
using DataAccess.Model;
using Microsoft.AspNetCore.Mvc;
using Security.SecurityModel;
using Security.Utility;
using Microsoft.AspNetCore.Authorization;
using Common;
using MailKit;
using Org.BouncyCastle.Asn1.Pkcs;

namespace FStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IJwtUtils jwtUtils;
        private readonly IEmailService mail;
        private readonly IUriService uriService;
        public AuthController(IUserService userService, IEmailService mail, IJwtUtils jwtUtils, IUriService uriService)
        {
            this.userService = userService;
            this.jwtUtils = jwtUtils;
            this.mail = mail;
            this.uriService = uriService;
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<Response<AuthResponse>> Login(UserLogin userLogin)
        {

            if (userLogin.Email == null || userLogin.Email.Length == 0)
            {
                throw new MissingFieldError("Email");
            }
            if (userLogin.Password == null || userLogin.Password.Length == 0)
            {
                throw new MissingFieldError("Password");
            }

            User user = await userService.getUserByEmail(userLogin.Email);
            if (user == null || !BCrypt.Net.BCrypt.Verify(userLogin.Password, user.Password))
            {
                throw new ForbiddenError("Wrong user name or password !!!");
            }

            var token = jwtUtils.GenerateJwtToken(user);

            return new Response<AuthResponse>(new AuthResponse(user, token));

        }

        [AllowAnonymous]
        [HttpGet("verify")]
        public string VerifyAccount([FromQuery]string id, [FromQuery] string token)
        {
            return $"Your id: {id}, your token: {token}";
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<Response<string>> Register(UserRegister userRegister)
        {
            if (userRegister.FullName == null || userRegister.FullName.Length == 0)
            {
                throw new MissingFieldError("FullName");
            }
            if (userRegister.Phone == null || userRegister.Phone.Length == 0)
            {
                throw new MissingFieldError("Phone");
            }
            if (userRegister.Email==null || userRegister.Email.Length == 0){
                throw new MissingFieldError("Email");
            }
            if (userRegister.Password == null || userRegister.Password.Length == 0)
            {
                throw new MissingFieldError("Password");
            }
            User user = await userService.registerUser(userRegister);
            var token = jwtUtils.GenerateJwtToken(user);

            var url = uriService.GetVerifyUri(user, token);
            /*var url = $"https://localhost/api/auth/verify?id={user.Id}&token={token}";*/
         
            MailData mailData = new MailData()
            {
                To = new List<string> { userRegister.Email },
                DisplayName="Admin FStore",
                Subject= "Verify Acoount",
                Body= $"{url}"
            };
            await mail.SendAsync(mailData, new CancellationToken());

            return new Response<string>() { Message = "Register successful !!" };
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        public async Task<Response<string>> ForgotPassword(UserRegister userRegister) 
        {

            return null;
        }

    }
}
