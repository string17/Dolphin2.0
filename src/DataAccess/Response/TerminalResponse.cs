using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Response
{
    public class TerminalResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public List<TerminalDetailsObj> TerminalDetails { get; set; }

        
    }

    public class TerminalDetailsObj
    {
        public int? TerminalId { get; set; }
        public string TerminalNo { get; set; }
        public string TerminalRef { get; set; }
        public string SerialNo { get; set; }
        public int? ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientAlias { get; set; }
        public int? BrandId { get; set; }
        public string BrandName { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Engineer { get; set; }
        public bool? IsUnderSupport { get; set; }
        public bool? IsTerminalActive { get; set; }
        public string Location { get; set; }
        public string TerminalAlias { get; set; }
        public int? StateId { get; set; }
        public string StateTitle { get; set; }
        public string RegionTitle { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string Computername { get; set; }
        public string SystemIp { get; set; }
    }
}
