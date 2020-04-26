using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq.Expressions;

namespace DZAFCPortal.Web.Client.Controls
{
    public partial class SecendaryLeftNav : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                BindLeftNav();

        }

        #region Service Definition
        private NewsCategoryService categoryService = new NewsCategoryService();

        private NavigateService navService = new NavigateService();
        #endregion

        #region PageEvent Handle
        #endregion

        #region ResourceGeneration & Binding
        /// <summary>
        /// 获取左侧导航数据源
        /// </summary>
        /// <returns></returns>
        List<Navigator> GenerateLeftNavSource(string navId)
        {
            Expression<Func<Navigator, bool>> predicate = n => n.Enabled && n.ParentID == navId;
            Expression<Func<Navigator, int>> orderCondition = n => n.OrderNum;

            return NavigatorGeneration.GenerateNavSource(predicate, orderCondition);
        }

        /// <summary>
        /// 绑定左侧导航
        /// </summary>
        void BindLeftNav()
        {
            string topNavId = Request["TopNavId"];
            if (string.IsNullOrEmpty(topNavId))
                return;

            var source = GenerateLeftNavSource(topNavId);

            rptCategory.DataSource = source;
            rptCategory.DataBind();
        }
        #endregion

        #region Extension Method

        /// <summary>
        /// 获取导航跳转至二级分类页面的URL
        /// </summary>
        /// <param name="categoryId"></param>
        /// <param name="navigatorId"></param>
        /// <returns></returns>
        public string GetSecondaryUrl(string Id, string navigatorId, string Url)
        {
            //var baseUrl = DZAFCPortal.Config.Base.ClientBasePath + "/Pages/";
            ////var category = navService.GenericService.GetModel(new Guid(categoryId));

            //baseUrl += "SecondaryList.aspx";

            //baseUrl += "?NavigateId=" + navigatorId + "&ParentId=" + Id;

            //return baseUrl;

            var baseUrl = DZAFCPortal.Config.Base.ClientBasePath + Url;
            if (Url.Contains("CategoryCode"))
            {
                baseUrl += "&TopNavId=" + navigatorId + "&CurNavId=" + Id;
            }
            else if (Url.Contains("?"))
            {
                baseUrl += "&TopNavId=" + navigatorId + "&CurNavId=" + Id;
            }
            else
            {
                baseUrl += "?TopNavId=" + navigatorId + "&CurNavId=" + Id;
            }

            return baseUrl;
        }

        public string GetHtml(string Id, string navigatorId, string Url, bool isRedirectNewTab)
        {

            var url = GetSecondaryUrl(Id, navigatorId, Url);
            return isRedirectNewTab ? "" : string.Format(" onclick='location.href={0}'", url);
        }
        #endregion
    }
}