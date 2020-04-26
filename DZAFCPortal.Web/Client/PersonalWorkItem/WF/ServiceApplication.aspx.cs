using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DZAFCPortal.Entity;
using DZAFCPortal.Service;

namespace DZAFCPortal.Web.Client.PersonalWorkItem.WF
{
    public partial class ServiceApplication : System.Web.UI.Page
    {
      
        private NavigateService navService = new NavigateService();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                GetAndBind();
        }

        /// <summary>
        /// 获取并绑定数据源
        /// </summary>
        private void GetAndBind()
        {
            var parentNavId = Request.Params["CurNavId"];
            if (!string.IsNullOrEmpty(parentNavId))
            {


                Expression<Func<Navigator, bool>> predicate = n => n.Enabled && n.ParentID == parentNavId;
                Expression<Func<Navigator, int>> orderCondition = n => n.OrderNum;

                var source = NavigatorGeneration.GenerateNavSource(predicate, orderCondition);

                rpt_content.DataSource = source;
                rpt_content.DataBind();
            }
        }

        /// <summary>
        /// 格式化URL
        /// </summary>
        /// <returns></returns>
        public string FormatUrl(object url, object id, object parentId)
        {
            var ext = url.ToString();

            if (string.IsNullOrEmpty(ext))
            {
                var parentId_guid = parentId.ToString();

                var topId = navService.GenericService.FirstOrDefault(n => n.ID == parentId_guid).ParentID;

                return DZAFCPortal.Config.Base.ClientBasePath + "/Pages/NYDevelop.aspx" + "?TopNavId=" + topId.ToString() + "&CurNavId=" + parentId.ToString();
            }
            if (ext.Contains("?")) ext += "&CurNavId=" + id.ToString();
            else ext += "?CurNavId=" + id.ToString();
            return DZAFCPortal.Config.Base.ClientBasePath + ext;
        }
    }
}