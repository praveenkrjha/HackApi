using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Linq;
using JDA.Common;
using JDA.Common.Helpers;
using System.Text;

namespace JDA.API.Helpers
{
    /// <summary>
    /// JDA.API.Helpers.TokenAuthorization class for token authorization
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Authorization.AuthorizationHandler{Helpers.TokenAuthorization}" />
    /// <seealso cref="Microsoft.AspNetCore.Authorization.IAuthorizationRequirement" />
    public class TokenAuthorization : AuthorizationHandler<TokenAuthorization>, IAuthorizationRequirement
    {
        /// <summary>
        /// Makes a decision if authorization is allowed based on a specific requirement.
        /// </summary>
        /// <param name="context">The authorization context.</param>
        /// <param name="requirement">The requirement to evaluate.</param>
        /// <returns></returns>
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TokenAuthorization requirement)
        {
            var httpContext = ((Microsoft.AspNetCore.Mvc.ActionContext)context.Resource).HttpContext;
            var headers = httpContext.Request.Headers[ServiceConstants.SecurityToken];
            if (!headers.Any())
            {
                context.Fail();
                return;
            }   
            var token = headers[0];
            if(string.IsNullOrWhiteSpace(token))
            {
                context.Fail();
                return;
            }
            var tokenDetails = Utilities.ValidateSecurityToken(headers);
            if (tokenDetails.TokenStatus == Entities.TokenStatus.NotProvided || tokenDetails.TokenStatus == Entities.TokenStatus.Invalid)
            {
                //If we are unable to decrypt token, it means it is invalid.
                context.Fail();
                var message = Encoding.UTF8.GetBytes("Invalid token");
                httpContext.Response.OnStarting(async () =>
                {
                    httpContext.Response.StatusCode = 403;
                    await httpContext.Response.Body.WriteAsync(message, 0, message.Length);
                });
                return;
            }
            var cardHolderId = tokenDetails.UserId;
            httpContext.Request.Headers.Add(ServiceConstants.UserId, tokenDetails.UserId.ToString());

            context.Succeed(requirement);
        }
    }
}
