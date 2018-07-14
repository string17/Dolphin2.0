using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Response
{
    public class MenuResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public List<MenuDetailsObj> MenuDetails { get; set; }
    }

    public class MenuDetailsObj
    {
        public int MenuId { get; set; }
        public int ItemId { get; set; }
        public int RoleId { get; set; }
        public string ItemAlias { get; set; }
        public string MenuName { get; set; }
        public string MenuURL { get; set; }
        public string ItemName { get; set; }
        public string ItemUrl { get; set; }
        public string ItemDesc { get; set; }
        public bool ItemStatus { get; set; }
        public bool MenuStatus { get; set; }
        public string MenuDesc { get; set; }
        public string ExternalURL { get; set; }
        public string ItemIcon { get; set; }
    }
}
