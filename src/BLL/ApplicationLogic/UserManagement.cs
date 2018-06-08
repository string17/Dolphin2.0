using DAL.CustomObjects;
using DolphinContext.Data.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;

namespace BLL.ApplicationLogic
{
    public class UserManagement
    {
        private readonly DolphinDb _db = DolphinDb.GetInstance();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
      

        public UserInfo GetUserInfo(UserObj userLogin)
        {
            string sql = "select A.*,B.*,C.ClientAlias from Dol_User A inner join User_Role B on A.RoleId=B.RoleId inner join Dol_Client C on C.ClientId=A.ClientId where A.UserName=@0 and A.IsDelete='false' ";
            var _actual = _db.FirstOrDefault<UserInfo>(sql, userLogin.Username);
            return _actual;

        }

       

        public DolUser GetUserByUsername(string Username, int? CompanyId)
        {
            string SQL = "Select * from DolUser where UserName =@0 AND CompanyId=@1";
            var actual = _db.FirstOrDefault<DolUser>(SQL, Username.ToUpper(), CompanyId);
            return actual;
        }

        public List<UserObj> GetUserByCompany()
        {
            string sql = "SELECT A.FirstName,A.MiddleName,A.LastName,A.UserName,A.PhoneNos,A.UserStatus,A.UserImg,A.RoleId,A.UserId,B.CustomerAlias FROM DolUser A INNER JOIN DolCompany B ON A.CompanyId = B.CompanyId ";
            var actual = _db.Fetch<UserObj>(sql);
            return actual;
        }

