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

namespace BusinessLogic
{
    public class TerminalManagement
    {
        private readonly DolphinDb _db = DolphinDb.GetInstance();
        private static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);
        private readonly AuditManagement _audit = new AuditManagement();
        private readonly BrandManagement _brand = new BrandManagement();
        private readonly ClientManagement _client = new ClientManagement();
        private readonly UserManagement _user = new UserManagement();


        //Get terminal details
        public List<TerminalDetailsObj> GetTerminalDetails(int? TerminalId)
        {
            var terminal = _db.SingleOrDefault<DolTerminal>("where TerminalId=@0", TerminalId);
            var state = _db.FirstOrDefault<DolState>("where StateId=@0", terminal.Stateid);
            var region = _db.FirstOrDefault<DolRegion>("where RegionId=@0", state.Regionid);
            var brand = _db.FirstOrDefault<DolBrand>("where BrandId=@0", terminal.Brandid);
            var user = _db.FirstOrDefault<DolUser>("where UserName=@0", terminal.Engineer);
            var client = _db.FirstOrDefault<DolClient>("where ClientId=@0", terminal.Clientid);

            var param = new TerminalDetailsObj
            {
                TerminalId = terminal.Terminalid,
                TerminalNo = terminal.Terminalno,
                TerminalRef = terminal.Terminalref,
                BrandId = terminal.Brandid,
                BrandName = brand.Brandname,
                ClientId = terminal.Clientid,
                ClientName = client.Clientname,
                Engineer = terminal.Engineer,
                FirstName = user.Firstname,
                MiddleName = user.Middlename,
                LastName = user.Lastname,
                IsTerminalActive = terminal.Isterminalactive,
                IsUnderSupport = terminal.Isundersupport,
                Location = terminal.Location,
                RegionTitle = region.Regiontitle,
                SerialNo = terminal.Serialno,
                StateTitle = state.Statetitle,
                StateId = terminal.Stateid,
                TerminalAlias = terminal.Terminalalias,
                CreatedBy = terminal.Createdby,
                CreatedOn = terminal.Createdon
            };
            var response = new List<TerminalDetailsObj>
            {
                param
            };

            return response;
        }

        //Get all terminals
        public List<TerminalDetailsObj> GetAllTerminals()
        {
            string sql = "select A.*,C.RegionTitle,E.ClientAlias from Dol_Terminal A inner join Dol_State B on A.StateId=B.StateId inner join Dol_Region C on B.RegionId=C.RegionId inner join Dol_Brand D on A.BrandId=D.BrandId inner join Dol_Client E on A.ClientId=E.ClientId";
            var actual = _db.Fetch<TerminalDetailsObj>(sql).ToList();
            return actual;
        }


        // Get terminals sorted by client
        public List<TerminalResponse> GetTerminalByClient(string ClientAlias)
        {
            var client = _db.SingleOrDefault<DolClient>("where ClientAlias=@0", ClientAlias);
            var terminal = _db.Fetch<TerminalResponse>("select A.*,B.StateTitle,C.RegionTitle from Dol_Terminal A inner join Dol_State B on A.StateId=B.StateId inner join Dol_Region C on C.RegionId=B.StateId where A.ClientId=@0", client.Clientid).ToList();
            return terminal;
        }


        // Get terminals sorted by TerminalNo
        public TerminalResponse GetTerminalByNo(string TerminalNo)
        {
            var terminal = _db.FirstOrDefault<TerminalResponse>("select A.*,B.StateTitle,C.RegionTitle from Dol_Terminal A inner join Dol_State B on A.StateId=B.StateId inner join Dol_Region C on C.RegionId=B.StateId where A.TerminalNo=@0", TerminalNo);
            return terminal;
        }


        // Get states
        public List<StateDetailsObj> GetAllStates()
        {
           return _db.Fetch<StateDetailsObj>("select * from Dol_State order by StateTitle ASC");
        }

        // Get states
        public StateDetailsObj GetStateByName(string Name)
        {
            return _db.SingleOrDefault<StateDetailsObj>("select * from Dol_State where StateTitle=@0", Name);
        }

        public bool InsertBulkTerminal(List<TerminalRequest> param)
        {
            try
            {
                var terminal = new DolTerminal();
                _db.Insert(terminal);
                return true;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("InsertBulkRequest", ex.Message);
                return false;
            }
        }

        //Insert terminal
        public bool InsertTerminal(TerminalRequest param)
        {
            try
            {
                var terminal = new DolTerminal();
                terminal.Terminalno = param.TerminalNo;
                terminal.Terminalref = param.TerminalRef;
                terminal.Brandid = param.BrandId;
                terminal.Clientid = param.ClientId;
                terminal.Engineer = param.Engineer;
                terminal.Isterminalactive = param.IsTerminalActive;
                terminal.Isundersupport = param.IsUnderSupport;
                terminal.Location = param.Location;
                terminal.Serialno = param.SerialNo;
                terminal.Stateid = param.StateId;
                terminal.Terminalalias = param.TerminalAlias;
                terminal.Createdby = param.CreatedBy;
                terminal.Createdon = param.CreatedOn;
                _db.Insert(terminal);
                return true;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("InsertTerminalRequest", ex.Message);
                return false;
            }
        }


