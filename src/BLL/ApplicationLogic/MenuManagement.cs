using DAL.CustomObjects;
using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ApplicationLogic
{
    public class MenuManagement
    {
        private readonly DolphinDb _db = DolphinDb.GetInstance();
        //public List<DolMenu> GetMenuByRoleId(decimal RoleId)
        //{
        //    string SQL = "select DolMenu.MenuId,DolMenu.MenuName,DolMenu.MenuURL,DolMenu.LinkIcon,DolMenu.ExternalUrl, DolRole_Menu.MenuId,DolMenu.ParentId,RoleId from DolMenu INNER JOIN DolRole_Menu ON DolMenu.MenuId=DolRole_Menu.MenuId WHERE DolRole_Menu.RoleId=" + RoleId;
        //    var actual = _db.Fetch<DolMenu>(SQL);
        //    return (actual);
        //}


        public List<UserMenuResp> GetMenuByRole(int RoleId)
        {
            string sql = "select A.*,B.*,C.* from Dol_MenuItem A inner join Role_Menu B on A.ItemId=B.ItemId inner join User_Role C on B.RoleId=C.RoleId where C.RoleId=@0";
            var _actual = _db.Fetch<UserMenuResp>(sql, RoleId).ToList();
            return _actual;
        }


        public List<DolMenuItem> GetAllSubMenu()
        {
            string sql = "select * from Dol_MenuItem where ItemURL is not null";
            var _actual = _db.Fetch<DolMenuItem>(sql);
            return _actual;
        }

        public List<DolMenuItem> GetMenuByMenuId()
        {
            var actual = _db.Fetch<DolMenuItem>();
            return actual;
        }

        public DolMenuItem GetMenuByName(string MenuName)
        {
            string SQL = "Select * from DolMenu where MenuName =@0";
            var actual = _db.FirstOrDefault<DolMenuItem>(SQL, MenuName);
            return actual;
        }

        public List<DolMenuItem> GetMenuByUsername(string Username)
        {

            try
            {
                string sql = "select A.*,B.*,C.* from Dol_MenuItem A inner join Role_Menu B on A.ItemId=B.ItemId inner join Dol_User C on B.RoleId=C.RoleId where A.ItemStatus='true' and C.UserName=@0 order by A.Sequence asc ";
                var _actual = _db.Fetch<DolMenuItem>(sql, Username);

                return _actual;
            }
            catch (Exception ex)
            {
                return null;
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
                return false;
            }

        }
    }
}
