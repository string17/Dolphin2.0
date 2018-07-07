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
    public class AuditManagement
    {
        private readonly DolphinDb _db = DolphinDb.GetInstance();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public bool InsertAudit(string UserName,string UserActivity,string Comment, DateTime EventDate,string SystemName,string SystemIp)
        {
            try
            {
                var audit = new AuditTrail();
                audit.Username = UserName;
                audit.Useractivity = UserActivity;
                audit.Comment = Comment;
                audit.Eventdate = EventDate;
                audit.Systemname = SystemName;
                audit.Systemip = SystemIp;
                _db.Insert<AuditTrail>(audit);
                return true;
            }
            catch(Exception ex)
            {
                Log.ErrorFormat("InsertAudit", ex.Message);
                return false;
            }
           
        }

        public List<AuditResponse> GetAuditById()
        {
              return _db.Fetch<AuditResponse>("select * from Audit_Trail order by AuditId desc");
        }


        public bool InsertSessionTracker(string UserName, string SystemIp, string SystemName)
        {
            try
            {
                int SessionId = GetLoginSequence();
                var track = new UserTracking();
                track.Username = UserName;
                track.Sessionid = new EncryptionManager().EncryptValue(SessionId.ToString());
                track.Systemip = SystemIp;
                track.Systemname = SystemName;
                track.Logindate = DateTime.Now;
                _db.Insert(track);
                return true;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("InsertSession", ex.Message);
                return false;
            }
        }


        public UserTracking TrackLogin(string username)
        {
            return _db.FirstOrDefault<UserTracking>("select * from User_Tracking where UserName =@0", username);
        }


        public int GetLoginSequence()
        {
            return _db.ExecuteScalar<int>("select top 1 Tid from User_Tracking order by Tid desc");
        }


        public bool TerminateSession(string Username)
        {
            string sql = "delete from User_Tracking where UserName =@0";
            var actual = _db.Delete<UserTracking>(sql, Username);
            if(actual !=0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public LoginResponse UserLogout(LoginRequest param)
        {
            bool userLogout = TerminateSession(param.UserName);
            if (userLogout)
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.LogoutAccount.ToString());
                InsertAudit(param.UserName, Constants.ActionType.LogoutAccount.ToString(), "Logout", DateTime.Now, param.Computername, param.SystemIp);
                return new LoginResponse
                {
                    ResponseCode = "00",
                    ResponseMessage = "Logout successful"

                };
            }
            else
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.UserName, Constants.ActionType.LogoutAccount.ToString());
                return new LoginResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "Logout failed"
                };
            }
        }
    }
}
