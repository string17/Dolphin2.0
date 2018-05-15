using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CustomObjects
{
    public class RegionResp
    {
        public string RespCode { get; set; }
        public string RespMessage { get; set; }
    }

    public class RegionObj
    {
        public int RegionId { get; set; }
        public string RegionTitle { get; set; }
        public string StateDesc { get; set; }
        public bool IsRegionActive { get; set; }
    }
}
