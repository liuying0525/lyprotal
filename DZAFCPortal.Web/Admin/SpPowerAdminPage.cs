using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DZAFCPortal.Web.Admin
{
    public class SpPowerAdminPage : System.Web.UI.Page
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var uName = DZAFCPortal.Utility.UserInfo.Account;

            if (!DZAFCPortal.Config.AppSettings.DefaultAccounts.Contains(uName))
            {
                Response.Write(String.Format("当前页面只有{0}账号有权限访问！", String.Join(",", DZAFCPortal.Config.AppSettings.DefaultAccounts)));
                Response.End();
            }
        }
    }
}