using BLL.ApplicationLogic;
using DAL.CustomObjects;
using DAL.Interfaces;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace DolphinApi.Controllers
{
    [System.Web.Http.RoutePrefix("dolphin")]
    public class DolphinApiController : ApiController
    {
        private static readonly ILog Log=LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly IUser _user;

        public DolphinApiController()
        {
            _user = new UserManagement();
        }

        [System.Web.Http.HttpPost]
        [System.Web.Mvc.Route("login")]
        public LoginResponse ValidateUser(UserObj userLogin)
        {
            var _userName = _user.DoesUsernameExists(userLogin.Username);
            if (_userName)
            {
                var _password = _user.DoesPasswordExists(userLogin.Username, userLogin.Password);
                if (_password)
                {
                    var _success = _user.ValidateUser(userLogin);
                    if (_success != null)
                    {
                        var _menu = _user.GetMenuByUsername(userLogin.Username);
                        if (_menu!=null)
                        {
                            Log.ErrorFormat("login successful", userLogin.Username, userLogin.Password, new LoginResponse { RespCode = "00", RespMessage = "Successful" });
                            return new LoginResponse { RespCode = "00", RespMessage = "Successful", FirstName = _success.FirstName, MiddleName = _success.MiddleName, LastName = _success.LastName, RoleName = _success.RoleName, UserImg = _success.UserImg, PhoneNo = _success.PhoneNo, Email = _success.Email, Status = _success.Status, UserName = _success.UserName, CompanyName = _success.CompanyName, CompanyId = _success.CompanyId, UserId = _success.UserId, CreatedBy = _success.CreatedBy, CreatedOn = _success.CreatedOn,Menu=_success.Menu };
                        }
                        else
                        {
                            Log.ErrorFormat("login not successful", new LoginResponse { RespCode = "01", RespMessage = "Unsuccessful" });
                            return new LoginResponse { RespCode = "01", RespMessage = "Unsuccessful" };
                        }
                    }
                    else
                    {
                        Log.ErrorFormat("login not successful", new LoginResponse { RespCode = "01", RespMessage = "Unsuccessful" });
                        return new LoginResponse { RespCode = "01", RespMessage = "Unsuccessful" };

                    }
                }
                else
                {
                    Log.ErrorFormat("Password: ", userLogin.Username, userLogin.Password, new LoginResponse { RespCode = "01", RespMessage = "Invalid Username" });
                    return new LoginResponse { RespCode = "01", RespMessage = "Invalid Username" };
                }
            }
            else
            {
                Log.ErrorFormat("Username: ", userLogin.Username,  new LoginResponse { RespCode = "01", RespMessage = "Invalid Username" });
                return new LoginResponse { RespCode = "01", RespMessage = "Invalid Username" };
            }
        
        }

    }
}