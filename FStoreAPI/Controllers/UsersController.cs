using BusinessAccess.Services.Interface;
using DataAccess.Model;
using Microsoft.AspNetCore.Mvc;
using Common;
using Security.Authorization;
using BusinessAccess.Filter;
using BusinessAccess.Helpers;
using BusinessAccess.Services.Implement;
using Security.SecurityModel;
using BusinessAccess.DTO;
using Security.Utility;

namespace FStoreAPI.Controllers
{

    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController
    {

        public static IWebHostEnvironment _env;
        private readonly IUserService _userService;
        private readonly IUriService _uriService;
        private readonly IJwtUtils jwtUtils;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public UsersController(IUserService userInfoService, IUriService uriService, IWebHostEnvironment env, IHttpContextAccessor httpContextAccessor, IJwtUtils jwtUtils)
        {
            _userService = userInfoService;
            _uriService = uriService;
            _httpContextAccessor = httpContextAccessor;
            this.jwtUtils = jwtUtils;
            _env = env;
        }

        [HttpGet("{id:int}")]
        public async Task<Response<User>> GetUserById(int id)
        {
            var result = await _userService.getById(id);

            return new Response<User>(result);
        }

        [HttpGet()]
        public async Task<PageResponse<List<User>>> Find([FromQuery] PaginationFilter filter)
        {
           
            var httpClient=new HttpClient();
            var request = _httpContextAccessor.HttpContext!.Request;
            var route = request.Path.Value;

            var validFilter = new PaginationFilter(filter.PageNumber, filter.PageSize);
            var pageData = await _userService.FindWithFilter(filter);
      
            var totalRecords = await _userService.countAll();
            var pageReponse = PaginationHelper.CreatePagedReponse<User>(pageData, validFilter, totalRecords, _uriService, route);

            return pageReponse;
        }

        [HttpPost("update-password")]
        public async Task<Response<AuthResponse>> UpdatePassword([FromBody] UpdatePasswordDTO data)
        {
            var currentUser = (User)_httpContextAccessor.HttpContext.Items["User"];
            Console.WriteLine(data.Password);
            if (currentUser == null || !BCrypt.Net.BCrypt.Verify(data.PasswordCurrent, currentUser.Password))
            {
                throw new BadRequestError("Your current password is wrong");
            }
            if (data.Password != data.PasswordConfirm)
            {
                throw new BadRequestError("Password confirm not same");
            }

            currentUser.Password = BCrypt.Net.BCrypt.HashPassword(data.Password);
            await _userService.updateUser(currentUser);

            var token = jwtUtils.GenerateJwtToken(currentUser);

            return new Response<AuthResponse>(new AuthResponse(currentUser, token));
        }

    
        [HttpPut]
        public async Task<Response<User>> UpdateUserName([FromBody] User data)
        {
            var currentUser = (User)_httpContextAccessor.HttpContext.Items["User"];

            //update user
            currentUser.FullName = data.FullName;

            User updatedUser = await _userService.updateUser(currentUser);
            Response<User> res = new Response<User>(updatedUser);
            res.Message = "update successful !!";
            return res;


        }


        [HttpPut("update-avatar")]
        public async Task<Response<User>> UpdateAvatar([FromForm] FileUpload data)
        {
            var currentUser = (User)_httpContextAccessor.HttpContext.Items["User"];
            if (!Directory.Exists(_env.WebRootPath + "\\Upload\\"))
            {
                Directory.CreateDirectory(_env.WebRootPath + "\\Upload\\");
            }
            FileStream fs = System.IO.File.Create(_env.WebRootPath + "\\Upload\\" + currentUser.Id + "-" + data.files.FileName);
            data.files.CopyTo(fs);
            fs.Flush();


            //update photo
            currentUser.Photo = currentUser.Id + "-" + data.files.FileName;

            User updatedUser = await _userService.updateUser(currentUser);
            Response<User> res = new Response<User>(updatedUser);
            res.Message = "update successful !!";
            return res;
        }


    }
}
