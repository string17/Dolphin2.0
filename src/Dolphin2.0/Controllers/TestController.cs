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

        public TestController()
        {
            _audit = new AuditManagement();
            _user = new UserManagement();
            _menu = new MenuManagement();
            _role = new RoleManagement();
            _client = new ClientManagement();
        }


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("login")]
        public UserResponse ValidateUser(UserObj param)
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
                                _audit.InsertAudit(param.Username, Constants.ActionType.Login.ToString(), "Valid Credentials", param.DateLog, param.SystemName, param.SystemIp);
                                _audit.InsertSessionTracker(param.Username, param.SystemIp, param.SystemName);
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



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("userinfo")]
        public UserInfo GetUserInfo(UserObj param)
        {
            var _success = _user.GetUserInfo(param);
            if (_success != null)
            {
                Log.ErrorFormat("User details", param.Username, param.Password, new UserInfo { RespCode = "00", RespMessage = "Successful" });
                _audit.InsertAudit(param.Username, Constants.ActionType.GetUserInfo.ToString(), "User details", param.DateLog, param.SystemName, param.SystemIp);
                return new UserInfo { RespCode = "00", RespMessage = "Successful", FirstName = _success.FirstName, MiddleName = _success.MiddleName, LastName = _success.LastName, RoleName = _success.RoleName, UserImg = _success.UserImg, PhoneNo = _success.PhoneNo, Email = _success.Email, IsUserActive = _success.IsUserActive, UserName = _success.UserName, Title = _success.Title, ClientId = _success.ClientId, UserId = _success.UserId, CreatedBy = _success.CreatedBy, CreatedOn = _success.CreatedOn };
            }
            else
            {
                Log.ErrorFormat("User details", new UserInfo { RespCode = "01", RespMessage = "Unsuccessful" });
                return new UserInfo { RespCode = "01", RespMessage = "Unsuccessful" };

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
        [System.Web.Http.Route("Allrole")]
        public List<RoleObj> GetAllRole(RoleObj role)
        {
            var _roles = _role.GetAllRole();
            return _roles;
        }

        //insert new Role
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("insertrole")]
        public RoleResp InsertRole(RoleObj role)
        {
            var userRole = new UserRole();
            userRole.Title = role.Title;
            userRole.Desc = role._Desc;
            userRole.Isroleactive = role.IsRoleActive;

            bool _roles = _role.InsertRole(role);
            if (_roles)
            {
                return new RoleResp
                {
                    RespCode = "00",
                    RespMessage = "Success"
                };
            }
            else
            {
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
        public UserResponse ModifyRole(string Username)
        {
            bool userLogout = _audit.TerminateSession(Username);
            if (userLogout)
            {
                return new UserResponse
                {
                    RespCode = "00",
                    RespMessage = "Logout successful"

                };
            }
            else
            {
                return new UserResponse
                {
                    RespCode = "00",
                    RespMessage = "Logout failed"
                };
            }
        }



        //Modify Role
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("forgotpassword")]
        public UserResponse ForgotPassword(string Email)
        {
            var resp = _user.PasswordNotification(Email);
            return resp;

        }


        //Modeify Role
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("modifyrole")]
        public bool ModifyRole(string Title, string Desc, bool Status, int? Id)
        {
            bool _roles = _role.UpdateRole(Title, Desc, Status, Id);
            if (_roles)
            {
                return true;
            }
            else
            {
                return false;
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
        public bool InsertClient(ClientObj client)
        {
            var clients = new DolClient();
            clients.Clientname = client.ClientName;
            clients.Clientalias = client.ClientAlias;
            clients.Resptime = client.RespTime;
            clients.Resttime = client.RestTime;
            clients.Isclientactive = client.IsClientActive;
            clients.Createdon = client.CreatedOn;
            clients.Createdby = client.CreatedBy;
            clients.Clientbanner = _client.DoFileUpload(client.ClientBanner);
            bool _clients = _client.InsertClient(clients);
            if (_clients)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        //insert new client
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("modifyclient")]
        public bool ModifyClient(ExtClientObj client)
        {
            //var clients = new DolClient();
            //clients.Clientname = client.ClientName;
            //clients.Clientalias = client.ClientAlias;
            //clients.Resptime = client.RespTime;
            //clients.Resttime = client.RestTime;
            //clients.Isclientactive = client.IsClientActive;
            //clients.Createdon = client.CreatedOn;
            //clients.Createdby = client.CreatedBy;
            //clients.Clientbanner = _client.DoFileUpload(client.ClientBanner);
            bool _clients = _client.UpdateClient(client);
            if (_clients)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        //insert new role menu
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("assignmenu")]
        public bool InsertRoleMenu(RoleMenuObj rolemenu)
        {
            var clients = new RoleMenu();
            clients.Itemid = rolemenu.ItemId;
            clients.Roleid = rolemenu.RoleId;
            clients.Menudesc = rolemenu.MenuDesc;
            bool _clients = _role.InsertRoleMenu(clients);
            if (_clients)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        //modify role menu
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("modifyrolemenu")]
        public bool ModifyRoleMenu(RoleMenuObj rolemenu)
        {
            bool _clients = _role.UpdateRoleMenu(rolemenu);
            if (_clients)
            {
                return true;
            }
            else
            {
                return false;
            }

        }


        //modify rolemenu
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("modifyrolemenu")]
        public List<RoleMenuObj> ListRoleMenu(RoleMenuObj rolemenu)
        {
            var role = _role.GetAllRoleMenu();
            return role;

        }


        //insert new user
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("insertuser")]
        public UserResponse InsertUser(UserDetails userDetails)
        {
            bool user = _user.InsertUser(userDetails);
            if (user)
            {
                return new UserResponse
                {
                    RespCode = "00",
                    RespMessage = "Success"
                };
            }
            else
            {
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
        public UserResponse ModifyUser(UserDetails userDetails)
        {
            bool user = _user.ModifyUserDetails(userDetails);
            if (user)
            {
                return new UserResponse
                {
                    RespCode = "00",
                    RespMessage = "Success"
                };
            }
            else
            {
                return new UserResponse
                {
                    RespCode = "01",
                    RespMessage = "Failure"
                };
            }
        }
    }
}

