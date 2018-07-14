using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataAccess.Request
{
    public class ClientRequest
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public string ClientAlias { get; set; }
        public int RespTimeIn { get; set; }
        public int RestTimeIn { get; set; }
        public int RespTimeOut { get; set; }
        public int RestTimeOut { get; set; }
        public HttpPostedFileBase BannerFile { get; set; }
        public string ClientBanner { get; set; }
        public bool IsClientActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Computername { get; set; }
        public string SystemIp { get; set; }
    }
}
