using System.ComponentModel.DataAnnotations;
using System.Net;

namespace NegotiationService.Presentation.Middlewares
{
    public class ExceptionResultMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionResultMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, ILogger<ExceptionResultMiddleware> logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception e)
            {
                
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                await httpContext.Response.WriteAsJsonAsync(e.Message);
            }
        }
    }
}
