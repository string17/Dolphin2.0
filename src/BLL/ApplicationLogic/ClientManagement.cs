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
        public List<DolClient> GetCompanyById()
        {
            var actual = _db.Fetch<DolClient>().ToList();
            return actual;
        }

        public List<DolClient> ExcludeCompany(string company)
        {
            string sql = "Select * from DolCompany where Title <> @0 ";
            var actual = _db.Fetch<DolClient>(sql,company).ToList();
            return actual;
        }

        public DolClient GetCompanyById(int? Id)
        {
            string sql = "Select * from DolCompany where Id =@0";
            var actual = _db.FirstOrDefault<DolClient>(sql, Id);
            return actual;

        }

        public DolClient GetCompanyByName(string CustomerName)
        {
            string SQL = "Select * from DolCompany where Title =@0";
            var actual = _db.FirstOrDefault<DolClient>(SQL, CustomerName);
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


        public bool InsertClient(DolClient Title)
        {
            try
            {
                _db.Insert(Title);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool UpdateClient(ExtClientObj client)
        {
            try
            {
                var _client = _db.SingleOrDefault<DolClient>("WHERE Id=@0", client.ClientId);
                _client.Clientname = client.ClientName;
                _client.Clientalias = client.ClientAlias;
                _client.Isclientactive = client.IsClientActive;
                _client.Clientbanner = client.ClientBanner;
                _client.Resptime = client.RespTime;
                _client.Resttime = client.RestTime;
                _db.Update(_client);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
