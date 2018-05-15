using DAL.CustomObjects;
using DolphinContext.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ApplicationLogic
{
    public class BrandManagement
    {
        private readonly DolphinDb _db = DolphinDb.GetInstance();
        public List<DolBrand> GetBrandById()
        {
            var actual = _db.Fetch<DolBrand>().ToList();
            return actual;
        }

        public List<DolBrand> ExcludeBrand(string BrandName)
        {
            string sql = "Select * from Dol_Brand where BrandName <> @0 ";
            var actual = _db.Fetch<DolBrand>(sql, BrandName).ToList();
            return actual;
        }

        public BrandResp GetBrandDetails(int? BrandId)
        {
            string sql = "Select * from Dol_Brand where BrandId =@0";
            var actual = _db.FirstOrDefault<DolBrand>(sql, BrandId);
            if (actual != null)
            {
                return new BrandResp
                {
                    RespCode = "00",
                    RespMessage = "Success",
                    BrandName = actual.Brandname,
                    BrandDesc = actual.Branddesc,
                    IsBrandActive = actual.Isbrandactive,
                    BrandId = actual.Brandid
                    
                };
            }
            else
            {
                return new BrandResp
                {
                    RespCode = "04",
                    RespMessage = "Failure"
                };
            }
        }


        public DolClient GetBrandByName(string BrandName)
        {
            string SQL = "Select * from Dol_Brand where BrandName =@0";
            var actual = _db.FirstOrDefault<DolClient>(SQL, BrandName);
            return actual;
        }


        public bool InsertBrand(string BrandName, string BrandDesc, bool IsBrandActive, string CreatedBy, string SystemIp, string SystemName)
        {
            try
            {
                var brand = new DolBrand();
                brand.Brandname = BrandName;
                brand.Branddesc = BrandDesc;
                brand.Isbrandactive = IsBrandActive;
                brand.Createdby = CreatedBy;
                brand.Createdon = DateTime.Now;
                brand.Createdby = CreatedBy;
                _db.Insert(brand);
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public bool UpdateBrandDetails(string BrandName, string BrandDesc, bool IsBrandActive, string CreatedBy, int BrandId)
        {
            try
            {
                var brand = _db.SingleOrDefault<DolBrand>("WHERE BrandId=@0", BrandId);
                brand.Brandname = BrandName;
                brand.Branddesc = BrandDesc;
                brand.Isbrandactive = IsBrandActive;
                brand.Createdby = CreatedBy;
                brand.Createdon = DateTime.Now;
                brand.Createdby = CreatedBy;
                _db.Update(brand);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
