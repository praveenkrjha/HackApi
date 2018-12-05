using System;
using System.Collections.Generic;
using System.Text;

namespace JDA.Entities.Request
{
    public class BeaconRequest
    {
        public int BeaconId { get; set; }

        public string UUID { get; set; }

        public int Major { get; set; }

        public int Minor { get; set; }
    }
}
