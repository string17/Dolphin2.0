using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CustomObjects
{
    public class BrandResp
    {
        public string RespCode { get; set; }
        public string RespMessage { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string BrandDesc { get; set; }
        public bool? IsBrandActive { get; set; }
        public string Computername { get; set; }
        public string SystemIp { get; set; }
    }

    public class BrandObj
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string BrandDesc { get; set; }
        public bool IsBrandActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string UserName { get; set; }
        public string Computername { get; set; }
        public string SystemIp { get; set; }

    }
}
