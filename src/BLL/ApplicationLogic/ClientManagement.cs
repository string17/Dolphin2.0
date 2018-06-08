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


        //Get list of clients
        public List<DolClient> GetClientList()
        {
            var actual = _db.Fetch<DolClient>().ToList();
            return actual;
        }


        //Exclude client
        public List<ClientObj> ExcludeClient(string ClientName)
        {
            string sql = "Select * from Dol_Client where ClientName <> @0 ";
            var clients = _db.Fetch<ClientObj>(sql, ClientName).ToList();
            return clients;
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
                    RespTimeUp=actual.Resptimeup,
                    RestTimeUp=actual.Resttimeup,
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


        public bool InsertClient(string ClientName, string ClientAlias, string ClientBanner, int RespTime, int RestTime, int RespTimeUp, int RestTimeUp, bool IsClientActive,string CreatedBy)
        {
            try
            {
                var client = new DolClient();
                client.Clientname = ClientName;
                client.Clientalias = ClientAlias;
                client.Resptime = RespTime;
                client.Resttime = RestTime;
                client.Resptimeup = RespTimeUp;
                client.Resttimeup = RestTimeUp;
                client.Isclientactive = IsClientActive;
                client.Clientbanner = ClientBanner;
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

        public bool UpdateClient(string ClientName, string ClientAlias, string ClientBanner, int RespTime, int RestTime, int RespTimeUp, int RestTimeUp, bool IsClientActive, int ClientId)
        {
            try
            {
                var client = _db.SingleOrDefault<DolClient>("WHERE ClientId=@0", ClientId);
                client.Clientname = ClientName;
                client.Clientalias = ClientAlias;
                client.Isclientactive = IsClientActive;
                client.Clientbanner = ClientBanner;
                client.Resptime = RespTime;
                client.Resttime = RestTime;
                client.Resptimeup = RespTimeUp;
                client.Resttimeup = RestTimeUp;
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
