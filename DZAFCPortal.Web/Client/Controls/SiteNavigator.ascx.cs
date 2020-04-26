using DZAFCPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Client.Controls
{
    public partial class SiteNavigator : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BindFields();
        }

        #region Service Definition
        //private NY_IndexNavigatorService navService = new NY_IndexNavigatorService();
        private NavigateService navService = new NavigateService();
        private NewsCategoryService categoryService = new NewsCategoryService();
        #endregion

        #region PageEvent Handle
        #endregion

        #region ResourceGeneration & Binding
        /// <summary>
        /// 绑定站点导航字段
        /// </summary>
        void BindFields()
        {
            string navigateId = Request["TopNavId"] == null ? string.Empty : Request["TopNavId"];
            string categoryId = Request["CurNavId"] == null ? string.Empty : Request["CurNavId"];
            if (string.IsNullOrEmpty(navigateId) || string.IsNullOrEmpty(categoryId))
                return;
            var nav = navService.GenericService.GetModel(navigateId);

            var home = nav.Title;
            lblHomeTitle.Text = "&gt;&nbsp;" + home;


            if (string.IsNullOrEmpty(categoryId))
                return;
            var category = navService.GenericService.GetModel(categoryId);
            var second = category != null ? category.Title : "";

            lblSecondaryTitle.Text = "&gt;&nbsp;" + second;
        }

        #endregion

        #region Extension Method
        #endregion
    }
}