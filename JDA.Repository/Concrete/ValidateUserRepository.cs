using Dapper;
using Serilog;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using JDA.Common;
using JDA.Common.Helpers;
using JDA.Entities;
using JDA.Repository.Contracts;
using JDA.Repository.DalRequest;
using JDA.Repository.DalResponse;
using JDA.Repository.DbConstants;

namespace JDA.Repository.Concrete
{
    public class ValidateUserRepository : IValidateUserRepository
    {
        readonly ILogger _logger;

        public ValidateUserRepository(ILogger logger)
        {
            _logger = logger;
        }

        public async Task<ValidateDalResponse> ValidateUser(IDbConnection connection, ValidateDalRequest request)
        {
            _logger.Information("Call ValidateUserRepository : ValidateUser");
            _logger.Information("Validating email id : " + request.EmailId);

            var response = new ValidateDalResponse { IsSuccess = true, TokenStatus = TokenStatus.NotRequired };
            try
            {
                var validateUserParams = new DynamicParameters();
                validateUserParams.Add(DbParamNames.EmailId, request.EmailId);

                response = (await connection.QueryAsync<dynamic>(SpConstants.ValidateUser, validateUserParams, commandType: CommandType.StoredProcedure)).Select(x => new ValidateDalResponse()
                {                    
                    UserId = x.UserId,
                    UserName = x.FirstName
                }).FirstOrDefault();
                if (response == null)
                {
                    response = new ValidateDalResponse { IsSuccess = true, TokenStatus = TokenStatus.Valid };
                }
                else
                {
                    response.IsSuccess = true;
                    response.TokenStatus = TokenStatus.Valid;
                }
                _logger.Information("Validated Email id : " + request.EmailId);
            }
            catch (SqlException ex)
            {
                _logger.Error(ex.Message);
                response.IsSuccess = false;
                response.ErrorMessage = ServiceConstants.ErrorCodes.ContainsKey(ex.Number) ? ServiceConstants.ErrorCodes[ex.Number] : ApiMessages.UnknownError;
            }
            catch (Exception ex)
            {
                _logger.Error("Error : {ex}", ex);
                response = new ValidateDalResponse { IsSuccess = false, ErrorMessage = ApiMessages.InternalError };                
            }
            return response;
        }
    }
}