        public bool UpdatePassword(string Username, string Password)
        {
            try
            {
                var _user = _db.SingleOrDefault<DolUser>("where UserName =@0", Username);
                _user.Password = Password;
                _db.Update(_user);
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public DolUser ModifyPassword(int Id)
        {
            string sql = "Select * from DolUser where UserId =@0";
            var actual = _db.FirstOrDefault<DolUser>(sql, Id);
            return actual;

        }

        //public UserObj GetUserProfileByUsername(string Username)
        //{
        //    var actual = _db.SingleOrDefault<DolUser>("where UserName=@0", Username);
        //    var userCompany = _db.SingleOrDefault<DolCompany>("where CustomerId=@0", actual.Customerid);
        //    var userrRole = _db.SingleOrDefault<DolRole>("where RoleId=@0", actual.Roleid);
        //    Company company = new Company();
        //    UserObj userView = new UserObj()
        //    {
        //        CustomerId = actual.CustomerId,
        //        FirstName = actual.FirstName,
        //        MiddleName = actual.MiddleName,
        //        LastName = actual.LastName,
        //        UserName = actual.UserName,
        //        UserPWD = actual.UserPWD,
        //        RoleId = actual.RoleId,
        //        PhoneNos = actual.PhoneNos,
        //        CustomerAlias = userCompany.CustomerAlias,
        //        UserImg = actual.UserImg,
        //        RoleName = userrRole.RoleName,
        //        UserStatus = actual.UserStatus,
        //        CreatedBy = actual.CreatedBy,
        //        CreatedOn = actual.CreatedOn

        //    };

        //    return userView;
        //}

        public UserInfo GetUserDetails(int UserId)
        {
            var _actual = _db.SingleOrDefault<DolUser>("where UserId=@0", UserId);
            var _company = _db.SingleOrDefault<DolClient>("where ClientId=@0", _actual.Clientid);
            var _role = _db.SingleOrDefault<UserRole>("Where RoleId=@0", _actual.Roleid);
            UserInfo _userObj = new UserInfo()
            {
                ClientId= _actual.Clientid,
                FirstName = _actual.Firstname,
                MiddleName = _actual.Middlename,
                LastName = _actual.Lastname,
                UserName = _actual.Username,
                Email=_actual.Email,
                Password = _actual.Password,
                RoleId = _actual.Roleid,
                PhoneNo = _actual.Phoneno,
                ClientAlias = _company.Clientalias,
                RoleName=_role.Rolename,
                UserImg = _actual.Userimg,
                Sex=_actual.Sex,
                IsUserActive=_actual.Isuseractive,
                CreatedBy=_actual.Createdby,
                CreatedOn=_actual.Createdon,
                RespCode="00",
                RespMessage="Success"

            };

            return _userObj;
        }

        //public EngineerViewModel getEngineerTerminal(string roleName)
        //{
        //    var term = _db.SingleOrDefault<DolRole>("where RoleName=@0", roleName);
        //    var actual = _db.SingleOrDefault<DolUser>("Where RoleId=@0", term.RoleId);
        //    var termNum = _db.ExecuteScalar<int>("select Count(*) from DolTerminal where UserId =@0", actual.UserId);
        //    EngineerViewModel engineerView = new EngineerViewModel()
        //    {
        //        FirstName = actual.FirstName,
        //        MiddleName = actual.MiddleName,
        //        LastName = actual.LastName,
        //        UserName = actual.UserName,
        //        UserPWD = actual.UserPWD,
        //        RoleId = actual.RoleId,
        //        PhoneNos = actual.PhoneNos,
        //        RoleName = term.RoleName,
        //        //UserImg = actual.UserImg

        //    };
        //    return engineerView;
        //}

        public List<UserInfo> GetAllEngineers()
        {
            string sql = "select A.*,B.RoleName,B.IsRoleActive from Dol_User A INNER JOIN User_Role B ON A.RoleId=B.RoleId where A.IsUserActive='true' AND B.RoleName='ENGINEER' ";
            var actual = _db.Fetch<UserInfo>(sql);
            return actual;
        }
        public List<UserInfo> AllUsers()
        {
            string sql = "select A.*, B.*,C.* from Dol_User A inner join Dol_Client B on A.ClientId=B.ClientId inner join User_Role C on A.RoleId=C.RoleId";
            var actual = _db.Fetch<UserInfo>(sql).ToList();
            return actual;
        }

        public List<DolUser> GetUserById()
        {
            var actual = _db.Fetch<DolUser>();
            return actual;
        }

        public List<DolMenuItem> GetMenuByUsername(string Username)
        {
            try
            {
                string SQL = "select A.* from DolMenu A inner join DolRole_Menu B on A.MenuId = B.MenuId inner join DolUser c on c.RoleId = B.RoleId where c.UserName =@0";
                var actual = _db.Fetch<DolMenuItem>(SQL, Username);
                return actual;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool DoesUsernameExists(string Username)
        {
            var rslt = _db.SingleOrDefault<DolUser>("where UserName=@0", Username); 
            if (rslt == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public bool DoesPasswordExists(string Username, string Password)
        {
            string _password = new EncryptionManager().EncryptValue(Password);
            var rslt = _db.FirstOrDefault<DolUser>("where UserName=@0 and Password=@1", Username, _password);
            if (rslt != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsUserActive(string Username, string Password)
        {
            string _password =new EncryptionManager().EncryptValue(Password);
            var rslt = _db.FirstOrDefault<DolUser>("where UserName=@0 and Password=@1" , Username, _password);
            if (rslt.Isuseractive==true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsCompanyActive(string username)
        {
            string sql = "select A.*,B.* from Dol_User A inner join Dol_Client B on B.ClientId=A.ClientId inner join User_Role C on C.RoleId=A.RoleId where A.UserName=@0";
            var _actual = _db.SingleOrDefault<UserInfo>(sql, username);
            if (_actual.IsClientActive)
            {
                return true;
            }
            else
            {
                return false;
            }
           
        }

        public bool DoesEmailExists(string Email)
        {
            var rslt = _db.SingleOrDefault<DolUser>("where Email=@0", Email); 
            if (rslt == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool InsertUser(UserInfo userDetails)
        {
            try
            {
                //return true;
                var user = new DolUser();
                user.Firstname = userDetails.FirstName;
                user.Middlename = userDetails.MiddleName;
                user.Lastname = userDetails.LastName;
                user.Username = userDetails.UserName;
                user.Email = userDetails.Email;
                user.Password = userDetails.Password;
                user.Phoneno = userDetails.PhoneNo;
                user.Sex = userDetails.Sex;
                user.Roleid = userDetails.RoleId;
                user.Clientid = userDetails.ClientId;
                user.Userimg = userDetails.UserImg;
                user.Isuseractive = userDetails.IsUserActive;
                user.Isdelete = userDetails.IsDelete;
                user.Createdby = userDetails.CreatedBy;
                user.Createdon = userDetails.CreatedOn;
                _db.Insert(user);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

 

        public int GetFreshUser(string Username)
        {
            string sql = "Select COUNT(*) from Audit_Trail where UserName = @0";
            var _actual = _db.ExecuteScalar<int>(sql, Username);
            return Convert.ToInt32(_actual);
        }

        public DolUser GetUserById(decimal UserId)
        {
            var actual = _db.SingleById<DolUser>(UserId);
            return actual;
        }



        public bool ModifyUserDetails(UserInfo param)
        {
            try
            {
                var users = _db.SingleOrDefault<DolUser>("WHERE UserId=@0", param.UserId);
                users.Firstname = param.FirstName;
                users.Middlename = param.MiddleName;
                users.Lastname = param.LastName;
                users.Username = param.UserName;
                users.Password = param.Password; //new EncryptionManager().EncryptValue(param.Password);
                users.Userimg = param.UserImg;
                users.Sex = param.Sex;
                users.Phoneno = param.PhoneNo;
                users.Roleid = param.RoleId;
                users.Isuseractive = param.IsUserActive;
                users.Modifiedby = param.ModifiedBy;
               // users.Modifiedon = param.ModifiedOn;
                _db.Update(users);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool UpdateProfile(UserDetails userDetails)
        {
            try
            {
                var _user = _db.SingleOrDefault<DolUser>("WHERE UserName=@0", userDetails.UserName);
                _user.Firstname = userDetails.FirstName;
                _user.Middlename = userDetails.MiddleName;
                _user.Lastname = userDetails.LastName;
                _user.Userimg = DoFileUpload(userDetails.UserImg);
                _user.Username = userDetails.UserName;
                _user.Password = new EncryptionManager().EncryptValue(userDetails.Password);
                _user.Phoneno = userDetails.PhoneNo;
                _user.Modifiedby = userDetails.ModifiedBy;
                _user.Modifiedon = userDetails.ModifiedOn;
                _db.Update(_user);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public AppResp PasswordNotification(UserObj param)
        {
            bool email = DoesEmailExists(param.Email);
            if (email)
            {
                EmailObj emailModel = new EmailObj();
                string AuthUsername = WebConfigurationManager.AppSettings["AuthUsername"];
                string AuthPWD = WebConfigurationManager.AppSettings["AuthPWD"];
                string PasswordUrl = WebConfigurationManager.AppSettings["PasswordUrl"];
                var body = "Kindly click on this link  to reset your password. </br>" + PasswordUrl + "?Email=" + param.Email;
                var message = new MailMessage();
                message.To.Add(new MailAddress(param.Email));  // replace with valid value 
                message.From = new MailAddress(WebConfigurationManager.AppSettings["SupportAddress"]);  // replace with valid value
                message.Subject = "Password Update";
                message.Body = string.Format(body, emailModel.FromEmail, emailModel.Message);
                message.IsBodyHtml = true;

                using (SmtpClient smtp = new SmtpClient())
                {
                    try
                    {
                        smtp.Host = WebConfigurationManager.AppSettings["EmailHost"];
                        smtp.EnableSsl = true;
                        NetworkCredential NetworkCred = new NetworkCredential(AuthUsername, AuthPWD);
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = NetworkCred;
                        smtp.Port = Convert.ToInt32(WebConfigurationManager.AppSettings["EmailPort"]);
                        smtp.Send(message);
                        return new AppResp
                        {
                            RespCode = "00",
                            RespMessage ="kindly check your email"
                        };
                    }
                    catch (Exception ex)
                    {
                        Log.InfoFormat("Email", ex.Message);
                        return new AppResp
                        {
                            RespCode = "01",
                            RespMessage = ex.Message
                        };
                    }
                }
            }
            else
            {
                return new AppResp
                {

                    RespCode="01",
                    RespMessage="Invalid email address"

                };
            }
            
        }

        public bool GetUserStatus(string RoleName)
        {
            switch (RoleName)
            {
                case "ACTIVE":
                    return true;
                case "INACTIVE":
                    return false;
                default:
                    return false;
            }
        }

        //public bool UpdatePassword(string Password, int? Id)
        //{
        //    try
        //    {
        //        var users = _db.SingleOrDefault<PureUser>("where UserId =@0", Id);
        //        users.Userpwd = Password;
        //        _db.Update(users);
        //        return true;
        //    }
        //    catch (Exception)
        //    {

        //        return false;
        //    }
        //}


        private string DoFileUpload(HttpPostedFileBase pic, string filename = "")
        {
            if (pic == null && string.IsNullOrWhiteSpace(filename))
            {
                return "";
            }
            if (!string.IsNullOrWhiteSpace(filename) && pic == null) return filename;

            string result = DateTime.Now.Millisecond + "UserPics.jpg";
            pic.SaveAs(HttpContext.Current.Server.MapPath("~/Content/UserImg/") + result);
            return result;
        }
    }
}
