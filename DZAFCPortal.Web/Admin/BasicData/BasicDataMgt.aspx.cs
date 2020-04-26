using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.BasicData
{
    public partial class BasicDataMgt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
                LiteralSiteMap.Text = Utils.GenerateTitle(Request.Url.AbsoluteUri);
        }
        public string SYSTEM_STARUP_TIME = System.DateTime.Now.ToString();
    }
}