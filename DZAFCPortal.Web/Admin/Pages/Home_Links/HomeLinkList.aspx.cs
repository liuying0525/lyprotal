using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Pages.Home_Links
{
    public partial class HomeLinkList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindLinkTypeDrop();
                string id = Request["type"];
                GetDataAndBind(id);
                LiteralSiteMap.Text = Utils.GenerateTitle(Request.Url.AbsoluteUri);
                //控制权限
               //DZAFCPortal.Authorization.Web.RepeaterHelper.RepeaterAuthorization(rptLink);

            }
        }

        #region--------Service 私有变量---------
        CommonLinkService linkSerivce = new CommonLinkService();
    
        #endregion

        #region--------页面控件的响应事件-----------
        /// <summary>
        /// 执行列表控件执行Command
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptLink_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var cmdName = e.CommandName.Trim();

            switch (cmdName)
            {
                case "Delete": Delete(e.CommandArgument.ToString()); break;
            }
        }

        protected void rptLink_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var dataItem = e.Item.DataItem as CommonLink;
                if (!String.IsNullOrEmpty(dataItem.Icon))
                {
                    //图标
                    Literal ltrIcon = e.Item.FindControl("ltrIcon") as Literal;
                    ltrIcon.Text = String.Format("<img src='{0}' style=' width:{1};height:{2}'></img>", dataItem.Icon,
                        63, 63);
                }
            }
        }

        protected void dropLinkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetDataAndBind("");
        }

        /// <summary>
        /// 执行分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            GetDataAndBind("");
        }
        #endregion

        #region------------私有方法----------

        private void Delete(string id)
        {
            linkSerivce.GenericService.Delete(id);
            linkSerivce.GenericService.Save();

            //删除后重新绑定数据
            GetDataAndBind("");
        }

        /// <summary>
        /// 获取数据，并绑定到列表中
        /// </summary>
        private void GetDataAndBind(string id)
        {
            //设置默认的查询条件
            Expression<Func<CommonLink, bool>> predicate = p => true;
            //   int count = 0;
            //var links = linkSerivce.GetListByPage(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out count, predicate, p => p.OrderNum, false).ToList();

            var links = linkSerivce.GenericService.GetAll(predicate).OrderBy(p => p.OrderNum).ToList();
            rptLink.DataSource = links;
            rptLink.DataBind();

            //设置页数
            //AspNetPager1.RecordCount = count;
        }


        /// <summary>
        /// 绑定分类下拉
        /// 下拉的 Value 为 ID 和是否可选组合
        /// 项以 , 隔开
        /// 例如：guid(值),1  :其中1表示可用  0：则表示禁用
        /// </summary>
        private void BindLinkTypeDrop()
        {
         
        }
        #endregion
    }
}