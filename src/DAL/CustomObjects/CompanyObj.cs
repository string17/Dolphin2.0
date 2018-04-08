using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL.CustomObjects
{
    public class CompanyObj
    {
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string RespTime { get; set; }
        public string RestTime { get; set; }
        public string Rest1Time { get; set; }
        public HttpPostedFileBase CustomerBanner { get; set; }
        public string CustomerAlias { get; set; }
        public bool? CustomerStatus { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
