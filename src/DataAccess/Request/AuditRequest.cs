using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Request
{
    public class AuditRequest
    {
        public string AuditId { get; set; }
        public string UserName { get; set; }
        public string UserActivity { get; set; }
        public string Comment { get; set; }
        public DateTime EventDate { get; set; }
        public string SystemName { get; set; }
        public string SystemIp { get; set; }
    }
}
