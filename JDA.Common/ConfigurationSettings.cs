using System.Collections.Generic;
using System.Xml.Linq;

namespace JDA.Common
{
    public class ConnectionStrings
    {
        public string HackConnection { get; set; }
    }

    public class AppSettings
    {
        public string MachineConfigPath { get; set; }
        public string RedisHost { get; set; }
    }
}
