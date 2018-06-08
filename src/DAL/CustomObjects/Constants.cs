using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.CustomObjects
{
    public class Constants
    {
        public enum ActionType
        {
            Login,
            GetUserInfo,
            ResetPassword,
            ForgotPassword,
            SetupClient,
            SetupUserRole,
            SetupUserAccount,
            BulkUserRecordUpload,
            BulkTerminalUpload,
            ModifyClientDetails,
            ModifyUserDetails,
            ModifyPersonalProfile,
            LockAccount,
            SetUpTerminal,
            ModifyTerminalDetails,
            CreateIncident,
            UpdateIncident,
            CloseIncident,
            CreateBrand,
            ModifyBrandDetails,
            ModifyUserRole,
            ModifyBrandDeatails,
            AssignRoleMenu,
            ModifyRoleMenu,
            LogoutAccount

        }
    }
}
