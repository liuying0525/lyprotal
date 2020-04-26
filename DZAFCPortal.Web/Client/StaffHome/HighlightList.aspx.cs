using DZAFCPortal.Entity;
using DZAFCPortal.Facade;
using DZAFCPortal.Service;
using NySoftland.Core.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Client.StaffHome
{
    public partial class HighlightList : AuthrizedBase
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
        HighlightsService auctionSerivce = new HighlightsService();
        HighlightsLikeHistoryService itemHistoryService = new HighlightsLikeHistoryService();
        StatusFacade nf = new StatusFacade();
        #endregion

        /// <summary>
        /// 获取数据，并绑定到列表中
        /// </summary>
        private void GetDataAndBind()
        {
            var efamily = auctionSerivce.GenericService.GetAll().OrderByDescending(p => p.AuctionBeginTime).Take(10).ToList();
            rptAuction.DataSource = efamily.ToList();
            rptAuction.DataBind();
        }

        private void GetDataAndBindall()
        {
            System.Linq.Expressions.Expression<Func<Highlights, bool>> pre = p => true;
            int count = 0;
            var efamily = auctionSerivce.GenericService.GetListByPage(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out count, pre, new OrderByMultiCondition<Highlights, DateTime>(c => c.AuctionBeginTime, true));
            AspNetPager1.RecordCount = count;
            repAuctions.DataSource = efamily.ToList();
            repAuctions.DataBind();
            //ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>active();</script>"); 
        }

        public string DateTimeConver(string datetime)
        {
            return nf.DateTimeConver(datetime);
        }

        public string Status(string BeginTime, string EndTime)
        {
            return nf.PromotionStatus(BeginTime, EndTime);
        }

        public string GenerateContentUrl(string id)
        {
            return "HighlightDetail.aspx?TopNavId=" + TopNavId + "&CurNavId=" + CurNavId + "&id=" + id;
        }

        public int auctionCount(string id)
        {
            int count = itemHistoryService.GenericService.GetAll(p => p.NY_AuctionItemID == id && p.HistoryThumbs == 1).ToList().Count;
            return count;
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
    }
}