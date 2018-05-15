using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CustomObjects
{
    public class RoleObj
    {
        public int RoleId { get; set; }
        public string Title { get; set; }
        public string _Desc { get; set; }
        public bool IsRoleActive { get; set; }
        public string UserName { get; set; }
        public string Computername { get; set; }
        public string SystemIp { get; set; }
    }



    public class RoleResp
    {
        public string RespCode { get; set; }
        public string RespMessage { get; set; }
        public int RoleId { get; set; }
        public string Title { get; set; }
        public string _Desc { get; set; }
        public bool IsRoleActive { get; set; }
        public string UserName { get; set; }
        public string Computername { get; set; }
        public string SystemIp { get; set; }
    }

    public class RoleMenuObj
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int RoleId { get; set; }
        public string MenuDesc { get; set; }
        public int MenuId { get; set; }
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
        public string RespCode { get; set; }
        public string RespMessage { get; set; }
        public string UserName { get; set; }
        public string Computername { get; set; }
        public string SystemIp { get; set; }
    }
}
