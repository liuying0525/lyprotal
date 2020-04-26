using DZAFCPortal.Entity;
using DZAFCPortal.Facade;
using DZAFCPortal.Service;
using NySoftland.Core.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Client.StaffHome
{
    public partial class OnlineVotingList : AuthrizedBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetDataAndBind();
                GetDataAndBindall();
            }
        }

        #region--------Service 私有变量---------
        OnlineVoteService onlineService = new OnlineVoteService();
        OnlineVoteOptionsService optionService = new OnlineVoteOptionsService();
        StatusFacade nf = new StatusFacade();
        private string Sum = "";
        #endregion
        /// <summary>
        /// 获取数据，并绑定到列表中
        /// </summary>
        private void GetDataAndBind()
        {
            //var option = optionService.GenericService.GetAll().OrderByDescending(p => p.CreateTime);
            var Online = onlineService.GenericService.GetAll().OrderByDescending(p => p.BeginTime).Take(10).ToList();
            rptOnline.DataSource = Online.ToList();
            rptOnline.DataBind();
        }

        private void GetDataAndBindall()
        {
            System.Linq.Expressions.Expression<Func<OnlineVote, bool>> pre = p => true;
            int count = 0;
            var roles = onlineService.GenericService.GetListByPage(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out count, pre, new OrderByMultiCondition<OnlineVote, DateTime>(c => c.BeginTime, true));
            AspNetPager1.RecordCount = count;
            repPreferent.DataSource = roles.ToList();
            repPreferent.DataBind();
        }

        public string OnlineStatus(string BeginTime, string EndTime)
        {
            return nf.OnlineStatus(BeginTime, EndTime);
        }

        public string DateTimeConver(string datetime)
        {
            return nf.DateTimeConver(datetime);
        }

        public string rankSpan(string top, string Option, string ReviewsNum, string OnlineVoteID)
        {
            return nf.rankSpan(top, Option, ReviewsNum, OnlineVoteID);
        }


        protected void rptPreferent_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = e.Item.DataItem as OnlineVote;
                var options = item.NY_VoteOptions.OrderByDescending(p => p.ReviewsNum).Take(3);

                var rpt = (Repeater)e.Item.FindControl("rptPreferent");
                rpt.DataSource = options;
                rpt.DataBind();
                //绑定rpt

                Sum = item.NY_VoteOptions.Sum(p => p.ReviewsNum).ToString();
            }
        }

        protected void rptOnlineOptions_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var item = e.Item.DataItem as OnlineVote;
                var options = item.NY_VoteOptions.ToList().OrderByDescending(p => p.ReviewsNum).Take(3);
                var rpt = (Repeater)e.Item.FindControl("rptOnlineOptions");
                rpt.DataSource = options;
                rpt.DataBind();
                //绑定rpt
                Sum = item.NY_VoteOptions.Sum(p => p.ReviewsNum).ToString();
            }
        }

        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            GetDataAndBindall();
        }
       
        public string GenerateContentUrl(string id)
        {
            return "OnlineVotingDetail.aspx?TopNavId=" + TopNavId + "&CurNavId=" + CurNavId + "&id=" + id;
        }
    }
}