using DataAccess.CustomObjects;
using DataAccess.Request;
using DataAccess.Response;
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

namespace BusinessLogic
{
    public class UserManagement
    {
        private readonly DolphinDb _db = DolphinDb.GetInstance();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        //private readonly AuditManagement _audit = new AuditManagement();
        //private readonly RoleManagement _role = new RoleManagement();
        //private readonly ClientManagement _client = new ClientManagement();




        public UserDetailsObj GetUserInfo(string UserName)
        {
            string sql = "select A.*,B.*,C.ClientAlias from Dol_User A inner join User_Role B on A.RoleId=B.RoleId inner join Dol_Client C on C.ClientId=A.ClientId where A.UserName=@0 and A.IsDelete='false' ";
            var info = _db.FirstOrDefault<UserDetailsObj>(sql, UserName);
            return info;

        }


        public UserDetailsResponse GetUserInfoByUsername(string UserName)
        {
            try
            {
                var result = GetUserInfo(UserName);
                if (result == null)
                {
                    return new UserDetailsResponse
                    {
                        ResponseCode = "01",
                        ResponseMessage = "No details found",
                        UserDetails = new List<UserDetailsObj>()
                    };
                }
                else
                {
                    var success = new List<UserDetailsObj>
                    {
                        result
                    };
                    return new UserDetailsResponse
                    {
                        ResponseCode = "00",
                        ResponseMessage = "Users details",
                        UserDetails = success
                    };
                }
            }
            catch(Exception ex)
            {
                return new UserDetailsResponse
                {
                    ResponseCode = "XX",
                    ResponseMessage = "Application Error",
                    UserDetails = new List<UserDetailsObj>()
                };
            }
        }

        //public DolUser GetUserByUsername(string Username, int? CompanyId)
        //{
        //    string SQL = "Select * from DolUser where UserName =@0 AND CompanyId=@1";
        //    var actual = _db.FirstOrDefault<DolUser>(SQL, Username.ToUpper(), CompanyId);
        //    return actual;
        //}


        //public UserDetailsObj GetDetailsByUsername(string Username)
        //{
        //    var actual = _db.FirstOrDefault<UserDetailsObj>("Select * from DolUser where UserName =@0 ", Username.ToUpper());
        //    return actual;
        //}

        //public DolUser GetUserByEmail(string Email)
        //{
        //    var actual = _db.FirstOrDefault<DolUser>("Select * from Dol_User where Email =@0", Email.ToUpper());
        //    return actual;
        //}

        //public List<UserDetailsResponse> GetUserByCompany()
        //{
        //    string sql = "SELECT A.FirstName,A.MiddleName,A.LastName,A.UserName,A.PhoneNos,A.UserStatus,A.UserImg,A.RoleId,A.UserId,B.CustomerAlias FROM DolUser A INNER JOIN DolCompany B ON A.CompanyId = B.CompanyId ";
        //    var actual = _db.Fetch<UserDetailsResponse>(sql);
        //    return actual;
        //}

        //public bool UpdatePassword(string Username, string Password)
        //{
        //    try
        //    {
        //        var _user = _db.SingleOrDefault<DolUser>("where UserName =@0", Username);
        //        _user.Password = Password;
        //        _db.Update(_user);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.ErrorFormat("UpdatePassword", ex);
        //        return false;
        //    }
        //}

        //public DolUser ModifyPassword(int Id)
        //{
        //    string sql = "Select * from DolUser where UserId =@0";
        //    var actual = _db.FirstOrDefault<DolUser>(sql, Id);
        //    return actual;

        //}


        //public UserDetailsObj GetUserDetails(int UserId)
        //{
        //    var _actual = _db.SingleOrDefault<DolUser>("where UserId=@0", UserId);
        //    var _company = _db.SingleOrDefault<DolClient>("where ClientId=@0", _actual.Clientid);
        //    var _role = _db.SingleOrDefault<UserRole>("Where RoleId=@0", _actual.Roleid);
        //    var param = new UserDetailsObj
        //    {
        //        ClientId = _actual.Clientid,
        //        FirstName = _actual.Firstname,
        //        MiddleName = _actual.Middlename,
        //        LastName = _actual.Lastname,
        //        UserName = _actual.Username,
        //        Email = _actual.Email,
        //        Password = _actual.Password,
        //        RoleId = _actual.Roleid,
        //        PhoneNo = _actual.Phoneno,
        //        ClientAlias = _company.Clientalias,
        //        RoleName = _role.Rolename,
        //        RoleDesc = _role.Roledesc,
        //        UserImg = _actual.Userimg,
        //        Sex = _actual.Sex,
        //        IsUserActive = _actual.Isuseractive,
        //        CreatedBy = _actual.Createdby,
        //        CreatedOn = _actual.Createdon
        //    };

