using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Linq;

namespace JDA.API.Middleware
{
    /// <summary>
    /// JDA.API.Middleware.SecurityMiddleware class for security middleware
    /// </summary>
    public class SecurityMiddleware
    {
        /// <summary>
        /// We are using a fixed Request Token that each client needs to provide for every API call.
        /// This can be changed to dynamic token per client if required in future. 
        /// However, we don't see a need for that as each authenticated API also requires a SecurityToken to be passed by client.
        /// </summary>
        public const string RequestToken = "B7A6A1F06D8A48BE826BBD184D0BBE17F224C661DABBD9B581ADAA7F2D56A375";

        private readonly RequestDelegate _next;

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityMiddleware"/> class.
        /// </summary>
        /// <param name="next">The next.</param>
        public SecurityMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        /// <summary>
        /// Invokes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            //Don't check for RequestToken for swagger
            if(context.Request.Path.ToString().ToLower().IndexOf("swagger") >= 0)
            {
                await _next(context);
                return;
            }
            if (!context.Request.Headers.Keys.Contains("RequestToken"))
            {
                context.Response.StatusCode = 400; //Bad Request                
                await context.Response.WriteAsync("RequestToken is missing");
                return;
            }
            if (!context.Request.Headers["RequestToken"][0].Equals(RequestToken))
            {
                context.Response.StatusCode = 403; //Forbidden
                await context.Response.WriteAsync("Invalid RequestToken");
                return;
            }
            await _next(context);
        }
    }
}
