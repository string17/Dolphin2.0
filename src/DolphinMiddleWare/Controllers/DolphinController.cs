using BusinessLogic;
using DataAccess.Request;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace DolphinMiddleWare.Controllers
{
    [System.Web.Http.RoutePrefix("dolphin")]
    public class DolphinController : ApiController
    {
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly UserManagement _user;
        private readonly MenuManagement _menu;
        //private readonly RoleManagement _role;
        private readonly ClientManagement _client;
        private readonly AuditManagement _audit;
        //private readonly BrandManagement _brand;
        //private readonly TerminalManagement _terminal;

        public DolphinController()
        {
            _audit = new AuditManagement();
            _user = new UserManagement();
            _menu = new MenuManagement();
            //_role = new RoleManagement();
            _client = new ClientManagement();
            //_brand = new BrandManagement();
            //_terminal = new TerminalManagement();
        }


        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("login")]
        //public HttpResponseMessage ValidateUser(object LoginObj)
        //{
        //    var param = JsonConvert.DeserializeObject<LoginRequest>(LoginObj.ToString());
        //    return Request.CreateResponse(new { status = true, result = _user.ValidateUser(param) });
        //}



        // GET: Dolphin
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("login")]
        public IHttpActionResult ValidateUser(object LoginObj)
        {
            var param = JsonConvert.DeserializeObject<LoginRequest>(LoginObj.ToString());
            return Json(_user.ValidateUser(param));
        }


        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("lockaccess")]
        //public HttpResponseMessage LockAccount(object LoginDetails)
        //{
        //    var param = JsonConvert.DeserializeObject<LoginRequest>(LoginDetails.ToString());
        //    return Request.CreateResponse(new { status = true, result = _audit.LockScreen(param) });
        //}



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("userinfo")]
        public IHttpActionResult GetUserInfo(object UserId)
        {
            //var param = JsonConvert.DeserializeObject<LoginRequest>(LoginObj.ToString());
            return Json(_user.GetUserInfoByUsername(UserId.ToString()));
        }



        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("changepassword")]
        //public HttpResponseMessage ResetPassword(object UserDetails)
        //{
        //    var param = JsonConvert.DeserializeObject<LoginRequest>(UserDetails.ToString());
        //    return Request.CreateResponse(new { status = true, result = _user.ResetPassword(param) });
        //}


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("Menu")]
        public IHttpActionResult GetUserMenu(object LoginObj)
        {
            //var param = JsonConvert.DeserializeObject<LoginRequest>(LoginObj.ToString());
            return Json(_menu.GetUserMenu(LoginObj.ToString()));
        }



        ////Return all active menu
        ////[System.Web.Http.HttpPost]
        ////[System.Web.Http.Route("Submenu")]
        ////public List<DolMenuItem> GetAllSubMenu(MenuObj menu)
        ////{
        ////    var _menus = _menu.GetAllSubMenu();
        ////    return _menus;
        ////}


        ////Return all active Role
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("allrole")]
        //public HttpResponseMessage GetAllRole()
        //{
        //    return Request.CreateResponse(new { status = true, result = _role.GetAllExistingRole() });
        //}


        ////Return all Client details
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("clientdetails")]
        //public HttpResponseMessage ClientDetails(object ClientObj)
        //{
        //    var param = JsonConvert.DeserializeObject<ClientRequest>(ClientObj.ToString());
        //    return Request.CreateResponse(new { status = true, result = _client.GetClientDetailsById(param.ClientId) });
        //}


        ////Return all Role details
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("roledetails")]
        //public HttpResponseMessage RoleDetails(object RoleObj)
        //{
        //    var param = JsonConvert.DeserializeObject<RoleRequest>(RoleObj.ToString());
        //    return Request.CreateResponse(new { status = true, result = _role.RoleDetails(param) });
        //}




        ////insert new Role
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("insertrole")]
        //public HttpResponseMessage InsertRole(object RoleObj)
        //{
        //    var request = JsonConvert.DeserializeObject<RoleRequest>(RoleObj.ToString());
        //    return Request.CreateResponse(new { status = true, result = _role.SetUpNewRole(request) });
        //}



        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("logout")]
        public IHttpActionResult UserLogout(object LoginObj)
        {
            var request = JsonConvert.DeserializeObject<LoginRequest>(LoginObj.ToString());
            return Json(_audit.UserLogout(request));
        }



        ////Modify Role
        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("forgotpassword")]
        public IHttpActionResult ForgotPassword(object LoginObj)
        {
            var request = JsonConvert.DeserializeObject<LoginRequest>(LoginObj.ToString());
            return Json(_user.SendPasswordNotification(request));
        }


        ////Modeify Role
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("modifyrole")]
        //public HttpResponseMessage ModifyRole(object RoleObj)
        //{
        //    var request = JsonConvert.DeserializeObject<RoleRequest>(RoleObj.ToString());
        //    return Request.CreateResponse(new { status = true, result = _role.ModifyRoleDetails(request) });
        //}




        ////Delete Role
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("deleterole")]
        //public string DeleteRole(int RoleId)
        //{
        //    int _roles = _role.DeleteRole(RoleId);
        //    if (_roles != 0)
        //    {
        //        return "Deleted";
        //    }
        //    else
        //    {
        //        return "Not Deleted";
        //    }
        //}


        ////insert new client
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("insertclient")]
        //public HttpResponseMessage InsertClient(object ClientObj)
        //{
        //    var request = JsonConvert.DeserializeObject<ClientRequest>(ClientObj.ToString());
        //    return Request.CreateResponse(new { status = true, result = _client.InsertClientDetails(request) });
        //}


        ////modify client
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("modifyclient")]
        //public HttpResponseMessage ModifyClient(object ClientObj)
        //{
        //    var request = JsonConvert.DeserializeObject<ClientRequest>(ClientObj.ToString());
        //    return Request.CreateResponse(new { status = true, result = _client.ModifyClientDetails(request) });
        //}


        //////insert new brand
        ////[System.Web.Http.HttpPost]
        ////[System.Web.Http.Route("insertbrand")]
        ////public bool InsertBrand(Bra param)
        ////{
        ////    bool _clients = _brand.InsertBrand(param.BrandName, param.BrandDesc, param.IsBrandActive, param.CreatedBy, param.SystemIp, param.Computername);
        ////    if (_clients)
        ////    {
        ////        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.CreateBrand.ToString());
        ////        _audit.InsertAudit(param.UserName, Constants.ActionType.CreateBrand.ToString(), "New Brand ", DateTime.Now, param.Computername, param.SystemIp);
        ////        return true;
        ////    }
        ////    else
        ////    {
        ////        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.CreateBrand.ToString());
        ////        return false;
        ////    }

        ////}


        ////modify brand


        ////[System.Web.Http.HttpPost]
        ////[System.Web.Http.Route("modifybrand")]
        ////public bool ModifyBrand(BrandObj param)
        ////{
        ////    bool success = _brand.UpdateBrandDetails(param.BrandName, param.BrandDesc, param.IsBrandActive, param.CreatedBy, param.BrandId);
        ////    if (success)
        ////    {
        ////        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ModifyBrandDetails.ToString());
        ////        _audit.InsertAudit(param.UserName, Constants.ActionType.ModifyBrandDetails.ToString(), "Brand details changed", DateTime.Now, param.Computername, param.SystemIp);
        ////        return true;
        ////    }
        ////    else
        ////    {
        ////        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ModifyBrandDetails.ToString());
        ////        return false;
        ////    }
        ////}


        ////insert new role menu


        ////[System.Web.Http.HttpPost]
        ////[System.Web.Http.Route("assignmenu")]
        ////public bool InsertRoleMenu(RoleMenuObj param)
        ////{
        ////    var clients = new RoleMenu();
        ////    clients.Itemid = param.ItemId;
        ////    clients.Roleid = param.RoleId;
        ////    clients.Menudesc = param.MenuDesc;
        ////    bool success = _role.InsertRoleMenu(clients);
        ////    if (success)
        ////    {
        ////        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.AssignRoleMenu.ToString());
        ////        _audit.InsertAudit(param.UserName, Constants.ActionType.AssignRoleMenu.ToString(), "Assign MenuRole", DateTime.Now, param.Computername, param.SystemIp);
        ////        return true;
        ////    }
        ////    else
        ////    {
        ////        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.AssignRoleMenu.ToString());
        ////        return false;
        ////    }

        ////}


        ////modify role menu


        ////[System.Web.Http.HttpPost]
        ////[System.Web.Http.Route("modifyrolemenu")]
        ////public bool ModifyRoleMenu(RoleMenuObj param)
        ////{
        ////    bool _clients = _role.UpdateRoleMenu(param);
        ////    if (_clients)
        ////    {
        ////        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ModifyRoleMenu.ToString());
        ////        _audit.InsertAudit(param.UserName, Constants.ActionType.ModifyRoleMenu.ToString(), "Assign Role Menu", DateTime.Now, param.Computername, param.SystemIp);
        ////        return true;
        ////    }
        ////    else
        ////    {
        ////        Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.ModifyRoleMenu.ToString());
        ////        return false;
        ////    }

        ////}


        ////modify rolemenu


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("allclient")]
        public IHttpActionResult ListClient()
        {
            var result = Request.CreateResponse(new { status = true, result = _client.ListAllClient() });
            return Json(result);
        }


        //////modify rolemenu
        ////[System.Web.Http.HttpPost]
        ////[System.Web.Http.Route("listrolemenu")]
        ////public List<RoleMenuObj> ListRoleMenu(RoleMenuObj rolemenu)
        ////{
        ////    var role = _role.GetAllRoleMenu();
        ////    return role;

        ////}


        ////modify rolemenu


        [System.Web.Http.HttpPost]
        [System.Web.Http.Route("allusers")]
        public IHttpActionResult ListUser()
        {
            var result = _user.ListAvailableUser();
            return Json(result);
        }

        ////insert new user
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("newuser")]
        //public HttpResponseMessage InsertUser(object UserObj)
        //{
        //    var request = JsonConvert.DeserializeObject<UserDetailsRequest>(UserObj.ToString());
        //    return Request.CreateResponse(new { status = true, result = _user.SetUpUserDetails(request) });
        //}


        ////Return all Client details
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("userdetails")]
        //public HttpResponseMessage UserDetails(object UserObj)
        //{
        //    var request = JsonConvert.DeserializeObject<UserDetailsRequest>(UserObj.ToString());
        //    return Request.CreateResponse(new { status = true, result = _user.RetrieveUserDetails(request) });
        //}


        ////insert new user
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("modifyuser")]
        //public HttpResponseMessage ModifyUser(object UserObj)
        //{
        //    var request = JsonConvert.DeserializeObject<UserDetailsRequest>(UserObj.ToString());
        //    return Request.CreateResponse(new { status = true, result = _user.ModifyUserInfo(request) });
        //}



        ////modify rolemenu
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("allterminals")]
        //public HttpResponseMessage ListTerminal()
        //{
        //    var success=Request.CreateResponse(new { status = true, result = _terminal.GetAllTerminals() });
        //    return success;
        //}

        ////insert new user
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("newterminal")]
        //public HttpResponseMessage InsertTerminal(object TerminalObj)
        //{
        //    var request = JsonConvert.DeserializeObject<TerminalRequest>(TerminalObj.ToString());
        //    return Request.CreateResponse(new { status = true, result = _terminal.InsertTerminalDetails(request) });
        //}


        ////Return all Client details
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("terminaldetails")]
        //public HttpResponseMessage TerminalDetails(object TerminalObj)
        //{
        //    var request = JsonConvert.DeserializeObject<TerminalRequest>(TerminalObj.ToString());
        //    return Request.CreateResponse(new { status = true, result = _terminal.TerminalDetails(request) });
        // }



        ////insert new user
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("bulkrecord")]
        //public HttpResponseMessage BulkRecord(object UserObj)
        //{
        //    var request = JsonConvert.DeserializeObject<List<UserDetailsBulkRequest>>(UserObj.ToString());
        //    return Request.CreateResponse(new { status = true, result = _user.SaveBulkRecord(request) });
        //}



        ////insert new user
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("uploadterminal")]
        //public HttpResponseMessage UploadTerminal(object TerminalObj)
        //{
        //    var request = JsonConvert.DeserializeObject<List<TerminalBulkRequest>>(TerminalObj.ToString());
        //    return Request.CreateResponse(new { status = true, result = _terminal.UploadBulkTerminalRecords(request) });
        //}


        ////Modify terminal record
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("modifyterminal")]
        //public HttpResponseMessage ModifyTerminal(object TerminalDetails)
        //{
        //    var request = JsonConvert.DeserializeObject<TerminalRequest>(TerminalDetails.ToString());
        //    return Request.CreateResponse(new { status = true, result = _terminal.ModifyTerminalDetails(request) });
        //}


        ////modify rolemenu
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("onlyclients")]
        //public HttpResponseMessage ListOnlyCustomer()
        //{
        //    var request= Request.CreateResponse(new { status = true, result = _client.ListOnlyCustomerRole() });
        //    return request;
        //}

        ////modify rolemenu
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("allstates")]
        //public HttpResponseMessage ListStates()
        //{
        //    var request = Request.CreateResponse(new { status = true, result = _terminal.ListAvailableStates() });
        //    return request;
        //}

        ////modify rolemenu
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("allengineers")]
        //public HttpResponseMessage ListEngineers()
        //{
        //    var request = Request.CreateResponse(new { status = true, result = _user.ListAvailableEngineers() });
        //    return request;
        //}

        ////modify rolemenu
        //[System.Web.Http.HttpPost]
        //[System.Web.Http.Route("allbrands")]
        //public HttpResponseMessage ListBrands()
        //{
        //    var request = Request.CreateResponse(new { status = true, result = _brand.ListAllAvailableBrands() });
        //    return request;
        //}
    }
}