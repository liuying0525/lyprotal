using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Client.Controls
{
    public partial class TopTitleBar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var curNavId = Request["CurNavId"];
            if (curNavId != null)
            {
                var item = new DZAFCPortal.Service.NavigateService().GenericService.GetModel(curNavId);
                CategoryTitle.Text = item.Title + " " + item.EnglishName;
            }
        }
    }
}