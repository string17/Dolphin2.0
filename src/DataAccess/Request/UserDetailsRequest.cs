using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DataAccess.Request
{
    public class UserDetailsRequest
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Sex { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string PhoneNo { get; set; }
        public string UserImg { get; set; }
        public HttpPostedFileBase ImgFile { get; set; }
        public string UserImg1 { get; set; }
        public int? RoleId { get; set; }
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
