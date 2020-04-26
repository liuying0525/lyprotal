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
    public partial class ActivityDetail : AuthrizedBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string id = Request["id"];
                int type = LoadEvents(id);
                GetRegAndBind(id, type);
            }
        }

        #region--------Service 私有变量---------
        ActivitiesService eventSerivce = new ActivitiesService();
        ActivityParticipantsService esSerivce = new ActivityParticipantsService();
        ActivityTeamsService teamService = new ActivityTeamsService();
        StatusFacade nf = new StatusFacade();
        DZAFCPortal.Authorization.DAL.UserService userService = new DZAFCPortal.Authorization.DAL.UserService();
        #endregion


        /// <summary>
        /// 获取数据，并绑定到列表中
        /// </summary>
        private int LoadEvents(string id)
        {
           
            var events = eventSerivce.GenericService.GetModel(id);
           
            litName2.Text = events.Name;
            imageUrl.ImageUrl = events.ImageURL;
            litBookTime.Text = events.BookBeginTime.ToString("yyyy-MM-dd HH:mm") + "至" + events.BookEndTime.ToString("yyyy-MM-dd HH:mm");
            litActTime.Text =  DateTimeConver(events.ActBeginTime.ToString()) + "至" + DateTimeConver(events.ActEndTime.ToString());
            litPublishDept.Text = events.PublishDept;
            litCreator.Text = new UserService().getUserName(events.Creator);
            labSummary.Text = events.Summary;
            labContent.Text = events.ActDescription;
            labActWay.Text = events.ActWay;
            var solicitationRegister = esSerivce.GenericService.GetAll(p => p.NY_EventSolicitationID == id).ToList();
            if (events.Type == 1)
            {
                labNum.Text = "已报名人数：" + solicitationRegister.Count + "/" + events.MaxPersonCount;
            }
            else
            {
                var teamRegister = teamService.GenericService.GetAll(p => p.NY_EventSolicitationID == id).ToList();
                labNum.Text = "已报名人数：" + teamRegister.Count + "/" + events.MaxPersonCount;
            }
            status.InnerText = nf.Status(events.BookBeginTime.ToString(), events.BookEndTime.ToString(), events.ActBeginTime.ToString(), events.ActEndTime.ToString());
            var btnStatus = nf.EventStatus(status.InnerText, solicitationRegister.Count, events.MaxPersonCount, id);
            btnRegistration.Text = btnStatus.Name;
            btnRegistration.CssClass = btnStatus.ClassName;
            btnRegistration.Enabled = btnStatus.Enable;
            return events.Type;
        }


        private void GetRegAndBind(string id, int type)
        {
            string account = UserInfo.Account;
            if (type == 1)
            {
                person.Visible = true;
                team.Visible = false;
                var solicitationRegister = esSerivce.GenericService.GetAll(p => p.NY_EventSolicitationID == id).OrderBy(p => p.RegistTime).ToList();
                rptRegister.DataSource = solicitationRegister;
                rptRegister.DataBind();
            }
            else
            {
                person.Visible = false;
                team.Visible = true;
                var solicitationRegister = teamService.GenericService.GetAll(p => p.NY_EventSolicitationID == id && p.UserAccount == account).OrderBy(p => p.CreateTime).ToList();
                rptTeam.DataSource = solicitationRegister;
                rptTeam.DataBind();
            }

        }

        public string DateTimeConver(string datetime)
        {
            return nf.DateTimeConver(datetime);
        }

        protected void btnRegistration_Click(object sender, EventArgs e)
        {
            ActivityParticipants er = new ActivityParticipants();
            string id = Request["id"];
            string EventAccount = UserInfo.Account;
            if (esSerivce.isBookMax(id))
            {
                Fxm.Utility.Page.MessageBox.Show("这个活动名额已满!");
                return;
            }
            if (esSerivce.isRegist(id, EventAccount))
            {
                Fxm.Utility.Page.MessageBox.Show("您已报名，请勿重复提交!");
                return;
            }
            if (eventSerivce.isBookTime(id))
            {
                Fxm.Utility.Page.MessageBox.Show("您已错过报名时间!");
                return;
            }
            var user = userService.GenericService.GetAll(p => p.Account == EventAccount).First();
            er.NY_EventSolicitationID = id;
            er.UserAccount = UserInfo.Account;
            //er.UserDisplayName = NySoftland.Moss.Helper.GetCurrentDisplayName();
            er.UserDisplayName = userDisplayName.UserDisplayName();
            er.RegistTime = DateTime.Now;
            //er.Sex = int.Parse(dropSex.SelectedValue);
            er.PhoneNumber = user.MobilePhone;
            er.EventAccount = user.OrganizationName;
            er.Email = user.Email;
            esSerivce.GenericService.Add(er);
            esSerivce.GenericService.Save();
            Fxm.Utility.Page.MessageBox.Show("您已成功报名，可在报名记录中查询到您的报名记录！");
            LoadEvents(id.ToString());
            GetRegAndBind(id.ToString(), 1);
        }
    }
}