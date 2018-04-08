using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL.Interfaces
{
    public interface ICompany
    {
        bool UpdateCompany(string Title, string Alias, string Banner, bool? Status, string RespTime, string RestTime, string Rest1Time, int? Id);
        List<DolCompany> ExcludeCompany(string company);
        DolCompany GetCompanyById(int? Id);
        DolCompany GetCompanyByName(string CustomerName);
        string DoFileUpload(HttpPostedFileBase pic, string filename = "");
        bool InsertCompany(DolCompany Title);
 
    }
}
