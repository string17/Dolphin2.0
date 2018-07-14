using DataAccess.CustomObjects;
using DataAccess.Request;
using DataAccess.Response;
using DolphinContext.Data.Models;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BusinessLogic
{
    public class ClientManagement
    {
        private readonly DolphinDb _db = DolphinDb.GetInstance();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly AuditManagement _audit = new AuditManagement();


        //Get list of clients
        public List<ClientDetailsObj> GetClientList()
        {
            return _db.Fetch<ClientDetailsObj>().ToList();
        }


        //Exclude client
        public List<ClientDetailsObj> ExcludeClient(string ClientName)
        {
            return _db.Fetch<ClientDetailsObj>("Select * from Dol_Client where ClientName <> @0 ", ClientName).ToList();
        }


        public ClientDetailsObj GetClientDetailsById(int? ClientId)
        {
            return _db.FirstOrDefault<ClientDetailsObj>("Select * from Dol_Client where ClientId =@0", ClientId);
        }



        public ClientResponse ClientDetails(ClientDetailsObj request)
        {
            var success = GetClientDetailsById(request.ClientId);
            if (success != null)
            {
                var client = new List<ClientDetailsObj>
                {
                    success
                };

                return new ClientResponse
                {
                    ResponseCode = "00",
                    ResponseMessage = "Success",
                    ClientDetails = client
                };
            }
            else
            {
                return new ClientResponse
                {
                    ResponseCode = "04",
                    ResponseMessage = "Failure",
                    ClientDetails = new List<ClientDetailsObj>()
                };
            }
        }


        public DolClient GetClientByName(string ClientName)
        {
            return _db.FirstOrDefault<DolClient>("Select * from Dol_Client where ClientName =@0", ClientName);
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


        public bool InsertClient(ClientRequest request)
        {
            try
            {
                _db.Insert(request);
                return true;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("InsertClient", ex.Message);
                return false;
            }

        }

        public bool UpdateClient(ClientRequest request)
        {
            try
            {
                var client = _db.SingleOrDefault<DolClient>("WHERE ClientId=@0", request.ClientId);
                client.Clientname = request.ClientName;
                client.Clientalias = request.ClientAlias;
                client.Isclientactive = request.IsClientActive;
                client.Clientbanner = request.ClientBanner;
                client.Resptimein = request.RespTimeIn;
                client.Resttimein = request.RestTimeIn;
                client.Resptimeout = request.RespTimeOut;
                client.Resttimeout = request.RestTimeOut;
                _db.Update(client);
                return true;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("UpdateClient", ex);
                return false;
            }
        }


        public ClientResponse ListOnlyCustomerRole()
        {
            string ClientName = "Altaviz";
            var success = ExcludeClient(ClientName);
            if (success == null)
            {
                return new ClientResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "Failure",
                    ClientDetails = new List<ClientDetailsObj>()
                };
            }

            return new ClientResponse
            {
                ResponseCode = "00",
                ResponseMessage = "Success",
                ClientDetails = success
            };
        }


        public ClientResponse InsertClientDetails(ClientRequest param)
        {
            if (param.ClientName == string.Empty)
            {
                return new ClientResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "Kindly supply the name",
                    ClientDetails = new List<ClientDetailsObj>()
                };
            }
            var client = GetClientByName(param.ClientAlias.ToUpper());
            if (client != null)
            {
                    return new ClientResponse
                    {
                        ResponseCode = "02",
                        ResponseMessage = "Record already exist",
                         ClientDetails=new List<ClientDetailsObj>()
                    };
                }


                bool success = InsertClient(param);
                if (success)
                {
                    Log.InfoFormat(param.Computername, param.SystemIp, param.CreatedBy, Constants.ActionType.SetupClient.ToString());
                    _audit.InsertAudit(param.CreatedBy, Constants.ActionType.SetupClient.ToString(), "Setup client account", DateTime.Now, param.Computername, param.SystemIp);
                    return new ClientResponse
                    {
                        ResponseCode = "00",
                        ResponseMessage = "Record created successfully",
                        ClientDetails = new List<ClientDetailsObj>()
                    };
                }
            else
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.CreatedBy, Constants.ActionType.SetupClient.ToString());
                return new ClientResponse
                {
                    ResponseCode = "03",
                    ResponseMessage = "Unable to setup the record",
                    ClientDetails = new List<ClientDetailsObj>()
                };
            }
        }


        public ClientResponse ModifyClientDetails(ClientRequest param)
        {
            bool success = UpdateClient(param);
            if (success)
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.CreatedBy, Constants.ActionType.ModifyClientDetails.ToString());
                _audit.InsertAudit(param.CreatedBy, Constants.ActionType.ModifyClientDetails.ToString(), "Client detail changed", DateTime.Now, param.Computername, param.SystemIp);
                return new ClientResponse
                {
                    ResponseCode = "00",
                    ResponseMessage = "record updated successfully",
                    ClientDetails=new List<ClientDetailsObj>()
                };
            }
            else
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.CreatedBy, Constants.ActionType.ModifyClientDetails.ToString());
                return new ClientResponse
                {
                    ResponseCode = "04",
                    ResponseMessage = "Unable to update record",
                    ClientDetails = new List<ClientDetailsObj>()
                };
            }

        }

        public ClientResponse ListAllClient()
        {
            var success = GetClientList();
            if (success == null)
            {
                return new ClientResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "No record found",
                    ClientDetails = new List<ClientDetailsObj>()
                };
            }
            else
            {
                return new ClientResponse
                {
                    ResponseCode = "00",
                    ResponseMessage = "Success",
                    ClientDetails = new List<ClientDetailsObj>()
                };
            }
        }
    }
}
