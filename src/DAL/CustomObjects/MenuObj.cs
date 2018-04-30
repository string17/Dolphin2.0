using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CustomObjects
{
    public class MenuObj
    {
       public string Username { get; set; }
    }

    public class UserMenuResp
    {
        public string RespCode { get; set; }
        public string RespMessage { get; set; }
     }

    public class UserMenuObj
    {
        public int MenuId { get; set; }
        public int ItemId { get; set; }
        public int RoleId { get; set; }
        public string MenuURL { get; set; }
        public string ItemName { get; set; }
        public int Sequence { get; set; }
        public string ItemUrl { get; set; }
        public string ItemDesc { get; set; }
        public bool ItemStatus { get; set; }
        public bool MenuStatus { get; set; }
        public string ItemAlias { get; set; }
        public string ExternalURL { get; set; }
        public string ItemIcon { get; set; }
    }
}
