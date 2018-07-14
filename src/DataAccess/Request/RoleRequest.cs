using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Request
{
    public class RoleRequest
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDesc { get; set; }
        public bool IsRoleActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string SystemIp { get; set; }
        public string Computername { get; set; }
    }
}
