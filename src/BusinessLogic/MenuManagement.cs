using DataAccess.Response;
using DolphinContext.Data.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
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

        public List<MenuDetailsObj> GetMenuByUsername(string Username)
        {
            string sql = "select A.*,B.*,C.* from Dol_MenuItem A inner join Role_Menu B on A.ItemId=B.ItemId inner join Dol_User C on B.RoleId=C.RoleId where A.ItemStatus='true' and C.UserName=@0 order by A.Sequence asc ";
            return _db.Fetch<MenuDetailsObj>(sql, Username);
        }

        public UserMenuResponse GetUserMenu(string UserMenu)
        {
            if (UserMenu == string.Empty)
            {
                return new UserMenuResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "Kindly supply username",
                    UserMenuDetails = new List<MenuDetailsObj>()
                };
            }


            try
            {
                var menu = GetMenuByUsername(UserMenu);
                if (menu != null)
                {
                    return new UserMenuResponse
                    {
                        ResponseCode = "00",
                        ResponseMessage = "Success",
                        UserMenuDetails = menu
                    };
                }
                else
                {
                    return new UserMenuResponse
                    {
                        ResponseCode = "01",
                        ResponseMessage = "No Menu",
                        UserMenuDetails = new List<MenuDetailsObj>()
                    };
                }
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("GetUserMenu", ex.ToString());
                return new UserMenuResponse() { ResponseCode = "XX", ResponseMessage = "SYSTEM ERROR", UserMenuDetails = new List<MenuDetailsObj>() };
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
