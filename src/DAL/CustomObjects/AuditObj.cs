using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CustomObjects
{
    public class AuditObj
    {
        public string AuditId { get; set; }
        public string UserName { get; set; }
        public string UserActivity { get; set; }
        public string Comment { get; set; }
        public DateTime EventDate { get; set; }
        public string SystemName { get; set; }
        public string SystemIp { get; set; }
    }

    public class AuditResp
    {
        public string RespCode { get; set; }
        public string RespMessage { get; set; }
    }
}
