using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Response
{
    public class UserDetailsResponse
    {
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public List<UserDetailsObj> UserDetails { get; set; }
    }


    public class UserDetailsObj
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNo { get; set; }
        public string UserImg { get; set; }
        public string Sex { get; set; }
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
}