        //    var result = new List<UserDetailsObj>
        //    {
        //        param
        //    };

        //    return param;
        //}


        //public List<UserDetailsObj> GetAllEngineers()
        //{
        //    string sql = "select A.*,B.RoleName,B.IsRoleActive from Dol_User A INNER JOIN User_Role B ON A.RoleId=B.RoleId where A.IsUserActive='true' AND (B.RoleName='ENGINEER1' OR B.RoleName='ENGINEER2');";
        //    var actual = _db.Fetch<UserDetailsObj>(sql);
        //    return actual;
        //}


        public List<UserDetailsObj> AllUsers()
        {
            string sql = "select A.*, B.*,C.* from Dol_User A inner join Dol_Client B on A.ClientId=B.ClientId inner join User_Role C on A.RoleId=C.RoleId";
            var actual = _db.Fetch<UserDetailsObj>(sql).ToList();
            return actual;
        }

        //public List<DolUser> GetUserById()
        //{
        //    var actual = _db.Fetch<DolUser>();
        //    return actual;
        //}

        //public List<DolMenuItem> GetMenuByUsername(string Username)
        //{
        //    string SQL = "select A.* from DolMenu A inner join DolRole_Menu B on A.MenuId = B.MenuId inner join DolUser c on c.RoleId = B.RoleId where c.UserName =@0";
        //    var actual = _db.Fetch<DolMenuItem>(SQL, Username);
        //    return actual;
        //}

        //public UserDetailsResponse GetUserInfo(LoginRequest param)
        //{
        //    try
        //    {
        //        if (param.UserName != null)
        //        {
        //            var success = GetUserInfo(param.UserName);
        //            if (success != null)
        //            {
        //                var result = new List<UserDetailsObj> { success };
        //                Log.ErrorFormat("User details", param.UserName, param.Password, new UserDetailsResponse { ResponseCode = "00", ResponseMessage = "Successful" });
        //                _audit.InsertAudit(param.UserName, Constants.ActionType.GetUserInfo.ToString(), "User details", DateTime.Now, param.Computername, param.SystemIp);
        //                return new UserDetailsResponse { ResponseCode = "00", ResponseMessage = "Successful", UserDetails = result };
        //            }
        //            else
        //            {
        //                Log.ErrorFormat("User details", new UserDetailsResponse { ResponseCode = "01", ResponseMessage = "Unsuccessful" });
        //                return new UserDetailsResponse { ResponseCode = "01", ResponseMessage = "Unsuccessful", UserDetails = new List<UserDetailsObj>() };

        //            }
        //        }
        //        else
        //        {
        //            Log.ErrorFormat("User details", new UserDetailsResponse { ResponseCode = "02", ResponseMessage = "Kindly supply Username" });
        //            return new UserDetailsResponse { ResponseCode = "02", ResponseMessage = "Kindly supply Username", UserDetails = new List<UserDetailsObj>() };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.ErrorFormat("GetUserInfo", ex.ToString());
        //        return new UserDetailsResponse() { ResponseCode = "XX", ResponseMessage = "System error", UserDetails = new List<UserDetailsObj>() };
        //    }
        //}

