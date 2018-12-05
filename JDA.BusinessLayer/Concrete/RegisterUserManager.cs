using JDA.BusinessLayer.Contracts;
using JDA.Common;
using JDA.Common.Helpers;
using JDA.Entities;
using JDA.Entities.Request;
using JDA.Entities.Response;
using JDA.Repository;
using JDA.Repository.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JDA.BusinessLayer.Concrete
{
    public class RegisterUserManager : IRegisterUserManager
    {
        private readonly AppSettings _appSettings;
        private readonly IRegisterUserRepository _registerUserRepository;
        private readonly ConnectionStrings _connectionStrings;
        private readonly ILogger _logger;

        public RegisterUserManager(IRegisterUserRepository registerUserRepository, ILogger logger, IOptions<ConnectionStrings> connectionStrings, IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _registerUserRepository = registerUserRepository;
            _connectionStrings = connectionStrings.Value;
            _logger = logger;
        }
        public async Task<ServiceResponse<RegisterUserResponse>> RegisterUser(IHttpContextAccessor context, RegisterUserRequest request)
        {
            _logger.Information("Call RegisterUserManager : RegisterUser");
            var response = new ServiceResponse<RegisterUserResponse> { IsSuccess = true, TokenStatus = TokenStatus.Valid, Data = new RegisterUserResponse() };

            string externalNumber = String.Empty;
            using (IDalContext dalContext = new DalContext(_connectionStrings.HackConnection))
            {
                try
                {
                    //Security Token
                    var securityToken = context.HttpContext.Request.Headers[ServiceConstants.SecurityToken][0];
                    var userId = Convert.ToInt32(context.HttpContext.Request.Headers[ServiceConstants.UserId][0]);
                    if (userId <= 0)
                    {
                        response.IsSuccess = false;
                        response.TokenStatus = TokenStatus.Invalid;
                    }
                    else
                    {
                        var dalResponse = await _registerUserRepository.RegisterUser(dalContext.DbConnection, request, securityToken, userId);


                        //Check Response
                        if (!dalResponse.IsSuccess)
                        {
                            response.IsSuccess = false;
                            response.Message = dalResponse.ErrorMessage ?? ApiMessages.InternalError;
                        }
                        else
                        {

                            response.IsSuccess = true;
                            response.Data.SecurityToken = securityToken;
                            response.Data.UserDetails = dalResponse.UserDetails;

                            //WriteLog
                            _logger.Information("User : " + request.EmailId + " registered");
                        }
                    }
                }
                catch (Exception ex)
                {  //Log error
                    response.Message = ApiMessages.InternalError;
                    _logger.Error(ex.Message);
                }
            }
            //Return response
            return response;
        }
    }
}
