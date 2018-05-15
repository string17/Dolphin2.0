using DAL.CustomObjects;
using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.ApplicationLogic
{
    public class ClientManagement
    {
        private readonly DolphinDb _db = DolphinDb.GetInstance();

        //public List<DolClient> GetClientById()
        //{
        //    var actual = _db.Fetch<DolClient>().ToList();
        //    return actual;
        //}

        public List<DolClient> GetClientList()
        {
            var actual = _db.Fetch<DolClient>().ToList();
            return actual;
        }

        public List<DolClient> ExcludeClient(string ClientName)
        {
            string sql = "Select * from Dol_Client where ClientName <> @0 ";
            var actual = _db.Fetch<DolClient>(sql, ClientName).ToList();
            return actual;
        }

        public ClientResp GetClientDetails(int? ClientId)
        {
            string sql = "Select * from Dol_Client where ClientId =@0";
            var actual = _db.FirstOrDefault<DolClient>(sql, ClientId);
            if (actual != null)
            {
                return new ClientResp
                {
                    RespCode = "00",
                    RespMessage = "Success",
                    ClientId=actual.Clientid,
                    ClientBanner = actual.Clientbanner,
                    ClientAlias = actual.Clientalias,
                    ClientName = actual.Clientname,
                    RestTime = actual.Resttime,
                    RespTime = actual.Resptime,
                    IsClientActive = actual.Isclientactive,
                    CreatedBy = actual.Createdby,
                    CreatedOn = actual.Createdon
                };
            }
            else
            {
                return new ClientResp
                {
                    RespCode = "04",
                    RespMessage = "Failure"
                };
            }
         }


        public DolClient GetClientByName(string ClientName)
        {
            string SQL = "Select * from Dol_Client where ClientName =@0";
            var actual = _db.FirstOrDefault<DolClient>(SQL, ClientName);
            return actual;
        }


        public string DoFileUpload(HttpPostedFileBase pic, string filename = "")
        {
            if (pic == null && string.IsNullOrWhiteSpace(filename))
            {
                return "";
            }
            if (!string.IsNullOrWhiteSpace(filename) && pic == null) return filename;

            string result = DateTime.Now.Millisecond + "Emblem.jpg";
            pic.SaveAs(HttpContext.Current.Server.MapPath("~/Content/Banner/") + result);
            return result;
        }


        public bool InsertClient(string ClientName, string ClientAlias, string ClientBanner, int RespTime, int RestTime, bool IsClientActive,string CreatedBy)
        {
            try
            {
                var client = new DolClient();
                client.Clientname = ClientName;
                client.Clientalias = ClientAlias;
                client.Resptime = RespTime;
                client.Resttime = RestTime;
                client.Isclientactive = IsClientActive;
                client.Createdon = DateTime.Now;
                client.Createdby = CreatedBy;
                _db.Insert(client);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool UpdateClient(string ClientName, string ClientAlias, HttpPostedFileBase ClientBanner, int RespTime, int RestTime, bool IsClientActive, string ExtClientBanner, int ClientId)
        {
            try
            {
                var client = _db.SingleOrDefault<DolClient>("WHERE ClientId=@0", ClientId);
                client.Clientname = ClientName;
                client.Clientalias = ClientAlias;
                client.Isclientactive = IsClientActive;
                client.Clientbanner =DoFileUpload(ClientBanner, ExtClientBanner);
                client.Resptime = RespTime;
                client.Resttime = RestTime;
                _db.Update(client);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
