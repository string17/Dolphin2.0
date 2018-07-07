using DAL.CustomObjects;
using DAL.Request;
using DAL.Response;
using DolphinContext.Data.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace BLL.ApplicationLogic
{
    public class RoleManagement
    {
        private readonly DolphinDb _db = DolphinDb.GetInstance();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly AuditManagement _audit = new AuditManagement();

        public List<RoleDetailsObj> GetAllRole()
        {
            return _db.Fetch<RoleDetailsObj>("select * from User_Role");
        }


        public RoleResponse GetAllExistingRole()
        {
            try
            {
                var roles = GetAllRole();
                if (roles != null)
                {
                    return new RoleResponse
                    {
                        ResponseCode = "00",
                        ResponseMessage = "Success",
                        RoleDetails = roles
                    };
                }
                else
                {
                    return new RoleResponse
                    {
                        ResponseCode = "01",
                        ResponseMessage = "No role found",
                        RoleDetails = new List<RoleDetailsObj>()
                    };
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("GetAllExistingRoles", ex.ToString());
                return new RoleResponse() { ResponseCode = "XX", ResponseMessage = "SYSTEM ERROR", RoleDetails = new List<RoleDetailsObj>() };
            }
        }



        public bool UpdateRole(string Title, string Desc, bool Status, int? Id)
        {
            try
            {
                var _role = _db.SingleOrDefault<UserRole>("where RoleId =@0", Id);
                _role.Rolename = Title;
                _role.Roledesc = Desc;
                _role.Isroleactive = Status;
                _db.Update(_role);
                return true;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("UpdateRole", ex.Message);
                return false;
            }
        }

        public bool InsertRole(UserRole request)
        {
            try
            {
                _db.Insert(request);
                return true;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("NewRole", ex.Message);
                return false;
            }
        }


        public UserRole RoleDetailsByName(string RoleName)
        {
            return _db.FirstOrDefault<UserRole>("select * from User_Role where RoleDesc =@0", RoleName);
        }


        public int DeleteRole(int RoleId)
        {
            string sql = "delete from User_Role where RoleId =@0";
            int actual = _db.Delete<UserRole>(sql, RoleId);
            return actual;
        }


        public bool InsertRoleMenu(RoleMenu request)
        {
            try
            {
                _db.Insert(request);
                return true;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("NewRoleMenu", ex.Message);
                return false;
            }
        }


        public RoleDetailsObj GetRoleDetailsById(int RoleId)
        {
            return _db.FirstOrDefault<RoleDetailsObj>("select * from User_Role where RoleId=@0", RoleId);

        }


        public RoleResponse RoleDetails(RoleRequest request)
        {
            try
            {
                var success = GetRoleDetailsById(request.RoleId);
                if (success != null)
                {
                    var result = new List<RoleDetailsObj>
                {
                    success
                };
                    return new RoleResponse { ResponseCode = "00", ResponseMessage = "Success", RoleDetails = result };
                }
                else
                {
                    return new RoleResponse { ResponseCode = "01", ResponseMessage = "Success", RoleDetails = new List<RoleDetailsObj>() };
                }
            }
            catch(Exception ex)
            {
                Log.ErrorFormat("GetRoleDetails", ex.ToString());
                return new RoleResponse() { ResponseCode = "XX", ResponseMessage = "System error", RoleDetails = new List<RoleDetailsObj>() };
            }
        }




        public RoleResponse SetUpNewRole(RoleRequest request)
        {
            var param = new UserRole();
            param.Rolename = request.RoleName;
            param.Roledesc = request.RoleDesc;
            param.Isroleactive = request.IsRoleActive;

            bool success = InsertRole(param);
            if (success)
            {
                Log.InfoFormat(request.Computername, request.SystemIp, request.CreatedBy, Constants.ActionType.SetupUserRole.ToString());
                _audit.InsertAudit(request.CreatedBy, Constants.ActionType.SetupUserRole.ToString(), "Setup User Role", DateTime.Now, request.Computername, request.SystemIp);
                return new RoleResponse
                {
                    ResponseCode = "00",
                    ResponseMessage = "Record successfully created"
                };
            }
            else
            {
                Log.InfoFormat(request.Computername, request.SystemIp, request.CreatedBy, Constants.ActionType.SetupUserRole.ToString());
                return new RoleResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "Unable to create record"
                };
            }
        }

        public List<RoleMenuResponse> GetAllRoleMenu()
        {
            return _db.Fetch<RoleMenuResponse>("select A.*, B.*, C.* from User_Role A inner join Role_Menu B on A.RoleId=B.RoleId inner join Dol_MenuItem C on B.ItemId=C.ItemId");
        }

        public bool UpdateRoleMenu(RoleMenuRequest request)
        {
            try
            {
                var _role = _db.SingleOrDefault<RoleMenu>("where Id =@0", request.Id);
                _role.Itemid = request.ItemId;
                _role.Roleid = request.RoleId;
                _role.Menudesc = request.MenuDesc;
                _db.Update(_role);
                return true;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("UpdateRoleMenu", ex.Message);
                return false;
            }
        }

        public RoleResponse ModifyRoleDetails(RoleRequest param)
        {
            if(param.RoleId==0 && param.RoleName == null)
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.CreatedBy, Constants.ActionType.ModifyUserRole.ToString());
                return new RoleResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "Unknown parameters",
                     RoleDetails=new List<RoleDetailsObj>()
                };
            }

            bool success = UpdateRole(param.RoleName, param.RoleDesc, param.IsRoleActive, param.RoleId);
            if (success)
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.CreatedBy, Constants.ActionType.ModifyUserRole.ToString());
                _audit.InsertAudit(param.CreatedBy, Constants.ActionType.ModifyUserRole.ToString(), "Role modified", DateTime.Now, param.Computername, param.SystemIp);
                return new RoleResponse
                {
                    ResponseCode = "00",
                    ResponseMessage = "Record successfully modified",
                    RoleDetails = new List<RoleDetailsObj>()
                };
            }
            else
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.CreatedBy, Constants.ActionType.ModifyUserRole.ToString());
                return new RoleResponse
                {
                    ResponseCode = "04",
                    ResponseMessage = "Unable to modify record",
                    RoleDetails = new List<RoleDetailsObj>()
                };
            }
        }
    }
}
