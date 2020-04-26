using DZAFCPortal.Entity;
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
    public partial class OnlineVotingDetail : AuthrizedBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string id = Request["id"];
                GetDataAndBind(id);
                GetRegAndBind(id);
            }
        }

        #region--------Service 私有变量---------
        OnlineVoteService onlineService = new OnlineVoteService();
        OnlineVoteOptionsService optionService = new OnlineVoteOptionsService();
        OnlineVoteRecordsService adminService = new OnlineVoteRecordsService();
        StatusFacade nf = new StatusFacade();
        #endregion
        private void GetDataAndBind(string id)
        {
            var preferent = onlineService.GenericService.GetModel(id);
            litTitle2.Text = preferent.Title;
            imageUrl.ImageUrl = preferent.ImageURL;
            litOnlineTime.Text = preferent.BeginTime.ToString("yyyy-MM-dd HH:mm") + "至" + preferent.EndTime.ToString("yyyy-MM-dd HH:mm");
            litPublishDept.Text = preferent.PublishDept;
            litCreator.Text = new UserService().getUserName(preferent.Creator);
            labSummary.Text = preferent.Summary;
            labContent.Text = preferent.OnlineDescription;
            aOnline.HRef = "NY_OnlineAdd.aspx?id=" + preferent.ID + "&TopNavId=" + TopNavId + "&CurNavId=" + CurNavId;
            status.InnerText = nf.OnlineStatus(preferent.BeginTime.ToString(), preferent.EndTime.ToString());
            //eventsStatus(status.InnerText);
            //if (status.InnerText != "投票进行中" || IsOnline(guid))
            //{
            //    aOnline.Visible = false;
            //    btnOnline.Enabled = false;
            //    btnOnline.Visible = true;
            //}
            if (status.InnerText == "预告")
            {
                btnOnline.Visible = true;
                aOnline.Visible = false;
                btnOnline.Text = "投票未开始";
                btnOnline.CssClass = "ny_detail_abstract_btn ny_btn_forbid fl";
            }
            else if (status.InnerText == "投票进行中")
            {
                if (IsOnline(id))
                {
                    btnOnline.Visible = true;
                    aOnline.Visible = false;
                }
            }
            else
            {
                btnOnline.Visible = true;
                aOnline.Visible = false;
                btnOnline.Text = "投票已结束";
                btnOnline.CssClass = "ny_detail_abstract_btn ny_btn_forbid fl";
            }
        }


        private void eventsStatus(string statusText)
        {
            //switch (statusText)
            //{
            //    case "预告":
            //        onlineDetail.Style.Add("Width", "0%");
            //        statusTrailer.Attributes.Add("class", "active fl");
            //        statusIng.Attributes.Add("class", "fl");
            //        statusEnd.Attributes.Add("class", "fr");
            //        break;
            //    case "投票进行中":
            //        onlineDetail.Style.Add("Width", "50%");
            //        statusTrailer.Attributes.Add("class", "active fl");
            //        statusIng.Attributes.Add("class", "active fl");
            //        statusEnd.Attributes.Add("class", "fr");
            //        break;
            //    case "投票结束":
            //        onlineDetail.Style.Add("Width", "100%");
            //        statusTrailer.Attributes.Add("class", "active fl");
            //        statusIng.Attributes.Add("class", "active fl");
            //        statusEnd.Attributes.Add("class", "active fr");
            //        break;
            //}
        }

        private void GetRegAndBind(string id)
        {

            var solicitationRegister = optionService.GenericService.GetAll(p => p.NY_OnlineVoteID == id).OrderByDescending(p => p.ReviewsNum).ToList();

            //CheckBoxList1.DataValueField = "ReviewsNum";
            repOnline.DataSource = solicitationRegister;
            repOnline.DataBind();
        }

        public string DateTimeConver(string datetime)
        {
            return nf.DateTimeConver(datetime);
        }

        public string percentage(string ReviewsNum, string pic)
        {
            string id = Request["id"];
            string Sum = GetSum(id);
            float result = 0;
            string resultback = "";
            if (ReviewsNum != "0" && Sum != "0")
            {
                result = float.Parse(ReviewsNum) / float.Parse(Sum);
                resultback = "(" + (result * 100).ToString("0.00") + "%)";
            }
            else
            {
                resultback = "(0%)";
            }
            if (!string.IsNullOrEmpty(pic))
            {
                resultback = resultback.Replace("(", "").Replace(")", "");
            }
            return resultback;
        }

        public string GetSum(string OnlineVoteID)
        {
            string Sum = "";
            Sum = optionService.GenericService.GetAll(p => p.NY_OnlineVoteID == OnlineVoteID).Sum(p => p.ReviewsNum).ToString();
            return Sum;
        }

        public bool IsOnline(string guid)
        {
            bool result = false;
            string userName = UserInfo.Account;
            var od = adminService.GenericService.GetAll().Where(p => p.OnlineVoteID == guid && p.UserName == userName).ToList();
            if (od.Count > 0)
            {
                result = true;
            }
            return result;
        }
    }
}