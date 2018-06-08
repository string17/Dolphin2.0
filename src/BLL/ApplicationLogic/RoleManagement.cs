using DAL.CustomObjects;
using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ApplicationLogic
{
    public class RoleManagement
    {
        private readonly DolphinDb _db = DolphinDb.GetInstance();

        public List<RoleObj> GetAllRole()
        {
            string sql = "select * from User_Role";
            var _actual = _db.Fetch<RoleObj>(sql);
            return _actual;
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
            catch (Exception)
            {

                return false;
            }
        }

        public bool InsertRole(RoleObj role)
        {
            //try
            //{
               // _db.Insert(role);
                return true;
            //}
            //catch (Exception ex)
            //{
              //  return false;
            //}
        }


        public UserRole RoleDetailsByName(string RoleName)
        {
            string sql = "select * from User_Role where RoleDesc =@0";
            var role = _db.FirstOrDefault<UserRole>(sql, RoleName);
            return role;
        }


        public int DeleteRole(int RoleId)
        {
            string sql = "delete from User_Role where RoleId =@0";
            int actual = _db.Delete<UserRole>(sql, RoleId);
            return actual;
        }


        public bool InsertRoleMenu(RoleMenu role)
        {
            try
            {
                _db.Insert(role);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public RoleResp GetRoleDetails(int RoleId)
        {
            string sql = "select * from User_Role where RoleId=@0";
            var _actual = _db.FirstOrDefault<RoleResp>(sql, RoleId);
            if (_actual != null)
            {
                return new RoleResp
                {
                    RespCode = "00",
                    RespMessage = "Success",
                    RoleId = _actual.RoleId,
                    RoleName = _actual.RoleName,
                    RoleDesc = _actual.RoleDesc,
                    IsRoleActive = _actual.IsRoleActive
                };
            }
            else
            {
                return new RoleResp
                {
                    RespCode = "04",
                    RespMessage = "Failed",
                };
            }
          
        }


        public List<RoleMenuObj> GetAllRoleMenu()
        {
            string sql = "select A.*, B.*, C.* from User_Role A inner join Role_Menu B on A.RoleId=B.RoleId inner join Dol_MenuItem C on B.ItemId=C.ItemId";
            var _actual = _db.Fetch<RoleMenuObj>(sql);
            return _actual;
            
        }

        public bool UpdateRoleMenu(RoleMenuObj roleMenu)
        {
            try
            {
                var _role = _db.SingleOrDefault<RoleMenu>("where Id =@0", roleMenu.Id);
                _role.Itemid = roleMenu.ItemId;
                _role.Roleid = roleMenu.RoleId;
                _role.Menudesc = roleMenu.MenuDesc;
                _db.Update(_role);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
