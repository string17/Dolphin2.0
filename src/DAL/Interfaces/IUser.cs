using DAL.CustomObjects;
using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL.Interfaces
{
    public interface IUser
    {
        LoginResponse ValidateUser(UserObj userLogin);
        bool UpdatePassword(string Password, int? Id);
        LoginResponse GetUserById(int UserId);

        DolUser GetUserByUsername(string Username, int? CompanyId);
        List<UserObj> GetUserByCompany();
   
        //bool UpdatePassword(string Password, int? Id);
        DolUser ModifyPassword(int Id);
     
        //UserObj GetUserProfileByUsername(string Username);
   
        //LoginResponse GetUserById(int UserId);

        //EngineerViewModel getEngineerTerminal(string roleName);

        List<DolUser> GetEngineer();
        List<DolUser> getAllUsers();
        List<DolUser> GetUserById();
        List<DolMenu> GetMenuByUsername(string Username);
        bool DoesUsernameExists(string Username);
        bool DoesPasswordExists(string Username, string Password);
     
        bool InsertUser(DolUser Username);
        int GetFreshUser(string Username);
      
        DolUser GetUserById(decimal UserId);
     
        bool UpdateUser(string FirstName, string MiddleName, string LastName, string UserName, string Password, string UserImg, string PhoneNo, int? RoleId, bool? Status, string ModifiedBy, DateTime ModifiedOn, int? UserId);
        bool UpdateProfile(string FirstName, string MiddleName, string LastName, string UserName, string Password, string UserImg, string PhoneNo, string ModifiedBy, DateTime ModifiedOn, string Username);
        string DoFileUpload(HttpPostedFileBase pic, string filename = "");

        List<DolMenu> GetMenuByRoleId(decimal RoleId);
     
        List<DolMenu> GetMenuByMenuId();
       
        DolMenu GetMenuByName(string MenuName);
        List<DolMenu> getMenuByUsername(string Username);
       
        List<DolMenu> getMenuById();
        bool InsertMenu(DolMenu MenuName);
    
    }
}
