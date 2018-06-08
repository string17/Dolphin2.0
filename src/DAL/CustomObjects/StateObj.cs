using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CustomObjects
{
    public class StateResp
    {
        public string RespCode { get; set; }
        public string RespMessage { get; set; }
    }

    public class StateObj
    {
        public int StateId { get; set; }
        public string StateTitle { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public bool IsStateActive { get; set; }
        public string Computername { get; set; }
        public string SystemIp { get; set; }
    }
}
