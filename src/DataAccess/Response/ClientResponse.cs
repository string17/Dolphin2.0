using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Response
{
    public class ClientResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public List<ClientDetailsObj> ClientDetails { get; set; }
    }


    public class ClientDetailsObj
    {
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int RespTimeIn { get; set; }
        public int RestTimeIn { get; set; }
        public int RespTimeOut { get; set; }
        public int RestTimeOut { get; set; }
        public string ClientBanner { get; set; }
        public string ClientAlias { get; set; }
        public bool? IsClientActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
    }
}