        //update terminal
        public bool ModifyTerminal(TerminalRequest param)
        {
            try
            {
                var terminal = _db.SingleOrDefault<DolTerminal>("where TerminalId=@0", param.TerminalId);
                terminal.Terminalno = param.TerminalNo;
                terminal.Terminalref = param.TerminalRef;
                terminal.Brandid = param.BrandId;
                terminal.Clientid = param.ClientId;
                terminal.Engineer = param.Engineer;
                terminal.Isterminalactive = param.IsTerminalActive;
                terminal.Isundersupport = param.IsUnderSupport;
                terminal.Location = param.Location;
                terminal.Serialno = param.SerialNo;
                terminal.Stateid = param.StateId;
                terminal.Terminalalias = param.TerminalAlias;
                terminal.Createdby = param.CreatedBy;
                terminal.Createdon = param.CreatedOn;
                _db.Update(terminal);
                return true;
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("ModifyTerminal", ex.Message);
                return false;
            }
        }


        public bool SupportStatus(string Status)
        {
            switch (Status)
            {
                case "ACTIVE":
                    return true;
                case "INACTIVE":
                    return false;
                default:
                    return false;
            }
        }



        public TerminalResponse ListTerminal()
        {
            var success = GetAllTerminals();
            if (success != null)
            {
                return new TerminalResponse
                {
                    ResponseCode = "00",
                    ResponseMessage = "Success",
                    TerminalDetails = success
                };
            }
            else
            {
                return new TerminalResponse
                {
                    ResponseCode = "00",
                    ResponseMessage = "Failure",
                    TerminalDetails = success
                };
            }
        }



