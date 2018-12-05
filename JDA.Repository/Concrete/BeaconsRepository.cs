using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using JDA.Common;
using JDA.Common.Helpers;
using JDA.Entities;
using JDA.Entities.Request;
using JDA.Entities.Response;
using JDA.Repository.Contracts;
using JDA.Repository.DalResponse;
using JDA.Repository.DbConstants;
using Serilog;

namespace JDA.Repository.Concrete
{
    public class BeaconsRepository : IBeaconsRepository
    {
        private readonly ILogger _logger;

        public BeaconsRepository(ILogger logger)
        {
            _logger = logger;
        }
        public async Task<ServiceResponse<List<BeaconResponse>>> GetBeacons(IDbConnection connection, BeaconRequest request, int userId, string token)
        {
            var response = new ServiceResponse<List<BeaconResponse>>() { IsSuccess = true, TokenStatus = TokenStatus.NotRequired, Data = new List<BeaconResponse>()};
            try
            {
                var validateUserParams = new DynamicParameters();
                validateUserParams.Add(DbParamNames.UserId, userId);
                //validateUserParams.Add(DbParamNames.SecurityToken, token);
                if (request.BeaconId > 0)
                {
                    validateUserParams.Add(DbParamNames.BeaconId, request.BeaconId);
                }

                validateUserParams.Add(DbParamNames.UUID, request.UUID);
                validateUserParams.Add(DbParamNames.Major, request.Major);
                validateUserParams.Add(DbParamNames.Minor, request.Minor);

                var data = (await connection.QueryAsync<dynamic>(SpConstants.GetAllBeacons, validateUserParams, commandType: CommandType.StoredProcedure)).Select(x => new BeaconResponse
                {
                    BeaconId = x.BeaconId,
                    UUID = x.UUID,
                    Major = x.Major,
                    Minor = x.Minor,
                    BeaconLocation = x.BeaconLocation
                });
                response.Data = data.ToList();
                _logger.Information("Get All Beacons Success");
            }
            catch (SqlException ex)
            {
                _logger.Error(ex.Message);
                response.IsSuccess = false;
                response.Message = ServiceConstants.ErrorCodes.ContainsKey(ex.Number) ? ServiceConstants.ErrorCodes[ex.Number] : ApiMessages.UnknownError;
            }
            catch (Exception ex)
            {
                _logger.Error("Error : {ex}", ex);
                response.IsSuccess= false;
                response.Message = ApiMessages.InternalError;
            }
            return response;
        }
    }
}
