using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NySoftland.Core.Log4;

namespace DZAFCPortal.Web.Client.Controls
{
    public partial class NavTitleBar : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var navigateId = Request["TopNavId"];
            try
            {
                if (navigateId != null)
                {

                    var item = new DZAFCPortal.Service.NavigateService().GenericService.GetModel(navigateId);
                    literalNavTitle.Text = item.Title;
                    imgUrl.ImageUrl = item.IconUrl;
                }
            }
            catch (Exception ex)
            {
                LogHelper.Error("站点地图初始化失败.ex:", ex);
            }
        }
    }
}