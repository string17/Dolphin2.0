using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Response
{
    public class StateResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public List<StateDetailsObj> StateDetails { get; set; }
    }



    public class StateDetailsObj
    {
        public int StateId { get; set; }
        public string StateTitle { get; set; }
        public string StateDesc { get; set; }
        public bool IsStateActive { get; set; }
    }
}
