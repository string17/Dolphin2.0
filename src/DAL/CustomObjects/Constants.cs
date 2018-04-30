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
            CreateRole,
            CreateUser,
            ModifyClientDetails,
            ModifyUserDetails,
            ModifyPersonalProfile,
            LockAccount,
            Logout

        }
    }
}
