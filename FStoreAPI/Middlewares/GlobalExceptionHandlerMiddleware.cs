
using Common;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.X509.Qualified;
using System.Net;
using System.Text.Json;

namespace FStoreAPI.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HanldeExceptionAsync(context, ex);
            }
        }
        private static Task HanldeExceptionAsync(HttpContext context, Exception exception) {
            HttpStatusCode status;
            var strackTrace = string.Empty;
            string message;
            var exceptionType = exception.GetType();

            if (exceptionType == typeof(BadRequestError))
            {
                message = exception.Message;
                status = HttpStatusCode.BadRequest;
                strackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(NotFoundError))
            {
                message = exception.Message;
                status = HttpStatusCode.NotFound;
                strackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(UnauthorizedError))
            {
                message = exception.Message;
                status = HttpStatusCode.Unauthorized;
                strackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(ForbiddenError))
            {
                message = exception.Message;
                status = HttpStatusCode.Forbidden;
                strackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(MissingFieldError))
            {
                message = exception.Message;
                status = HttpStatusCode.BadRequest;
                strackTrace = exception.StackTrace;
            }
            else if (exceptionType == typeof(InternalError))
            {
                message = exception.Message;
                status = HttpStatusCode.InternalServerError;
                strackTrace = exception.StackTrace;
            }
            else
            {
                message = exception.Message;
                status = HttpStatusCode.InternalServerError;
                strackTrace = exception.StackTrace;
            }
            /* var exceptionResult = JsonSerializer.Serialize(new { error = exception.Message.Length>0 ? exception.Message: "api khong ton tai", code = (int)status });*/
            Response<string> res = new Response<string>() { Message = exception.Message, };

            var exceptionResult = JsonSerializer.Serialize(res);
            Console.WriteLine("Gọi api");
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)status;

            return context.Response.WriteAsync(exceptionResult);
        }
    }
}
