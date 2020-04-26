using DZAFCPortal.Utility;
using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Client.StaffHome
{
    public partial class ActivitySignUp : AuthrizedBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        #region--------Service 私有变量---------
        ActivitiesService eeSerivce = new ActivitiesService();
        ActivityParticipantsService esSerivce = new ActivityParticipantsService();
        #endregion
        protected void btnRegistration_Click(object sender, EventArgs e)
        {
            ActivityParticipants er = new ActivityParticipants();
            string id = Request["id"];
            string TopNavId = Request["TopNavId"] == null ? string.Empty : Request["TopNavId"];
            string CurNavId = Request["CurNavId"] == null ? string.Empty : Request["CurNavId"];
            string EventAccount = txtEventAccount.Text.Trim();
            if (isBookMax(id))
            {
                Response.Write("<script defer='defer'>alert('这个活动名额已满!!');</script>");
                Response.Write("<script>window.location.href='NY_Home_eventDetail.aspx?TopNavId=" + TopNavId + "&CurNavId=" + CurNavId + "&id=" + id.ToString() + "';</script>");
                return;
            }
            if (isRegist(id, EventAccount))
            {
                Response.Write("<script defer='defer'>alert('您已报名或者员工号已提交过，请勿重复提交!');</script>");
                Response.Write("<script>window.location.href='NY_Home_eventDetail.aspx?TopNavId=" + TopNavId + "&CurNavId=" + CurNavId + "&id=" + id.ToString() + "';</script>");
                return;
            }
            if (isBookTime(id))
            {
                Response.Write("<script defer='defer'>alert('您已错过报名时间!');</script>");
                Response.Write("<script>window.location.href='NY_Home_eventDetail.aspx?TopNavId=" + TopNavId + "&CurNavId=" + CurNavId + "&id=" + id.ToString() + "';</script>");
                return;
            }
            er.NY_EventSolicitationID = id;
            er.UserAccount = UserInfo.Account;
            er.UserDisplayName = txtName.Text.Trim();
            er.RegistTime = DateTime.Now;
            er.Sex = int.Parse(dropSex.SelectedValue);
            er.PhoneNumber = txtPhoneNumber.Text.Trim();
            er.EventAccount = txtEventAccount.Text.Trim();
            er.Email = txtEmail.Text.Trim() + "@hkbea.com";
            esSerivce.GenericService.Add(er);
            esSerivce.GenericService.Save();
            Response.Write("<script defer='defer'>alert('报名已成功!');</script>");
            //Fxm.Utility.Page.MessageBox.Show("保存已成功!");
            Response.Write("<script>window.location.href='NY_Home_eventsDetail.aspx?TopNavId=" + TopNavId + "&CurNavId=" + CurNavId + "&id=" + id.ToString() + "';</script>");
        }

        private bool isBookMax(string guid)
        {
            bool result = false;
            var booktimeRegister = eeSerivce.GenericService.GetModel(guid);
            var teamRegister = esSerivce.GenericService.GetAll(p => p.NY_EventSolicitationID == guid).ToList();
            int MaxCount = booktimeRegister.MaxPersonCount;
            int UsedMaxCOunt = teamRegister.Count;
            if (teamRegister.Count > MaxCount)
            {
                result = true;
            }
            return result;
        }


        private bool isRegist(string guid, string EventAccount)
        {
            bool result = false;
            var solicitationRegister = esSerivce.GenericService.GetAll(p => p.NY_EventSolicitationID == guid).FirstOrDefault();
            if (solicitationRegister != null)
            {
                if (solicitationRegister.UserAccount == UserInfo.Account || solicitationRegister.EventAccount == EventAccount)
                {
                    result = true;
                }
            }
            return result;
        }

        private bool isBookTime(string guid)
        {
            bool result = false;
            DateTime Bookbegintime;
            DateTime Bookendtime;
            var booktimeRegister = eeSerivce.GenericService.GetModel(guid);
            Bookbegintime = booktimeRegister.BookBeginTime;
            Bookendtime = booktimeRegister.BookEndTime;
            if (DateTime.Now > Bookendtime || DateTime.Now < Bookbegintime)
            {
                result = true;
            }
            return result;
        }
    }
}