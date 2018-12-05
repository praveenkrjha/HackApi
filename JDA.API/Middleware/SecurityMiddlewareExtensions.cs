using Microsoft.AspNetCore.Builder;

namespace JDA.API.Middleware
{
    /// <summary>
    /// JDA.API.Middleware.SecurityMiddlewareExtensions class for security middleware extensions
    /// </summary>
    public static class SecurityMiddlewareExtensions
    {
        /// <summary>
        /// Uses the security middleware.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseSecurityMiddleware(this IApplicationBuilder builder)
        {
            builder.UseMiddleware<SecurityMiddleware>();
            return builder;
        }
    }
}
