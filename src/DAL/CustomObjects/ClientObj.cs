using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL.CustomObjects
{
    public class ClientResp
    {
        public string RespCode { get; set; }
        public string RespMessage { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int? RespTime { get; set; }
        public int? RestTime { get; set; }
        public int? RespTimeUp { get; set; }
        public int? RestTimeUp { get; set; }
        public string ClientBanner { get; set; }
        public string ClientAlias { get; set; }
        public bool? IsClientActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }


    public class ClientObj
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int RespTime { get; set; }
        public int RestTime { get; set; }
        public int RespTimeUp { get; set; }
        public int RestTimeUp { get; set; }
        public string Rest1Time { get; set; }
        public string ClientBanner { get; set; }
        public string ExtClientBanner { get; set; }
        public string ClientAlias { get; set; }
        public bool IsClientActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string UserName { get; set; }
        public string Computername { get; set; }
        public string SystemIp { get; set; }
    }



    public class ExtClientObj
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string RespTime { get; set; }
        public string RestTime { get; set; }
        public int RespTimeUp { get; set; }
        public int RestTimeUp { get; set; }
        public string Rest1Time { get; set; }
        public string ClientBanner { get; set; }
        public string ClientAlias { get; set; }
        public bool IsClientActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Computername { get; set; }
        public string SystemIp { get; set; }
    }


}