        public bool DoesUsernameExists(string Username)
        {
            var _db= DolphinDb.GetInstance();
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


        //public LoginResponse ResetPassword(LoginRequest param)
        //{
        //    try
        //    {
        //        if (param.UserName != null && param.Password != null)
        //        {
        //            bool checkPassword = DoesPasswordExists(param.UserName, param.Password);
        //            if (!checkPassword)
        //            {
        //                bool success = UpdatePassword(param.UserName, param.Password);
        //                if (success)
        //                {
        //                    Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ResetPassword.ToString());
        //                    _audit.InsertAudit(param.UserName, Constants.ActionType.ResetPassword.ToString(), "Password changed", DateTime.Now, param.Computername, param.SystemIp);
        //                    return new LoginResponse
        //                    {
        //                        ResponseCode = "00",
        //                        ResponseMessage = "Password Updated successfully"
        //                    };
        //                }
        //                else
        //                {
        //                    Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ResetPassword.ToString(), "Failed");
        //                    return new LoginResponse
        //                    {
        //                        ResponseCode = "03",
        //                        ResponseMessage = "Unable to Update password"
        //                    };
        //                }
        //            }
        //            else
        //            {
        //                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ResetPassword.ToString(), "Password repetition");
        //                return new LoginResponse
        //                {
        //                    ResponseCode = "04",
        //                    ResponseMessage = "Password must not be the same with old password"
        //                };
        //            }
        //        }
        //        else
        //        {
        //            Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ResetPassword.ToString(), "Empty fields");
        //            return new LoginResponse
        //            {
        //                ResponseCode = "05",
        //                ResponseMessage = "Kindly supply the missing value"
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.ErrorFormat("ResetPassowrd", ex.ToString());
        //        return new LoginResponse() { ResponseCode = "XX", ResponseMessage = "System error" };
        //    }
        //}


        public LoginResponse ValidateUser(LoginRequest param)
        {
            try
            {
                if (param.UserName == null && param.Password == null)
                {
                    Log.ErrorFormat("Login ", new LoginResponse { ResponseCode = "01", ResponseMessage = "Null parameter" });
                    return new LoginResponse { ResponseCode = "01", ResponseMessage = "Kindly supply the missing parameter" };
                }
                var _userName = DoesUsernameExists(param.UserName);
                if (!_userName)
                {
                    Log.ErrorFormat("UserName: ", param.UserName, new LoginResponse { ResponseCode = "02", ResponseMessage = "Invalid UserName" });
                    return new LoginResponse { ResponseCode = "02", ResponseMessage = "Invalid UserName" };
                }
                var _password = DoesPasswordExists(param.UserName, param.Password);
                if (!_password)
                {
                    Log.ErrorFormat("Password: ", param.UserName, param.Password, new LoginResponse { ResponseCode = "03", ResponseMessage = "Invalid Password" });
                    return new LoginResponse { ResponseCode = "03", ResponseMessage = "Invalid Password" };
                }

                bool _validUser = IsUserActive(param.UserName, param.Password);
                if (!_validUser)
                {
                    Log.ErrorFormat("Password: ", param.UserName, param.Password, new LoginResponse { ResponseCode = "04", ResponseMessage = "Inactive account" });
                    return new LoginResponse { ResponseCode = "04", ResponseMessage = "Inactive account" };
                }

                bool isCompanyActive = IsCompanyActive(param.UserName);
                if (!isCompanyActive)
                {
                    Log.ErrorFormat("Password: ", param.UserName, param.Password, new LoginResponse { ResponseCode = "05", ResponseMessage = "Company's account is not active" });
                    return new LoginResponse { ResponseCode = "05", ResponseMessage = "Company's account is not active" };
                }
                var existingAccount = GetFreshUser(param.UserName);
                if (existingAccount != 0)
                {
                    Log.ErrorFormat("Valid credentials", param.UserName, param.Password, new LoginResponse { ResponseCode = "00", ResponseMessage = "Successful" });
                    //_audit.InsertAudit(param.UserName, Constants.ActionType.Login.ToString(), "Valid Credentials", DateTime.Now, param.Computername, param.SystemIp);
                   //_audit.InsertSessionTracker(param.UserName, param.SystemIp, param.Computername);
                    return new LoginResponse { ResponseCode = "00", ResponseMessage = "Successful" };
                }
                else
                {
                    Log.ErrorFormat("Valid credentials", param.UserName, param.Password, new LoginResponse { ResponseCode = "01", ResponseMessage = "Successful" });
                    return new LoginResponse { ResponseCode = "06", ResponseMessage = "Successful" };
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("Login ", new LoginResponse { ResponseCode = "07", ResponseMessage = ex.ToString() });
                return new LoginResponse { ResponseCode = "07", ResponseMessage = "Kindly supply the missing parameter" };
            }
        }




        public bool DoesPasswordExists(string Username, string Password)
        {
            var _db = DolphinDb.GetInstance();
            string password = new EncryptionManager().EncryptValue(Password);
            var rslt = _db.FirstOrDefault<DolUser>("where UserName=@0 and Password=@1", Username, password);
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
            var _db = DolphinDb.GetInstance();
            string _password = new EncryptionManager().EncryptValue(Password);
            var rslt = _db.FirstOrDefault<DolUser>("where UserName=@0 and Password=@1", Username, _password);
            if (rslt.Isuseractive == true)
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
            var _db = DolphinDb.GetInstance();
            string sql = "select A.*,B.* from Dol_User A inner join Dol_Client B on B.ClientId=A.ClientId inner join User_Role C on C.RoleId=A.RoleId where A.UserName=@0";
            var result = _db.SingleOrDefault<UserDetailsObj>(sql, username);
            if (result.IsClientActive)
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
            var _db = DolphinDb.GetInstance();
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

        //public bool InsertUser(UserDetailsRequest param)
        //{
        //    try
        //    {
        //        //return true;
        //        var user = new DolUser();
        //        user.Firstname = param.FirstName;
        //        user.Middlename = param.MiddleName;
        //        user.Lastname = param.LastName;
        //        user.Username = param.UserName;
        //        user.Email = param.Email;
        //        user.Password = param.Password;
        //        user.Phoneno = param.PhoneNo;
        //        user.Sex = param.Sex;
        //        user.Roleid = param.RoleId;
        //        user.Clientid = param.ClientId;
        //        user.Userimg = param.UserImg;
        //        user.Isuseractive = param.IsUserActive;
        //        user.Isdelete = param.IsDelete;
        //        user.Createdby = param.CreatedBy;
        //        user.Createdon = param.CreatedOn;
        //        _db.Insert(user);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.ErrorFormat("InsertNewUser", ex.Message);
        //        return false;
        //    }
        //}


        //public UserDetailsResponse SaveBulkRecord(List<UserDetailsBulkRequest> param)
        //{
        //    int successful = 0;
        //    int failed = 0;
        //    int existing = 0;
        //    try
        //    {
        //        foreach (var n in param)
        //        {
        //            bool email = DoesEmailExists(n.Email.ToUpper());
        //            if (!email)
        //            {
        //                var request = new UserDetailsRequest();
        //                request.FirstName = n.FirstName.ToUpper();
        //                request.MiddleName = n.MiddleName.ToUpper();
        //                request.LastName = n.LastName.ToUpper();
        //                request.Email = n.Email.ToUpper();
        //                request.UserName = n.UserName.ToUpper();
        //                request.Password = n.Password;
        //                request.PhoneNo = n.PhoneNo;
        //                request.RoleId = _role.RoleDetailsByName(n.RoleName.ToUpper()).Roleid;
        //                request.ClientId = _client.GetClientByName(n.ClientName.ToUpper()).Clientid;
        //                request.IsUserActive = GetUserStatus(n.UserStatus);
        //                request.CreatedBy = n.CreatedBy;
        //                request.CreatedOn = n.CreatedOn;
        //                request.Sex = n.Sex.ToUpper();
        //                InsertUser(request);
        //                successful++;
        //            }
        //            else
        //            {
        //                existing++;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.ErrorFormat(param.FirstOrDefault().Computername, param.FirstOrDefault().SystemIp, param.FirstOrDefault().CreatedBy, Constants.ActionType.SetUpTerminal.ToString());
        //        return new UserDetailsResponse
        //        {
        //            ResponseCode = "01",
        //            ResponseMessage = ex.Message + " " + successful + " was added successfully",
        //        };
        //    }
        //    Log.InfoFormat(param.FirstOrDefault().Computername, param.FirstOrDefault().SystemIp, param.FirstOrDefault().CreatedBy, Constants.ActionType.BulkUserRecordUpload.ToString() + "failed: " + failed + ", exists: " + existing);
        //    _audit.InsertAudit(param.FirstOrDefault().CreatedBy, Constants.ActionType.BulkUserRecordUpload.ToString(), "Record Upload", DateTime.Now, param.FirstOrDefault().Computername, param.FirstOrDefault().SystemIp);
        //    return new UserDetailsResponse
        //    {
        //        ResponseCode = "00",
        //        ResponseMessage = successful + " Record successfully added, " + failed + " failed and " + existing + " already exists"
        //    };

        //}


        public int GetFreshUser(string Username)
        {
            var _db = DolphinDb.GetInstance();
            string sql = "Select COUNT(*) from Audit_Trail where UserName = @0";
            var _actual = _db.ExecuteScalar<int>(sql, Username);
            return Convert.ToInt32(_actual);
        }

        //public DolUser GetUserById(decimal UserId)
        //{
        //    var actual = _db.SingleById<DolUser>(UserId);
        //    return actual;
        //}



        //public bool ModifyUserDetails(UserDetailsRequest param)
        //{
        //    try
        //    {
        //        var users = _db.SingleOrDefault<DolUser>("WHERE UserId=@0", param.UserId);
        //        users.Firstname = param.FirstName;
        //        users.Middlename = param.MiddleName;
        //        users.Lastname = param.LastName;
        //        users.Username = param.UserName;
        //        users.Password = param.Password; //new EncryptionManager().EncryptValue(param.Password);
        //        users.Userimg = param.UserImg;
        //        users.Sex = param.Sex;
        //        users.Phoneno = param.PhoneNo;
        //        users.Roleid = param.RoleId;
        //        users.Isuseractive = param.IsUserActive;
        //        users.Modifiedby = param.ModifiedBy;
        //        // users.Modifiedon = param.ModifiedOn;
        //        _db.Update(users);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.ErrorFormat("UpdateUserProfile", ex);
        //        return false;
        //    }
        //}


        //public bool UpdateProfile(UserDetailsRequest userDetails)
        //{
        //    try
        //    {
        //        var _user = _db.SingleOrDefault<DolUser>("WHERE UserName=@0", userDetails.UserName);
        //        _user.Firstname = userDetails.FirstName;
        //        _user.Middlename = userDetails.MiddleName;
        //        _user.Lastname = userDetails.LastName;
        //        _user.Userimg = DoFileUpload(userDetails.ImgFile);
        //        _user.Username = userDetails.UserName;
        //        _user.Password = new EncryptionManager().EncryptValue(userDetails.Password);
        //        _user.Phoneno = userDetails.PhoneNo;
        //        _user.Modifiedby = userDetails.ModifiedBy;
        //        _user.Modifiedon = userDetails.ModifiedOn;
        //        _db.Update(_user);
        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.ErrorFormat("ModifyUserDetails", ex);
        //        return false;
        //    }
        //}


        public LoginResponse SendPasswordNotification(LoginRequest param)
        {
            if (param.Email==string.Empty)
            {
                Log.ErrorFormat("Email", param.Email, "Invalid email address");
                return new LoginResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "Kindly supply the email address"
                };

            }

            bool request = DoesEmailExists(param.Email);
            if (!request)
            {
                Log.ErrorFormat("Email", param.Email, "Invalid email address");
                return new LoginResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "Invalid email address"
                };

            }

            EmailRequest emailModel = new EmailRequest();
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
                    object WebConfigurationManager = null;
                    smtp.Host = ""; //WebConfigurationManager.AppSettings["EmailHost"];
                    smtp.EnableSsl = true;
                    NetworkCredential NetworkCred = new NetworkCredential(AuthUsername, AuthPWD);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 503;// Convert.ToInt32(WebConfigurationManager.AppSettings["EmailPort"]);
                    smtp.Send(message);
                    Log.InfoFormat("Email", param.Email);
                    return new LoginResponse
                    {
                        ResponseCode = "00",
                        ResponseMessage = "Email sent kindly check your email",
                    };
                }
                catch (Exception ex)
                {
                    Log.ErrorFormat("Email", param.Email, ex.Message);
                    return new LoginResponse
                    {
                        ResponseCode = "XX",
                        ResponseMessage = "System error"
                    };
                }
            }
        }


