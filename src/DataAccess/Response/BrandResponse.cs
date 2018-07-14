using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Response
{
    public class BrandResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public List<BrandDetailsObj> BrandDetails { get; set; }
      }



    public class BrandDetailsObj
    {
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public string BrandDesc { get; set; }
        public bool? IsBrandActive { get; set; }
    }
}
