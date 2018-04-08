using DAL.Interfaces;
using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BLL.ApplicationLogic
{
    public class InstitutionManagement:ICompany
    {
        private readonly DolphinDb _db = DolphinDb.GetInstance();
        public List<DolCompany> GetCompanyById()
        {
            var actual = _db.Fetch<DolCompany>().ToList();
            return actual;
        }

        public List<DolCompany> ExcludeCompany(string company)
        {
            string sql = "Select * from DolCompany where Title <> @0 ";
            var actual = _db.Fetch<DolCompany>(sql,company).ToList();
            return actual;
        }

        public DolCompany GetCompanyById(int? Id)
        {
            string sql = "Select * from DolCompany where Id =@0";
            var actual = _db.FirstOrDefault<DolCompany>(sql, Id);
            return actual;

        }

        public DolCompany GetCompanyByName(string CustomerName)
        {
            string SQL = "Select * from DolCompany where Title =@0";
            var actual = _db.FirstOrDefault<DolCompany>(SQL, CustomerName);
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
            pic.SaveAs(HttpContext.Current.Server.MapPath("~/Content/CompanyBanner/") + result);
            return result;
        }


        public bool InsertCompany(DolCompany Title)
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

        public bool UpdateCompany(string Title, string Alias, string Banner, bool? Status, string RespTime, string RestTime, string Rest1Time, int? Id)
        {
            try
            {
                var _client = _db.SingleOrDefault<DolCompany>("WHERE Id=@0", Id);
                _client.Title = Title;
                _client.Alias = Alias;
                _client.Status = Status;
                _client.Banner = RespTime;
                _client.Resptime = RespTime;
                _client.Resttime = RestTime;
                _client.Rest1time = Rest1Time;
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
