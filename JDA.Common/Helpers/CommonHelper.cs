
using System;
using JDA.Common;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace JDA.Common.Helpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class CommonHelper
    {
        /// <summary>
        /// Retrieves the security token.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="logger"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public static int GetCardHolderAndToken(IHttpContextAccessor context, ILogger logger, out string token)
        {
            token = string.Empty;
            try
            {
                token = context.HttpContext.Request.Headers[ServiceConstants.SecurityToken][0];
                //Card Holder ID
                return Convert.ToInt32(context.HttpContext.Request.Headers[ServiceConstants.UserId][0]);
            }
            catch(Exception ex)
            {
                logger.Error("Error : {ex}", ex);
                return 0;
            }
        }
    }
}
