using DAL.Response;
using DolphinContext.Data.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BLL.ApplicationLogic
{
    public class BrandManagement
    {
        private readonly DolphinDb _db = DolphinDb.GetInstance();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);


        public List<DolBrand> GetBrandById()
        {
            return _db.Fetch<DolBrand>().ToList();
        }

        public List<DolBrand> ExcludeBrand(string BrandName)
        {
            return _db.Fetch<DolBrand>("Select * from Dol_Brand where BrandName <> @0 ", BrandName).ToList();
        }

        public BrandDetailsObj GetBrandDetails(int? BrandId)
        {
            string sql = "Select * from Dol_Brand where BrandId =@0";
            return _db.FirstOrDefault<BrandDetailsObj>(sql, BrandId);
   
        }


        public DolBrand GetBrandByName(string BrandName)
        {
            return _db.FirstOrDefault<DolBrand>("Select * from Dol_Brand where BrandName =@0", BrandName);
        }


        public List<BrandDetailsObj> GetAllBrands()
        {
            return _db.Fetch<BrandDetailsObj>("Select * from Dol_Brand").ToList();
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
            catch (Exception ex)
            {
                Log.ErrorFormat("InsertBrandDetails", ex);
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
            catch (Exception ex)
            {
                Log.ErrorFormat("UpdateBrandDetails", ex);
                return false;
            }
        }

        public BrandResponse ListAllAvailableBrands()
        {
            var success = GetAllBrands();
            if (success == null)
            {
                return new BrandResponse
                {
                    ResponseCode = "00",
                    ResponseMessage = "No data found",
                    BrandDetails = new List<BrandDetailsObj>()
                };
            }

            return new BrandResponse
            {
                ResponseCode = "00",
                ResponseMessage = "Success",
                BrandDetails = success
            };
        }
    }
}
