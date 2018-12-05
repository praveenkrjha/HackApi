using Dapper;
using JDA.Common;
using JDA.Common.Helpers;
using JDA.Entities;
using JDA.Entities.Request;
using JDA.Repository.Contracts;
using JDA.Repository.DalEntities;
using JDA.Repository.DbConstants;
using Serilog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using JDA.Repository.DalResponse;

namespace JDA.Repository.Concrete
{
    /// <summary>
    /// JDA.Repository.Concrete.RegisterUserRepository class for register user repository
    /// </summary>
    /// <seealso cref="JDA.Repository.Contracts.IRegisterUserRepository" />
    public class RegisterUserRepository : IRegisterUserRepository
    {
        readonly ILogger _logger;

        public RegisterUserRepository(ILogger logger)
        {
            _logger = logger;
        }


        public async Task<RegisterUserDalResponse> RegisterUser(IDbConnection connection, RegisterUserRequest request, string encryptedToken, int userId)
        {
            _logger.Information("Call RegisterUserRepository : RegisterUser");
            var response = new RegisterUserDalResponse { IsSuccess = true, TokenStatus = TokenStatus.Valid };
            var cardId = new List<int>();
            try
            {
                //Add DB parameters               
                var registerUser = new DynamicParameters();
                registerUser.Add(DbParamNames.EmailId, request.EmailId);
                registerUser.Add(DbParamNames.Password, request.Password);
                registerUser.Add(DbParamNames.SecurityToken, encryptedToken);
                registerUser.Add(DbParamNames.DeviceModel, request.DeviceModel);
                registerUser.Add(DbParamNames.DeviceManufacturer, request.DeviceManufacturer);
                registerUser.Add(DbParamNames.DeviceRegistrationId, request.DeviceRegistrationId);
                registerUser.Add(DbParamNames.UserId, userId);

                //Execute SP                  

                using (var dalResponse = await connection.QueryMultipleAsync(SpConstants.RegisterUser, registerUser, commandType: CommandType.StoredProcedure))
                {
                    var cardHolderDetail = dalResponse.Read<RegisterUserDalEntities>().FirstOrDefault();
                    response.UserDetails = new UserDetails
                    {
                        UserId = cardHolderDetail.UserId,
                        Email = cardHolderDetail.Email,
                        FirstName = cardHolderDetail.FirstName,
                        LastName = cardHolderDetail.LastName,
                    };

                    response.IsSuccess = true;
                    response.TokenStatus = TokenStatus.Valid;

                    _logger.Debug("Registered CardholderId with email id : " + request.EmailId);
                }
            }
            catch (SqlException ex)
            {
                _logger.Error("JDA.Repository.Concrete RegisterUserRepository" + ex.Message);
                //Checking for Invalid Token
                if (ex.Number == ServiceConstants.NolongerRegistered)
                {
                    response.TokenStatus = TokenStatus.Invalid;
                }
                response.IsSuccess = false;
                response.ErrorMessage = ServiceConstants.ErrorCodes.ContainsKey(ex.Number) ? ServiceConstants.ErrorCodes[ex.Number] : ApiMessages.UnknownError;
            }
            catch (Exception ex)
            {
                _logger.Error("Error : {ex}", ex);
                response = new RegisterUserDalResponse { IsSuccess = false, ErrorMessage = ApiMessages.InternalError };
            }
            return response;
        }
    }
}
