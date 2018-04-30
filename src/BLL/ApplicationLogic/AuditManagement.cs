using DAL.CustomObjects;
using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.ApplicationLogic
{
    public class AuditManagement
    {
        private readonly DolphinDb _db = DolphinDb.GetInstance();

        
        public bool InsertAudit(string UserName,string UserActivity,string Comment, DateTime DateLog,string SystemName,string SystemIp)
        {
            try
            {
                var audit = new AuditTrail();
                audit.Username = UserName;
                audit.Useractivity = UserActivity;
                audit.Comment = Comment;
                audit.Datelog = DateLog;
                audit.Systemname = SystemName;
                audit.Systemip = SystemIp;
                _db.Insert<AuditTrail>(audit);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
           
        }

        public List<AuditObj> GetAuditById()
        {
            string sql = "select * from Audit_Trail order by AuditId desc";
            var actual = _db.Fetch<AuditObj>(sql);
            return actual;
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
                return false;
            }
        }


        public UserTracking TrackLogin(string username)
        {
            string sql = "select * from User_Tracking where UserName =@0";
            var actual = _db.FirstOrDefault<UserTracking>(sql, username);
            return actual;
        }


        public int GetLoginSequence()
        {
            string sql = "select top 1 Tid from User_Tracking order by Tid desc";
            int lastSequence = _db.ExecuteScalar<int>(sql);
            
            return lastSequence;
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
    }
}
