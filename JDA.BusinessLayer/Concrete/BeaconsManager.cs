using System;
using System.Collections.Generic;
using System.Threading.Tasks;
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

namespace JDA.BusinessLayer.Concrete
{
    public class BeaconsManager : IBeaconsManager
    {
        private readonly IBeaconsRepository _repository;
        private readonly ConnectionStrings _connectionStrings;
        public BeaconsManager(IBeaconsRepository repository, IOptions<ConnectionStrings> connectionStrings)
        {
            _repository = repository;
            _connectionStrings = connectionStrings.Value;
        }

        public async Task<ServiceResponse<List<BeaconResponse>>> GetBeacons(IHttpContextAccessor context, BeaconRequest request)
        {
            var response = new ServiceResponse<List<BeaconResponse>>() { IsSuccess = true, TokenStatus = TokenStatus.NotRequired, Data = new List<BeaconResponse>() };

            var securityToken = context.HttpContext.Request.Headers[ServiceConstants.SecurityToken][0];
            var userId = Convert.ToInt32(context.HttpContext.Request.Headers[ServiceConstants.UserId][0]);
            if (userId <= 0)
            {
                response.IsSuccess = false;
                response.TokenStatus = TokenStatus.Invalid;
                return response;
            }

            using (IDalContext dalContext = new DalContext(_connectionStrings.HackConnection))
            {
                response =  await _repository.GetBeacons(dalContext.DbConnection, request, userId, securityToken);
            }

            return response;
        }
    }
}
