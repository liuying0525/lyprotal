using DZAFCPortal.Utility;
using DZAFCPortal.Entity;
using DZAFCPortal.Facade;
using DZAFCPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DZAFCPortal.Web.Client.StaffHome.AjaxPage
{
    /// <summary>
    /// getAjaxThumbs 的摘要说明
    /// </summary>
    public class getAjaxThumbs : IHttpHandler
    {
        HighlightsLikeHistoryService nh = new HighlightsLikeHistoryService();
        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";

            updateThumbs(context);
        }
        HighlightsLikeHistoryService itemHistoryService = new HighlightsLikeHistoryService();
        private void updateThumbs(HttpContext context)
        {
            try
            {
                string img = context.Request["img"];
                string id = context.Request["id"];

                string account = UserInfo.Account;
                //string name = NySoftland.Moss.Helper.GetCurrentDisplayName();
                string name = userDisplayName.UserDisplayName();
                var history = nh.GenericService.GetAll(p => p.NY_AuctionItemID == id && p.AuctionAccount == account).ToList();
                if (history.Count <= 0)
                {
                    Entity.HighlightsLikeHistory auction = new Entity.HighlightsLikeHistory();
                    auction.NY_AuctionItemID = id;
                    auction.AuctionName = name;
                    auction.AuctionAccount = account;
                    auction.HistoryThumbs = 1;
                    nh.GenericService.Add(auction);
                }
                else
                {
                    var history2 = nh.GenericService.GetModel(history.First().ID);
                    if (history2.HistoryThumbs == 0)
                    {
                        history2.HistoryThumbs = 1;
                    }
                    else
                    {
                        history2.HistoryThumbs = 0;
                    }
                    nh.GenericService.Update(history2);
                }
                nh.GenericService.Save();
           
                int count = itemHistoryService.GenericService.GetAll(p => p.NY_AuctionItemID == id && p.HistoryThumbs == 1).ToList().Count;
                context.Response.Write("1,"+count);
            }
            catch (Exception ex)
            {
                context.Response.Write(ex.Message);
            }
        }


        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}