using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace DAL.CustomObjects
{
    public class UserObj
    {

        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class UserDetails
    {
        public string RespCode { get; set; }
        public string RespMessage { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public HttpPostedFileBase UserImg { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public string CompanyName { get; set; }
        public string Alias { get; set; }
        public int? CompanyId { get; set; }
        public bool? Status { get; set; }
        public bool IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
    }

    public class Menus
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
        public string MenuURL { get; set; }
        public int ParentId { get; set; }
        public string MenuDesc { get; set; }
        public string ExternalURL { get; set; }
        public string LinkIcon { get; set; }
    }

    public class LoginResponse
    {
        public string RespCode { get; set; }
        public string RespMessage { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public string UserImg { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public string CompanyName { get; set; }
        public string Alias { get; set; }
        public int? CompanyId { get; set; }
        public bool? Status { get; set; }
        public bool IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public List<Menus> Menu { get; set; }
    }
}
