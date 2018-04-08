using DAL.CustomObjects;
using DAL.Interfaces;
using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace BLL.ApplicationLogic
{
    public class UserManagement : IUser
    {
        private readonly DolphinDb _db = DolphinDb.GetInstance();
        public LoginResponse ValidateUser(UserObj userLogin)
        {
            var _password = EncryptPassword(userLogin.Password);
            string sql = "select A.*,B.* from DolUser A inner join DolRole B on A.RoleId=B.RoleId where A.UserName=@0 and A.UserPWD=@1";
            var _actual = _db.FirstOrDefault<LoginResponse>(sql, userLogin.Username, _password);
            return _actual;

        }




        private string EncryptPassword(string UserPWD)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(UserPWD);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    UserPWD = Convert.ToBase64String(ms.ToArray());
                }
            }
            return UserPWD;
        }

        private string DecryptPassword(string UserPWD)
        {
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(UserPWD);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    UserPWD = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return UserPWD;
        }


        //public DolUser getUserByUsername(string Username)
        //{
        //    string sql = "Select * from DolUser where UserName =@0";
        //    var actual = _db.FirstOrDefault<DolUser>(sql, Username.ToUpper());
        //    return actual;
        //}

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

        public bool UpdatePassword(string Password, int? Id)
        {
            try
            {
                var _user = _db.SingleOrDefault<DolUser>("where UserId =@0", Id);
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

        public LoginResponse GetUserById(int UserId)
        {
            var _actual = _db.SingleOrDefault<DolUser>("where UserId=@0", UserId);
            var _company = _db.SingleOrDefault<DolCompany>("where CompanyId=@0", _actual.Companyid);
            var _role = _db.SingleOrDefault<DolRole>("Where RoleId=@0", _actual.Roleid);
            LoginResponse _userObj = new LoginResponse()
            {
                CompanyId= _actual.Companyid,
                FirstName = _actual.Firstname,
                MiddleName = _actual.Middlename,
                LastName = _actual.Lastname,
                UserName = _actual.Username,
                Password = _actual.Password,
                RoleId = _actual.Roleid,
                PhoneNo = _actual.Phoneno,
                Alias = _company.Alias,
                RoleName=_role.Title,
                UserImg = _actual.Userimg

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

        public List<DolUser> GetEngineer()
        {
            string sql = "select A.*,B.RoleName,B.RoleStatus from DolUser A INNER JOIN DolRole B ON A.RoleId=B.RoleId where A.UserStatus='true' AND B.RoleName='ENGINEER' ";
            var actual = _db.Fetch<DolUser>(sql);
            return actual;
        }
        public List<DolUser> getAllUsers()
        {
            var actual = _db.Fetch<DolUser>();
            return actual;
        }

        public List<DolUser> GetUserById()
        {
            var actual = _db.Fetch<DolUser>();
            return actual;
        }

        public List<DolMenu> GetMenuByUsername(string Username)
        {
            try
            {
                string SQL = "select A.* from DolMenu A inner join DolRole_Menu B on A.MenuId = B.MenuId inner join DolUser c on c.RoleId = B.RoleId where c.UserName =@0";
                var actual = _db.Fetch<DolMenu>(SQL, Username);
                return actual;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public bool DoesUsernameExists(string Username)
        {
            var rslt = _db.Fetch<DolUser>().Where(a => a.Username == Username);
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
            string _password = EncryptPassword(Password);
            var rslt = _db.FirstOrDefault<DolUser>("where UserName=@0 and Password=@1", Username, _password);
            if (rslt.Password == Password)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool InsertUser(DolUser Username)
        {
            try
            {
                _db.Insert(Username);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

 

        public int GetFreshUser(string Username)
        {
            string sql = "Select COUNT(*) from AuditTrail where UserName = @0";
            var _actual = _db.ExecuteScalar<int>(sql, Username);
            return Convert.ToInt32(_actual);
        }

        public DolUser GetUserById(decimal UserId)
        {
            var actual = _db.SingleById<DolUser>(UserId);
            return actual;
        }



        public bool UpdateUser(string FirstName, string MiddleName, string LastName, string UserName, string Password, string UserImg, string PhoneNo, int? RoleId, bool? Status, string ModifiedBy, DateTime ModifiedOn, int? UserId)
        {
            try
            {
                var _users = _db.SingleOrDefault<DolUser>("WHERE UserId=@0", UserId);
                _users.Firstname = FirstName;
                _users.Middlename = MiddleName;
                _users.Lastname = LastName;
                _users.Username = UserName;
                _users.Password = Password;
                _users.Userimg = UserImg;
                _users.Phoneno = PhoneNo;
                _users.Roleid = RoleId;
                _users.Status = Status;
                _users.Modifiedby = ModifiedBy;
                _users.Modifiedon = ModifiedOn;
                _db.Update(_users);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public bool UpdateProfile(string FirstName, string MiddleName, string LastName, string UserName, string Password, string UserImg, string PhoneNo, string ModifiedBy, DateTime ModifiedOn, string Username)
        {
            try
            {
                var _user = _db.SingleOrDefault<DolUser>("WHERE UserName=@0", Username);
                _user.Firstname = FirstName;
                _user.Middlename = MiddleName;
                _user.Lastname = LastName;
                _user.Userimg = UserImg;
                _user.Username = UserName;
                _user.Password = Password;
                _user.Phoneno = PhoneNo;
                //users.RoleId = RoleId;
                //users.UserStatus = UserStatus;
                _user.Modifiedby = ModifiedBy;
                _user.Modifiedon = ModifiedOn;
                _db.Update(_user);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public string DoFileUpload(HttpPostedFileBase pic, string filename = "")
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


        public List<DolMenu> GetMenuByRoleId(decimal RoleId)
        {
            string SQL = "select DolMenu.MenuId,DolMenu.MenuName,DolMenu.MenuURL,DolMenu.LinkIcon,DolMenu.ExternalUrl, DolRole_Menu.MenuId,DolMenu.ParentId,RoleId from DolMenu INNER JOIN DolRole_Menu ON DolMenu.MenuId=DolRole_Menu.MenuId WHERE DolRole_Menu.RoleId=" + RoleId;
            var actual = _db.Fetch<DolMenu>(SQL);
            return (actual);
        }


        public List<DolMenu> GetMenuByMenuId()
        {
            var actual = _db.Fetch<DolMenu>();
            return actual;
        }

        public DolMenu GetMenuByName(string MenuName)
        {
            string SQL = "Select * from DolMenu where MenuName =@0";
            var actual = _db.FirstOrDefault<DolMenu>(SQL, MenuName);
            return actual;
        }

        public List<DolMenu> getMenuByUsername(string Username)
        {
            try
            {
                string SQL = "select A.* from DolMenu A inner join DolRole_Menu B on A.MenuId = B.MenuId inner join DolUser c on c.RoleId = B.RoleId where c.UserName =@0";
                var actual = _db.Fetch<DolMenu>(SQL, Username);
                return actual;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public List<DolMenu> getMenuById()
        {
            var actual = _db.Fetch<DolMenu>();
            return actual;
        }

        public bool InsertMenu(DolMenu MenuName)
        {
            try
            {
                _db.Insert(MenuName);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }


    }
}
