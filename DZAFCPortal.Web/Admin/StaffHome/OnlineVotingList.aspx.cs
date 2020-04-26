using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using NySoftland.Core.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.StaffHome
{
    public partial class OnlineVotingList : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LiteralSiteMap.Text = Utils.GenerateTitle(Request.Url.AbsoluteUri);
                GetDataAndBind();
                DZAFCPortal.Authorization.Web.RepeaterHelper.RepeaterAuthorization(rptPrefer);
            }
        }
        #region--------Service 私有变量---------
        OnlineVoteService onlineService = new OnlineVoteService();
        OnlineVoteRecordsService adminService = new OnlineVoteRecordsService();
        #endregion

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
                var ee = onlineService.GenericService.GetModel(id);
                onlineService.GenericService.Delete(ee);
                onlineService.GenericService.Save();

                adminService.GenericService.Delete(p => p.NY_OnlineVoteAdminID == id);
                adminService.GenericService.Save();
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
            System.Linq.Expressions.Expression<Func<DZAFCPortal.Entity.OnlineVote, bool>> pre = p => true;
            int count = 0;
            var prefer = onlineService.GenericService.GetListByPage(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out count, pre, new OrderByMultiCondition<OnlineVote, DateTime>(c => c.BeginTime, true));
            if (count == 0)
            {
                Prompt.Visible = true;
                AspNetPager1.Visible = false;
            }
            AspNetPager1.RecordCount = count;
            rptPrefer.DataSource = prefer.ToList();
            rptPrefer.DataBind();
        }



        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            GetDataAndBind();
        }
    }
}