        //public bool GetUserStatus(string RoleName)
        //{
        //    switch (RoleName)
        //    {
        //        case "ACTIVE":
        //            return true;
        //        case "INACTIVE":
        //            return false;
        //        default:
        //            return false;
        //    }
        //}


        //private string DoFileUpload(HttpPostedFileBase pic, string filename = "")
        //{
        //    if (pic == null && string.IsNullOrWhiteSpace(filename))
        //    {
        //        return "";
        //    }
        //    if (!string.IsNullOrWhiteSpace(filename) && pic == null) return filename;

        //    string result = DateTime.Now.Millisecond + "UserPics.jpg";
        //    pic.SaveAs(HttpContext.Current.Server.MapPath("~/Content/UserImg/") + result);
        //    return result;
        //}


        //public UserDetailsResponse SetUpUserDetails(UserDetailsRequest param)
        //{
        //    bool success = DoesUsernameExists(param.UserName);
        //    if (success)
        //    {
        //        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.SetupUserAccount.ToString());
        //        return new UserDetailsResponse
        //        {
        //            ResponseCode = "04",
        //            ResponseMessage = "Username already exist"
        //        };
        //    }


        //    try
        //    {
        //        bool result = InsertUser(param);
        //        if (result)
        //        {
        //            Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.SetupUserAccount.ToString());
        //            _audit.InsertAudit(param.UserName, Constants.ActionType.SetupUserAccount.ToString(), "Create user account", DateTime.Now, param.Computername, param.SystemIp);
        //            return new UserDetailsResponse
        //            {
        //                ResponseCode = "00",
        //                ResponseMessage = "Record successfully added"
        //            };
        //        }
        //        else
        //        {
        //            Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.SetupUserAccount.ToString());
        //            return new UserDetailsResponse
        //            {
        //                ResponseCode = "01",
        //                ResponseMessage = "Unable to create record"
        //            };
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.SetupUserAccount.ToString(), ex);
        //        return new UserDetailsResponse
        //        {
        //            ResponseCode = "XX",
        //            ResponseMessage = "System error",
        //            UserDetails = new List<UserDetailsObj>()
        //        };
        //    }
        //}


        //public UserDetailsResponse ListAvailableEngineers()
        //{
        //    var success = GetAllEngineers();
        //    if (success == null)
        //    {
        //        return new UserDetailsResponse
        //        {
        //            ResponseCode = "01",
        //            ResponseMessage = "No data found",
        //            UserDetails = new List<UserDetailsObj>()
        //        };
        //    }

        //    return new UserDetailsResponse
        //    {
        //        ResponseCode = "00",
        //        ResponseMessage = "Success",
        //        UserDetails = new List<UserDetailsObj>()
        //    };
        //}



        //public UserDetailsResponse RetrieveUserDetails(UserDetailsRequest param)
        //{
        //    if (param.UserId != 0)
        //    {
        //        var success = GetUserDetails(param.UserId);
        //        if (success != null)
        //        {
        //            var result = new List<UserDetailsObj>
        //            {
        //                success
        //            };
        //            return new UserDetailsResponse
        //            {
        //                ResponseCode = "00",
        //                ResponseMessage = "Success",
        //                UserDetails = result
        //            };
        //        }
        //        else
        //        {
        //            return new UserDetailsResponse
        //            {
        //                ResponseCode = "01",
        //                ResponseMessage = "UserId doesn't exist",
        //                UserDetails = new List<UserDetailsObj>()
        //            };
        //        }
        //    }
        //    else
        //    {
        //        return new UserDetailsResponse
        //        {
        //            ResponseCode = "01",
        //            ResponseMessage = "Unknown UserId",
        //            UserDetails = new List<UserDetailsObj>()
        //        };
        //    }
        //}


        //public UserDetailsResponse ModifyUserInfo(UserDetailsRequest param)
        //{
        //    bool success = ModifyUserDetails(param);
        //    if (success)
        //    {
        //        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ModifyUserDetails.ToString());
        //        _audit.InsertAudit(param.UserName, Constants.ActionType.ModifyUserDetails.ToString(), "Userdetails modified", DateTime.Now, param.Computername, param.SystemIp);
        //        return new UserDetailsResponse
        //        {
        //            ResponseCode = "00",
        //            ResponseMessage = "Record updated successfully",
        //            UserDetails = new List<UserDetailsObj>()
        //        };
        //    }
        //    else
        //    {
        //        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ModifyUserDetails.ToString());
        //        return new UserDetailsResponse
        //        {
        //            ResponseCode = "01",
        //            ResponseMessage = "Unable to update record",
        //            UserDetails = new List<UserDetailsObj>()
        //        };
        //    }
        //}


        public UserDetailsResponse ListAvailableUser()
        {
            var success = AllUsers();
            if (success == null)
            {
                return new UserDetailsResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "Unable to update record",
                    UserDetails = new List<UserDetailsObj>()
                };
            }
            else
            {
                return new UserDetailsResponse
                {
                    ResponseCode = "00",
                    ResponseMessage = "Success",
                    UserDetails = success
                };
            }
        }
    }
}