        public TerminalResponse InsertTerminalDetails(TerminalRequest param)
        {
            if (param.TerminalNo == null)
            {
                return new TerminalResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "Empty Terminal Number",
                    TerminalDetails = new List<TerminalDetailsObj>()
                };
            }
            var terminal = GetTerminalByNo(param.TerminalNo);
            if (terminal != null)
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.CreatedBy, Constants.ActionType.SetUpTerminal.ToString());
                return new TerminalResponse
                {
                    ResponseCode = "04",
                    ResponseMessage = "Terminal already exist",
                    TerminalDetails = new List<TerminalDetailsObj>()
                };
            }

            try
            {
                bool success = InsertTerminal(param);
                if (success)
                {
                    Log.InfoFormat(param.Computername, param.SystemIp, param.CreatedBy, Constants.ActionType.SetUpTerminal.ToString());
                    _audit.InsertAudit(param.CreatedBy, Constants.ActionType.SetUpTerminal.ToString(), "Create terminal", DateTime.Now, param.Computername, param.SystemIp);
                    return new TerminalResponse
                    {
                        ResponseCode = "00",
                        ResponseMessage = "Record successfully added",
                        TerminalDetails = new List<TerminalDetailsObj>()
                    };
                }
                else
                {
                    Log.InfoFormat(param.Computername, param.SystemIp, param.CreatedBy, Constants.ActionType.SetUpTerminal.ToString());
                    return new TerminalResponse
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Unable to create record",
                        TerminalDetails = new List<TerminalDetailsObj>()
                    };
                }
            }
            catch(Exception ex)
            {
                Log.ErrorFormat(param.Computername, param.SystemIp, param.CreatedBy, Constants.ActionType.SetUpTerminal.ToString(), ex);
                return new TerminalResponse
                {
                    ResponseCode = "XX",
                    ResponseMessage = "System error",
                    TerminalDetails = new List<TerminalDetailsObj>()
                };
            }
        }


        public TerminalResponse TerminalDetails(TerminalRequest param)
        {
            if (param.TerminalId == 0)
            {
                Log.ErrorFormat(param.Computername, param.SystemIp, param.CreatedBy, Constants.ActionType.SetUpTerminal.ToString());
                return new TerminalResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "No Terminal Id",
                    TerminalDetails = new List<TerminalDetailsObj>()
                };
            }
            try
            {
                var success = GetTerminalDetails(param.TerminalId);
                if (success == null)
                {
                    Log.ErrorFormat(param.Computername, param.SystemIp, param.CreatedBy, Constants.ActionType.SetUpTerminal.ToString());
                    return new TerminalResponse
                    {
                        ResponseCode = "02",
                        ResponseMessage = "No record found",
                        TerminalDetails = new List<TerminalDetailsObj>()
                    };
                }

                Log.ErrorFormat(param.Computername, param.SystemIp, param.CreatedBy, Constants.ActionType.SetUpTerminal.ToString());
                return new TerminalResponse
                {
                    ResponseCode = "00",
                    ResponseMessage = "Success",
                    TerminalDetails = success
                };
            }
            catch(Exception ex)
            {
                Log.ErrorFormat(param.Computername, param.SystemIp, param.CreatedBy, Constants.ActionType.SetUpTerminal.ToString(), ex);
                return new TerminalResponse
                {
                    ResponseCode = "XX",
                    ResponseMessage = "System error",
                    TerminalDetails = new List<TerminalDetailsObj>()
                };
            }
        }


        public TerminalResponse UploadBulkTerminalRecords(List<TerminalBulkRequest> param)
        {
            int successful = 0;
            int failed = 0;
            int existing = 0;
            
            try
            {
                foreach (var n in param)
                {
                    var existTerminal = GetTerminalByNo(n.TerminalNo);
                    if (existTerminal == null)
                    {
                        var value = new TerminalRequest();
                        value.TerminalNo = n.TerminalNo;
                        value.TerminalRef = n.TerminalRef;
                        value.SerialNo = n.SerialNo;
                        value.BrandId = _brand.GetBrandByName(n.BrandName).Brandid;
                        value.ClientId = _client.GetClientByName(n.ClientName).Clientid;
                        value.Engineer = _user.GetUserByEmail(n.Engineer).Username;
                        value.IsUnderSupport = SupportStatus(n.Support.ToUpper());
                        value.IsTerminalActive = value.IsUnderSupport;
                        value.TerminalAlias = n.TerminalAlias;
                        value.StateId = GetStateByName(n.State.ToLower()).StateId;
                        value.Location = n.Location;
                        value.CreatedBy = n.CreatedBy;
                        value.CreatedOn = n.CreatedOn;
                        InsertTerminal(value);
                        successful++;
                    }
                    else
                    {
                        existing++;
                    }


                }
            }
            catch (Exception ex)
            {
                Log.InfoFormat(param.FirstOrDefault().Computername, param.FirstOrDefault().SystemIp, param.FirstOrDefault().CreatedBy, Constants.ActionType.SetUpTerminal.ToString());
                return new TerminalResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = ex.Message + " " + successful + " was added successfully",
                    TerminalDetails=new List<TerminalDetailsObj>()
                };
            }

            Log.InfoFormat(param.FirstOrDefault().Computername, param.FirstOrDefault().SystemIp, param.FirstOrDefault().CreatedBy, Constants.ActionType.SetUpTerminal.ToString());
            _audit.InsertAudit(param.FirstOrDefault().CreatedBy, Constants.ActionType.SetUpTerminal.ToString(), "Create terminal", DateTime.Now, param.FirstOrDefault().Computername, param.FirstOrDefault().SystemIp);
            return new TerminalResponse
            {
                ResponseCode = "00",
                ResponseMessage = successful + " Record successfully added, " + failed + " failed and " + existing + " already exists",
                TerminalDetails = new List<TerminalDetailsObj>()
            };
        }


        public TerminalResponse ModifyTerminalDetails(TerminalRequest param)
        {
            if (param.TerminalId==0)
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.CreatedBy, Constants.ActionType.ModifyTerminalDetails.ToString());
                return new TerminalResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "Record updated successfully",
                     TerminalDetails=new List<TerminalDetailsObj>()
                };
            }

            try
            {
                bool terminal = ModifyTerminal(param);
                if (terminal)
                {
                    Log.InfoFormat(param.Computername, param.SystemIp, param.CreatedBy, Constants.ActionType.ModifyTerminalDetails.ToString());
                    _audit.InsertAudit(param.CreatedBy, Constants.ActionType.ModifyTerminalDetails.ToString(), "TerminalDetails modified", DateTime.Now, param.Computername, param.SystemIp);
                    return new TerminalResponse
                    {
                        ResponseCode = "00",
                        ResponseMessage = "Record updated successfully",
                         TerminalDetails=new List<TerminalDetailsObj>()
                    };
                }
                else
                {
                    Log.InfoFormat(param.Computername, param.SystemIp, param.CreatedBy, Constants.ActionType.ModifyTerminalDetails.ToString());
                    return new TerminalResponse
                    {
                        ResponseCode = "01",
                        ResponseMessage = "Unable to update record",
                         TerminalDetails= new List<TerminalDetailsObj>()
                    };
                }
            }
            catch(Exception ex)
            {
                Log.InfoFormat(param.Computername, param.SystemIp, param.CreatedBy, Constants.ActionType.ModifyTerminalDetails.ToString(), ex);
                return new TerminalResponse
                {
                    ResponseCode = "XX",
                    ResponseMessage = "System error",
                    TerminalDetails = new List<TerminalDetailsObj>()
                };
            }
        }


        public StateResponse ListAvailableStates()
        {
            var success = GetAllStates();
            if (success == null)
            {
                return new StateResponse
                {
                    ResponseCode = "01",
                    ResponseMessage = "No data found",
                    StateDetails=new List<StateDetailsObj>()
                };
            }

            return new StateResponse
            {
                 ResponseCode = "00",
                ResponseMessage = "Success",
                StateDetails = new List<StateDetailsObj>()
            };
        }
    }
}
