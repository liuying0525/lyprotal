
using DZAFCPortal.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;

namespace DZAFCPortal.Authorization.Web
{
    public class PortalPageBase : Page 
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            string val = PageAuthorization.ProcessAuthorization(Request.Url.PathAndQuery);
            if (val != "true")
            {
                Response.Write(val);
                Response.End();
            }
            try
            {
                new ControlHelper(UserInfo.Account).ControlAuthorization(Page.Controls);
            }
            catch (Exception ex)
            {
                Response.Write("<h2>" + ex.Message + "</h2>");
                Response.End();
            }
        }
    }
}
