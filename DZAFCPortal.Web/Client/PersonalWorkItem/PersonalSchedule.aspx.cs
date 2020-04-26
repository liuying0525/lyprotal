using DZAFCPortal.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Client.PersonalWorkItem
{
    public partial class PersonalSchedule : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            hfDomainAccount.Value = DZAFCPortal.Config.AppSettings.DefautDomainName + "\\" + UserInfo.Account;
        }
    }
}