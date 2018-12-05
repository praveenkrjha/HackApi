using JDA.Common;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using System;

namespace JDA.BusinessLayer.Concrete
{
    /// <summary>
    /// JDA.BusinessLayer.Concrete.RedisManager class for redis manager
    /// </summary>
    public class RedisManager
    {
        public static AppSettings AppSettings;
        
        /// <summary>
        /// The lazy connection
        /// </summary>
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            string host = AppSettings.RedisHost;
            try
            {
                return ConnectionMultiplexer.Connect(host);
            }
            catch (Exception ex)
            {
                return null;
            }
        });

        /// <summary>
        /// Get RedisConnection
        /// </summary>
        /// <value>
        /// The redis connection.
        /// </value>
        public static ConnectionMultiplexer RedisConnection
        {
            get
            {
                return LazyConnection.Value;
            }
        }
    }
}


