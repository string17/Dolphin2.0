using System;
using System.Collections.Generic;
using System.Text;
using System.Web;

namespace DAL.CustomObjects
{
    public class UserObj
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string UserActivity { get; set; }
        public string Comment { get; set; }
        public DateTime EventDate { get; set; }
        public string Computername { get; set; }
        public string SystemIp { get; set; }
    }

    public class UserInfo
    {
        public string RespCode { get; set; }
        public string RespMessage { get; set; }
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public string UserImg { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDesc { get; set; }
        public string Title { get; set; }
        public string ClientAlias { get; set; }
        public int? ClientId { get; set; }
        public bool IsClientActive { get; set; }
        public bool? IsUserActive { get; set; }
        public bool IsDelete { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string SystemIp { get; set; }
        public string Computername { get; set; }
    }

    //public class UserInfo
    //{
    //    public string RespCode { get; set; }
    //    public string RespMessage { get; set; }
    //    public int UserId { get; set; }
    //    public string FirstName { get; set; }
    //    public string MiddleName { get; set; }
    //    public string LastName { get; set; }
    //    public string UserName { get; set; }
    //    public string Email { get; set; }
    //    public string Password { get; set; }
    //    public string PhoneNo { get; set; }
    //    public string UserImg { get; set; }
    //    public int? RoleId { get; set; }
    //    public string RoleName { get; set; }
    //    public string ClientAlias { get; set; }
    //    public string Alias { get; set; }
    //    public int? ClientId { get; set; }
    //    public bool IsClientActive { get; set; }
    //    public bool? IsUserActive { get; set; }
    //    public bool IsDelete { get; set; }
    //    public string CreatedBy { get; set; }
    //    public DateTime? CreatedOn { get; set; }
    //    public string ModifiedBy { get; set; }
    //    public DateTime ModifiedOn { get; set; }
    //    public string Computername { get; set; }
    //    public string SystemIp { get; set; }
    //}


    public class UserDetails
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public HttpPostedFileBase UserImg { get; set; }
        public HttpPostedFileBase UserFile { get; set; }
        public string UserImg1 { get; set; }
        public int? RoleId { get; set; }
        public string RoleName { get; set; }
        public string UserStatus { get; set; }
        public string ClientName { get; set; }
        public int? ClientId { get; set; }
        public bool IsDelete { get; set; }
        public bool IsUserActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }
        public string SystemIp { get; set; }
        public string Computername { get; set; }
    }
}
