using DZAFCPortal.Facade;
using DZAFCPortal.Service;
using DZAFCPortal.Authorization.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DZAFCPortal.Utility;

namespace DZAFCPortal.Web.Client.StaffHome
{
    public partial class HighlightDetail : AuthrizedBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request["id"];
                LoadEvents(id);
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
        private void LoadEvents(string id)
        {
            string account = UserInfo.Account;
            var events = auctionSerivce.GenericService.GetModel(id);
            var events2 = itemHistoryService.GenericService.GetAll(p => p.NY_AuctionItemID == id && p.AuctionAccount == account).FirstOrDefault();
            litName2.Text = events.AuctionName;
            litAuctionTime.Text = events.AuctionBeginTime.ToString("yyyy-MM-dd HH:mm") + "至" + events.AuctionEndTime.ToString("yyyy-MM-dd HH:mm");
            labSummary.Text = events.Summary;
            if (events2 != null)
            {
                if (events2.HistoryThumbs == 0)
                {
                    imgThumbs.Attributes.Add("class", "ny_mainlist_nametitle_cancelgood");
                    labPraise.Text = "赞";
                }
                else
                {
                    imgThumbs.Attributes.Add("class", "ny_mainlist_nametitle_good");
                    labPraise.Text = "取消赞";
                }
            }
            imgThumbs.Attributes.Add("acution", events.ID);
            imageUrl.ImageUrl = events.IndexImgUrl;
            labContent.Text = events.ItemSummary;
            litPublishDept.Text = events.PublishDept;
            litCreator.Text = new UserService().getUserName(events.Creator);
            auctionCount(id);
            //status.InnerText = nf.PromotionStatus(events.AuctionBeginTime.ToString(), events.AuctionEndTime.ToString());
        }

        public void auctionCount(string id)
        {

            int count = itemHistoryService.GenericService.GetAll(p => p.NY_AuctionItemID == id && p.HistoryThumbs == 1).ToList().Count;
            labGood.Text = count.ToString();
        }
    }
}