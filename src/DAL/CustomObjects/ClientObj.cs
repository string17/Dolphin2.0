using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL.CustomObjects
{
    public class ClientObj
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string RespTime { get; set; }
        public string RestTime { get; set; }
        public string Rest1Time { get; set; }
        public HttpPostedFileBase ClientBanner { get; set; }
        public string ClientAlias { get; set; }
        public bool? IsClientActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }



    public class ExtClientObj
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string RespTime { get; set; }
        public string RestTime { get; set; }
        public string Rest1Time { get; set; }
        public string ClientBanner { get; set; }
        public string ClientAlias { get; set; }
        public bool? IsClientActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
