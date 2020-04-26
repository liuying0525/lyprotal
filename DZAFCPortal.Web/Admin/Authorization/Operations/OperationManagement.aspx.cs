using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Authorization.Operations
{
    public partial class OperationManagement : SpPowerAdminPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LiteralSiteMap.Text = Utils.GenerateTitle(Request.Url.AbsolutePath);
        }
    }
}