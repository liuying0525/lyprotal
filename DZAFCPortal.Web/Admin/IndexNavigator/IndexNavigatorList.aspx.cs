using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NYPortal.Web.Admin.IndexNavigator
{
    public partial class IndexNavigatorList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetDataAndBind();
                LiteralSiteMap.Text = Utils.GenerateTitle(Request.Url.AbsolutePath);
            }
        }

        #region--------Service 私有变量---------

        NY_IndexNavigatorService navService = new NY_IndexNavigatorService();

        #endregion

        #region--------页面控件的响应事件-----------


        /// <summary>
        /// 执行列表控件执行Command
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptSlider_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var cmdName = e.CommandName.Trim();

            switch (cmdName)
            {
                //case "Edit": ShowEditPage(e.CommandArgument.ToString()); break;
                case "Delete": Delete(e.CommandArgument.ToString()); break;
            }
        }
        #endregion

        #region------------私有方法----------
        private void Delete(string id)
        {
            navService.GenericService.Delete(id);
            navService.GenericService.Save();

            //删除后重新绑定数据
            GetDataAndBind();
        }

        /// <summary>
        /// 获取数据，并绑定到列表中
        /// </summary>
        private void GetDataAndBind()
        {
            var navs = navService.GenericService.GetAll().OrderBy(p => p.OrderNum).ToList();

            rptSlider.DataSource = navs;
            rptSlider.DataBind();
        }

        #endregion
    }
}