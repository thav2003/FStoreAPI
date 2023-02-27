
using BusinessAccess.Services.Interface;
using Security.Utility;

namespace FStoreAPI.Middlewares
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId!=null)
            {
                context.Items["User"] = await userService.getById(userId.Value);
            }
            await _next(context);
        }
  
    }
}
