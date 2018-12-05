using Microsoft.AspNetCore.Builder;

namespace JDA.API.Middleware
{
    /// <summary>
    /// JDA.API.Middleware.RequestResponseLoggingMiddlewareExtensions class for request response logging middleware extensions
    /// </summary>
    public static class RequestResponseLoggingMiddlewareExtensions
    {
        /// <summary>
        /// Uses the request response logging.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseRequestResponseLogging(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<RequestLoggingMiddleware>();
           // builder.UseMiddleware<ResponseLoggingMiddleware>();
            return builder;
        }
    }
}
