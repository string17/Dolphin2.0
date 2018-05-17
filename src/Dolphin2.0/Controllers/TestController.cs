using BLL.ApplicationLogic;
using DAL.CustomObjects;
using DolphinContext.Data.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace Dolphin2._0.Controllers
{
    [System.Web.Http.RoutePrefix("dolphin")]
    public class TestController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly UserManagement _user;
        private readonly MenuManagement _menu;
        private readonly RoleManagement _role;
        private readonly ClientManagement _client;
        private readonly AuditManagement _audit;
        private readonly BrandManagement _brand;

        public TestController()
        {
            _audit = new AuditManagement();
            _user = new UserManagement();
            _menu = new MenuManagement();
            _role = new RoleManagement();
            _client = new ClientManagement();
            _brand = new BrandManagement();
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("login")]
        public UserResponse ValidateUser(UserObj param)
        {
            if (param.Username != null && param.Password != null && param.Computername != null && param.SystemIp != null)
            {
                var _userName = _user.DoesUsernameExists(param.Username);
                if (_userName)
                {
                    var _password = _user.DoesPasswordExists(param.Username, param.Password);
                    if (_password)
                    {
                        bool _validUser = _user.IsUserActive(param.Username, param.Password);
                        if (_validUser)
                        {

                            bool isCompanyActive = _user.IsCompanyActive(param.Username);
                            if (isCompanyActive)
                            {
                                var existingAccount = _user.GetFreshUser(param.Username);
                                if (existingAccount != 0)
                                {
                                    Log.ErrorFormat("Valid credentials", param.Username, param.Password, new UserResponse { RespCode = "00", RespMessage = "Successful" });
                                    _audit.InsertAudit(param.Username, Constants.ActionType.Login.ToString(), "Valid Credentials", DateTime.Now, param.Computername, param.SystemIp);
                                    _audit.InsertSessionTracker(param.Username, param.SystemIp, param.Computername);
                                    return new UserResponse { RespCode = "00", RespMessage = "Successful" };
                                }
                                else
                                {
                                    Log.ErrorFormat("Valid credentials", param.Username, param.Password, new UserResponse { RespCode = "01", RespMessage = "Successful" });
                                    return new UserResponse { RespCode = "01", RespMessage = "Successful" };
                                }

                            }
                            else
                            {
                                Log.ErrorFormat("Password: ", param.Username, param.Password, new UserResponse { RespCode = "02", RespMessage = "Company's account is not active" });
                                return new UserResponse { RespCode = "02", RespMessage = "Company's account is not active" };
                            }

                        }
                        else
                        {
                            Log.ErrorFormat("Password: ", param.Username, param.Password, new UserResponse { RespCode = "02", RespMessage = "Inactive account" });
                            return new UserResponse { RespCode = "02", RespMessage = "Inactive account" };
                        }

                    }
                    else
                    {
                        Log.ErrorFormat("Password: ", param.Username, param.Password, new UserResponse { RespCode = "02", RespMessage = "Invalid Password" });
                        return new UserResponse { RespCode = "02", RespMessage = "Invalid Password" };
                    }
                }
                else
                {
                    Log.ErrorFormat("Username: ", param.Username, new UserResponse { RespCode = "02", RespMessage = "Invalid Username" });
                    return new UserResponse { RespCode = "02", RespMessage = "Invalid Username" };
                }
            }
            else
            {
                //Log.ErrorFormat( new UserResponse { RespCode = "06", RespMessage = "Null parameter" });
                return new UserResponse { RespCode = "06", RespMessage = "Kindly supply the missing parameter" };
            }
        }



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("userinfo")]
        public UserInfo GetUserInfo(UserObj param)
        {
            var _success = _user.GetUserInfo(param);
            if (_success != null)
            {
                Log.ErrorFormat("User details", param.Username, param.Password, new UserInfo { RespCode = "00", RespMessage = "Successful" });
                _audit.InsertAudit(param.Username, Constants.ActionType.GetUserInfo.ToString(), "User details", DateTime.Now, param.Computername, param.SystemIp);
                return new UserInfo { RespCode = "00", RespMessage = "Successful", FirstName = _success.FirstName, MiddleName = _success.MiddleName, LastName = _success.LastName, RoleName = _success.RoleName, UserImg = _success.UserImg, PhoneNo = _success.PhoneNo, Email = _success.Email, IsUserActive = _success.IsUserActive, UserName = _success.UserName, Title = _success.Title, ClientId = _success.ClientId, UserId = _success.UserId, CreatedBy = _success.CreatedBy, CreatedOn = _success.CreatedOn };
            }
            else
            {
                Log.ErrorFormat("User details", new UserInfo { RespCode = "01", RespMessage = "Unsuccessful" });
                return new UserInfo { RespCode = "01", RespMessage = "Unsuccessful" };

            }
        }



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("changepassword")]
        public UserResponse ResetPassword(UserObj param)
        {
            if (param.Username != null && param.Password != null && param.Computername != null && param.SystemIp != null)
            {
                bool checkPassword = _user.DoesPasswordExists(param.Username, param.Password);
                if (!checkPassword)
                {
                    bool success = _user.UpdatePassword(param.Username, param.Password);
                    if (success)
                    {
                        Log.InfoFormat(param.Computername, param.SystemIp, param.Username, Constants.ActionType.ResetPassword.ToString());
                        _audit.InsertAudit(param.Username, Constants.ActionType.ResetPassword.ToString(), "Password changed", param.EventDate, param.Computername, param.SystemIp);
                        return new UserResponse
                        {
                            RespCode = "00",
                            RespMessage = "Password Updated successfully"
                        };
                    }
                    else
                    {

                        Log.InfoFormat(param.Computername, param.SystemIp, param.Username, Constants.ActionType.ResetPassword.ToString(), "Failed");
                        return new UserResponse
                        {
                            RespCode = "03",
                            RespMessage = "Password unable to Update"
                        };
                    }
                }
                else
                {
                    Log.InfoFormat(param.Computername, param.SystemIp, param.Username, Constants.ActionType.ResetPassword.ToString(), "Password repetition");
                    return new UserResponse
                    {
                        RespCode = "04",
                        RespMessage = "Password must not be the same with old password"
                    };
                }
            
            }
            else
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.Username, Constants.ActionType.ResetPassword.ToString(),"Empty fields");
                return new UserResponse
                {
                    RespCode = "00",
                    RespMessage = "Kindly supply the missing value"
                };
            }
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Menu")]
        public List<DolMenuItem> GetUserMenu(MenuObj param)
        {
            var menu = _menu.GetMenuByUsername(param.Username);
            return menu;

        }



        //Return all active menu
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Submenu")]
        public List<DolMenuItem> GetAllSubMenu(MenuObj menu)
        {
            var _menus = _menu.GetAllSubMenu();
            return _menus;
        }


        //Return all active Role
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("allrole")]
        public List<RoleObj> GetAllRole()
        {
            var _roles = _role.GetAllRole();
            return _roles;
        }


        //Return all Client details
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("clientdetails")]
        public ClientResp ClientDetails(ClientObj param)
        {
            var success = _client.GetClientDetails(param.ClientId);
            return success;
        }


        //Return all Role details
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("roledetails")]
        public RoleResp RoleDetails(RoleObj param)
        {
            var _roles = _role.GetRoleDetails(param.RoleId);
            return _roles;
        }




        //insert new Role
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("insertrole")]
        public RoleResp InsertRole(RoleObj param)
        {
            var userRole = new UserRole();
            userRole.Title = param.Title;
            userRole.Desc = param._Desc;
            userRole.Isroleactive = param.IsRoleActive;

            bool success = _role.InsertRole(param);
            if (success)
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.SetupUserRole.ToString());
                _audit.InsertAudit(param.UserName, Constants.ActionType.SetupUserRole.ToString(), "Setup User Role", DateTime.Now, param.Computername, param.SystemIp);
                return new RoleResp
                {
                    RespCode = "00",
                    RespMessage = "Success"
                };
            }
            else
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.SetupUserRole.ToString());
                return new RoleResp
                {
                    RespCode = "01",
                    RespMessage = "Failure"
                };
            }

        }


        //Modify Role
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("logout")]
        public UserResponse UserLogout(UserObj param)
        {
            bool userLogout = _audit.TerminateSession(param.Username);
            if (userLogout)
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.Username, Constants.ActionType.LogoutAccount.ToString());
                _audit.InsertAudit(param.Username, Constants.ActionType.LogoutAccount.ToString(), "Logout", DateTime.Now, param.Computername, param.SystemIp);
                return new UserResponse
                {
                    RespCode = "00",
                    RespMessage = "Logout successful"

                };
            }
            else
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.Username, Constants.ActionType.LogoutAccount.ToString());
                return new UserResponse
                {
                    RespCode = "04",
                    RespMessage = "Logout failed"
                };
            }
        }



        //Modify Role
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("forgotpassword")]
        public UserResponse ForgotPassword(UserObj param)
        {
            if (param.Email!=null && param.Computername != null && param.SystemIp != null)
            {
                var resp = _user.PasswordNotification(param);
                Log.InfoFormat(param.Computername, param.SystemIp, param.Username, Constants.ActionType.ForgotPassword.ToString());
                _audit.InsertAudit(param.Username, Constants.ActionType.ForgotPassword.ToString(), "Forgot Password", DateTime.Now, param.Computername, param.SystemIp);
                return resp;
            }
            else
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.Username, Constants.ActionType.ForgotPassword.ToString());
                return new UserResponse
                {
                    RespCode = "04",
                    RespMessage = "Kindly supply the missing value"
                };
            }
           

        }


        //Modeify Role
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("modifyrole")]
        public RoleResp ModifyRole(RoleResp param)
        {
            bool _roles = _role.UpdateRole(param.Title, param._Desc, param.IsRoleActive, param.RoleId);
            if (_roles)
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ModifyUserRole.ToString());
                _audit.InsertAudit(param.UserName, Constants.ActionType.ModifyUserRole.ToString(), "Role modified", DateTime.Now, param.Computername, param.SystemIp);
                return new RoleResp {
                    RespCode="00",
                    RespMessage="Success"
                     };
            }
            else
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ModifyUserRole.ToString());
                return new RoleResp
                {
                    RespCode = "04",
                    RespMessage = "Failure"
                };
            }
        }




        //Delete Role
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("deleterole")]
        public string DeleteRole(int RoleId)
        {
            int _roles = _role.DeleteRole(RoleId);
            if (_roles != 0)
            {
                return "Deleted";
            }
            else
            {
                return "Not Deleted";
            }
        }


        //insert new client
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("insertclient")]
        public bool InsertClient(ClientObj param)
        {
            bool _clients = _client.InsertClient(param.ClientName,param.ClientAlias,param.ClientBanner,param.RespTime,param.RestTime,param.IsClientActive,param.CreatedBy);
            if (_clients)
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.SetupClient.ToString());
                _audit.InsertAudit(param.UserName, Constants.ActionType.SetupClient.ToString(), "Setup client account", DateTime.Now, param.Computername, param.SystemIp);
                return true;
            }   
            else
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.SetupClient.ToString());
                return false;
            }

        }


        //modify client
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("modifyclient")]
        public ClientResp ModifyClient(ClientObj param)
        {
            bool success = _client.UpdateClient(param.ClientName, param.ClientAlias, param.ClientBanner, param.RespTime, param.RestTime, param.IsClientActive, param.ClientId);
            if (success)
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ModifyClientDetails.ToString());
                _audit.InsertAudit(param.UserName, Constants.ActionType.ModifyClientDetails.ToString(), "Client detail changed", DateTime.Now, param.Computername, param.SystemIp);
                return new ClientResp
                {
                    RespCode = "00",
                    RespMessage = "Success"

                };
            }
            else
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ModifyClientDetails.ToString());
                return new ClientResp
                {
                    RespCode = "04",
                    RespMessage = "Failure"

                };
            }

        }


        //insert new brand
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("insertbrand")]
        public bool InsertBrand(BrandObj param)
        {
            bool _clients = _brand.InsertBrand(param.BrandName, param.BrandDesc, param.IsBrandActive, param.CreatedBy,param.SystemIp,param.Computername);
            if (_clients)
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.CreateBrand.ToString());
                _audit.InsertAudit(param.UserName, Constants.ActionType.CreateBrand.ToString(), "New Brand ", DateTime.Now, param.Computername, param.SystemIp);
                return true;
            }
            else
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.CreateBrand.ToString());
                return false;
            }

        }


        //modify brand
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("modifybrand")]
        public bool ModifyBrand(BrandObj param)
        {
            bool success = _brand.UpdateBrandDetails(param.BrandName, param.BrandDesc, param.IsBrandActive,param.CreatedBy, param.BrandId);
            if (success)
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ModifyBrandDetails.ToString());
                _audit.InsertAudit(param.UserName, Constants.ActionType.ModifyBrandDetails.ToString(), "Brand details changed", DateTime.Now, param.Computername, param.SystemIp);
                return true;
            }
            else
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ModifyBrandDetails.ToString());
                return false;
            }
        }


        //insert new role menu
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("assignmenu")]
        public bool InsertRoleMenu(RoleMenuObj param)
        {
            var clients = new RoleMenu();
            clients.Itemid = param.ItemId;
            clients.Roleid = param.RoleId;
            clients.Menudesc = param.MenuDesc;
            bool success = _role.InsertRoleMenu(clients);
            if (success)
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.AssignRoleMenu.ToString());
                _audit.InsertAudit(param.UserName, Constants.ActionType.AssignRoleMenu.ToString(), "Assign MenuRole", DateTime.Now, param.Computername, param.SystemIp);
                return true;
            }
            else
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.AssignRoleMenu.ToString());
                return false;
            }

        }


        //modify role menu
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("modifyrolemenu")]
        public bool ModifyRoleMenu(RoleMenuObj param)
        {
            bool _clients = _role.UpdateRoleMenu(param);
            if (_clients)
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName , Constants.ActionType.ModifyRoleMenu.ToString());
                _audit.InsertAudit(param.UserName, Constants.ActionType.ModifyRoleMenu.ToString(), "Assign Role Menu", DateTime.Now, param.Computername, param.SystemIp);
                return true;
            }
            else
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ModifyRoleMenu.ToString());
                return false;
            }

        }


        //modify rolemenu
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("allclient")]
        public List<DolClient> ListClient()
        {
            var success = _client.GetClientList();
            return success;

        }


        //modify rolemenu
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("listrolemenu")]
        public List<RoleMenuObj> ListRoleMenu(RoleMenuObj rolemenu)
        {
            var role = _role.GetAllRoleMenu();
            return role;

        }


        //insert new user
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("insertuser")]
        public UserResponse InsertUser(UserDetails param)
        {
            bool user = _user.InsertUser(param);
            if (user)
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.SetupUserAccount.ToString());
                _audit.InsertAudit(param.UserName, Constants.ActionType.SetupUserAccount.ToString(), "Create user account",DateTime.Now, param.Computername, param.SystemIp);
                return new UserResponse
                {
                    RespCode = "00",
                    RespMessage = "Success"
                };
            }
            else
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.SetupUserAccount.ToString());
                 return new UserResponse
                {
                    RespCode = "01",
                    RespMessage = "Failure"
                };
            }
        }


        //insert new user
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("modifyuser")]
        public UserResponse ModifyUser(UserDetails param)
        {
            bool user = _user.ModifyUserDetails(param);
            if (user)
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ModifyUserDetails.ToString());
                _audit.InsertAudit(param.UserName, Constants.ActionType.ModifyUserDetails.ToString(), "Userdetails modified", DateTime.Now, param.Computername, param.SystemIp);
                return new UserResponse
                {
                    RespCode = "00",
                    RespMessage = "Success"
                };
            }
            else
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ModifyUserDetails.ToString());
                return new UserResponse
                {
                    RespCode = "01",
                    RespMessage = "Failure"
                };
            }
        }
    }
}

