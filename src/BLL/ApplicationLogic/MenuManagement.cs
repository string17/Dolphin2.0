using DAL.Response;
using DolphinContext.Data.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ApplicationLogic
{
    public class MenuManagement
    {
        private readonly DolphinDb _db = DolphinDb.GetInstance();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        //public List<DolMenu> GetMenuByRoleId(decimal RoleId)
        //{
        //    string SQL = "select DolMenu.MenuId,DolMenu.MenuName,DolMenu.MenuURL,DolMenu.LinkIcon,DolMenu.ExternalUrl, DolRole_Menu.MenuId,DolMenu.ParentId,RoleId from DolMenu INNER JOIN DolRole_Menu ON DolMenu.MenuId=DolRole_Menu.MenuId WHERE DolRole_Menu.RoleId=" + RoleId;
        //    var actual = _db.Fetch<DolMenu>(SQL);
        //    return (actual);
        //}


        public List<UserMenuResponse> GetMenuByRole(int RoleId)
        {
            string sql = "select A.*,B.*,C.* from Dol_MenuItem A inner join Role_Menu B on A.ItemId=B.ItemId inner join User_Role C on B.RoleId=C.RoleId where C.RoleId=@0";
            var _actual = _db.Fetch<UserMenuResponse>(sql, RoleId).ToList();
            return _actual;
        }


        public List<DolMenuItem> GetAllSubMenu()
        {
              return _db.Fetch<DolMenuItem>("select * from Dol_MenuItem where ItemURL is not null");
        }


        public List<DolMenuItem> GetMenuByMenuId()
        {
            var actual = _db.Fetch<DolMenuItem>();
            return actual;
        }


        public DolMenuItem GetMenuByName(string MenuName)
        {
            return _db.FirstOrDefault<DolMenuItem>("Select * from DolMenu where MenuName =@0", MenuName);
        }

        public List<RoleMenuDetailsObj> GetMenuByUsername(string Username)
        {
            try
            {
                string sql = "select A.*,B.*,C.* from Dol_MenuItem A inner join Role_Menu B on A.ItemId=B.ItemId inner join Dol_User C on B.RoleId=C.RoleId where A.ItemStatus='true' and C.UserName=@0 order by A.Sequence asc ";
                var _actual = _db.Fetch<RoleMenuDetailsObj>(sql, Username);
                return _actual;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("GetMenu", ex.Message);
                return null;
            }
        }

        public RoleMenuResponse GetUserMenu(string UserMenu)
        {
            try
            {
                if (!string.IsNullOrEmpty(UserMenu))
                {
                    var menu = GetMenuByUsername(UserMenu);

                    return new RoleMenuResponse
                    {
                        ResponseCode = "00",
                        ResponseMessage = "Success",
                        RoleMenuDetails = menu
                    };
                }
                else
                {
                    return new RoleMenuResponse
                    {
                        ResponseCode = "01",
                        ResponseMessage = "No Menu",
                        RoleMenuDetails = new List<RoleMenuDetailsObj>()
                    };
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("GetUserMenu", ex.ToString());
                return new RoleMenuResponse() { ResponseCode = "XX", ResponseMessage = "SYSTEM ERROR", RoleMenuDetails = new List<RoleMenuDetailsObj>() };
            }
        }

        public List<DolMenuItem> GetMenuById()
        {
            var actual = _db.Fetch<DolMenuItem>();
            return actual;
        }

        public bool InsertMenu(DolMenuItem MenuName)
        {
            try
            {
                _db.Insert(MenuName);
                return true;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("InsertMenu", ex.Message);
                return false;
            }

        }
    }
}
