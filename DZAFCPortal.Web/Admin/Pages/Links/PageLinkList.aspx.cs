using DZAFCPortal.Config.Enums;
using DZAFCPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Pages.Links
{
    public partial class PageLinkList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LiteralSiteMap.Text = Utils.GenerateTitle(Request.Url.AbsoluteUri);
                GetDataAndBind();
            }
        }
        #region--------Service 私有变量---------
        IndexScrollService linkSerivce = new IndexScrollService();
        #endregion

        /// <summary>
        /// 执行列表控件执行Command
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptEFamil_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var cmdName = e.CommandName.Trim();

            switch (cmdName)
            {
                //case "Edit": ShowEditPage(e.CommandArgument.ToString()); break;
                case "Delete": Delete(e.CommandArgument.ToString()); break;
            }
        }
        private void Delete(string id)
        {
            try
            {
                var ee = linkSerivce.GenericService.GetModel(id);
                linkSerivce.GenericService.Delete(ee);
                linkSerivce.GenericService.Save();

                //删除后重新绑定数据
                GetDataAndBind();
            }
            catch (Exception ex)
            {
                Fxm.Utility.Page.MessageBox.Show("删除过程发生错误，请重试!");

                //记录错误日志
                NySoftland.Core.Log4.LogHelper.Error("删除优惠活动[PreferentActionList.aspx]", ex);
            }
        }

        /// <summary>
        /// 获取数据，并绑定到列表中
        /// </summary>
        private void GetDataAndBind()
        {
            int type = int.Parse(Request["type"]);
            //System.Linq.Expressions.Expression<Func<DZAFCPortal.Entity.PageLink, bool>> es = p => p.PageLinkType == type;
            //int count = 0;
            //var prefer = linkSerivce.GenericService.GetListByPage(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out count, es, p => p.Name, true);
            //if (count == 0)
            //{
            //    Prompt.Visible = true;
            //    AspNetPager1.Visible = false;
            //}
            //AspNetPager1.RecordCount = count;
            // txtTitle.Text = title(type);
            //txtTitle.Text = ((PageLinkTypeEnum)type).ToString();


            var prefer = linkSerivce.GenericService.GetAll().Where(p => p.PageLinkType == type).OrderBy(p => p.OrderNum).ToList();
            rptEFamil.DataSource = prefer.ToList();
            rptEFamil.DataBind();
        }

        //private string title(int type)
        //{
        //    string title = "";
        //    switch (type)
        //    {
        //        case 100:
        //            title = PageLinkTypeEnum.办公平台系统.ToString();
        //            break;
        //        case 200:
        //            title = PageLinkTypeEnum.首页滚动图.ToString();
        //            break;
        //        case 300:
        //            title = PageLinkTypeEnum.外部关联网站.ToString();
        //            break;
        //    }
        //    return title;
        //}

        public string ImgSize()
        {
            int type = int.Parse(Request["type"]);
            string size = "";
            if (type == 200)
            {
                size = "width='194' height='92'";
            }
            else
            {
                size = "width='100' height='100'";
            }
            return size;
        }


        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        //{
        //    GetDataAndBind();
        //}


    }
}