using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using JDA.BusinessLayer.Contracts;
using JDA.Common;
using JDA.Common.Helpers;
using JDA.Entities.Response;
using JDA.Repository;
using JDA.Repository.DbConstants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Serilog;
using StackExchange.Redis;

namespace JDA.BusinessLayer.Concrete
{
    public class AttendanceManager : IAttendanceManager
    {
        private const string Attendancekey = "Hack-AttendanceKey";
        private readonly ConnectionStrings _connectionStrings;
        private static IDatabase _redisDb;

        private readonly AppSettings _appSettings;
        private readonly ILogger _logger;

        public AttendanceManager(ILogger logger, IOptions<AppSettings> appSettings, IOptions<ConnectionStrings> connectionStrings)
        {
            _logger = logger;
            _appSettings = appSettings.Value;
            _connectionStrings = connectionStrings.Value;
        }

        void InitializeRedisDb()
        {
            if (_redisDb != null) return;

            RedisManager.AppSettings = _appSettings;
            ConnectionMultiplexer redis = RedisManager.RedisConnection;// redis connection             
            if (redis != null)
            {
                _redisDb = redis.GetDatabase();
            }
        }

        public async Task<bool> MarkAttendance(IHttpContextAccessor context)
        {
            var userId = Convert.ToInt32(context.HttpContext.Request.Headers[ServiceConstants.UserId][0]);
            try
            {
                InitializeRedisDb();
                //_redisDb.StringSet("key67647364", "Praveen");
                var key = await _redisDb.StringSetBitAsync(Attendancekey, userId, true);
                //var ddd = key.ToString();
            }
            catch (Exception ex)
            {
                return false;
                Console.WriteLine(ex);
            }

            return true;
        }

        public async Task<List<AttendenceData>> GetAttendanceData(IHttpContextAccessor context)
        {
            //var userId = Convert.ToInt32(context.HttpContext.Request.Headers[ServiceConstants.UserId][0]);
            try
            {
                InitializeRedisDb();

                List<AttendenceData> attendance = new List<AttendenceData>();
                using (IDalContext dalContext = new DalContext(_connectionStrings.HackConnection))
                {
                    var data = (await dalContext.DbConnection.QueryAsync<dynamic>("SELECT UserId, FirstName, LastName from UserDetails", null, commandType: CommandType.Text)).Select(x => new AttendenceData
                    {
                        UserId = x.UserId,
                        FirstName = x.FirstName,
                        LastName = x.LastName
                    });

                    if (data.Any())
                    {
                        attendance.AddRange(data);
                        foreach (var attendanceData in attendance)
                        {
                            attendanceData.Presence = await _redisDb.StringGetBitAsync(Attendancekey, attendanceData.UserId);
                        }
                    }
                }

                return attendance;
            }
            catch (Exception ex)
            {
                return null;
                Console.WriteLine(ex);
            }
        }
    }
}
