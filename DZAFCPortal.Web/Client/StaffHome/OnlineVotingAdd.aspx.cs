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
    public partial class OnlineVotingAdd : AuthrizedBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string id = Request["id"];
                GetRegAndBind(id);
            }
        }

        #region--------Service 私有变量---------
        OnlineVoteService onlineService = new OnlineVoteService();
        OnlineVoteOptionsService optionService = new OnlineVoteOptionsService();
        OnlineVoteRecordsService adminService = new OnlineVoteRecordsService();
        #endregion
        private void GetRegAndBind(string id)
        {

            var solicitationRegister = optionService.GenericService.GetAll(p => p.NY_OnlineVoteID == id).OrderBy(p => p.CreateTime).ToList();

            //CheckBoxList1.DataValueField = "ReviewsNum";
            repOnlineAdd.DataSource = solicitationRegister;
            repOnlineAdd.DataBind();
            var vote = onlineService.GenericService.GetModel(id);
            if (vote.VoteType == "MustNum")
            {
                litNum.Text = "必须选" + vote.VoteNum.ToString() + "项";
            }
            else
            {
                litNum.Text = "最多可选" + vote.VoteNum.ToString() + "项";
            }
            //litNum.Text = vote.VoteNum.ToString();
            spanTitle.InnerText = vote.Title;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int online = 0;
            int num = 0; //选中评论个数
            string option = "";
            string id = Request["id"];
            if (!adminService.IsOnline(id))
            {
                for (int i = 0; i < this.repOnlineAdd.Items.Count; i++)
                {
                    CheckBox checkOnline = (CheckBox)this.repOnlineAdd.Items[i].FindControl("checkOption");
                    if (checkOnline.Checked)
                    {
                        num += 1;
                    }
                }
                var vote = onlineService.GenericService.GetModel(id);
                if (vote.VoteType == "MaxNum")
                {
                    if (num > vote.VoteNum)
                    {
                        Response.Write("<script defer='defer'>alert('您选择的选项数量大于最大可选项数量！');</script>");
                        Response.Write("<script>window.location.href='NY_OnlineDetail.aspx?id=" + id.ToString() + "&TopNavId=" + TopNavId + "&CurNavId=" + CurNavId + "';</script>");
                        return;
                    }
                }
                else
                {
                    if (num != vote.VoteNum)
                    {
                        Response.Write("<script defer='defer'>alert('您必须选择" + vote.VoteNum + "项！');</script>");
                        Response.Write("<script>window.location.href='NY_OnlineDetail.aspx?id=" + id.ToString() + "&TopNavId=" + TopNavId + "&CurNavId=" + CurNavId + "';</script>");
                        return;
                    }
                }
                for (int i = 0; i < this.repOnlineAdd.Items.Count; i++)
                {
                    CheckBox checkOnline = (CheckBox)this.repOnlineAdd.Items[i].FindControl("checkOption");
                    Label labNum = (Label)this.repOnlineAdd.Items[i].FindControl("labNum");
                    if (checkOnline.Checked)
                    {
                        online = int.Parse(labNum.Text);
                        option = checkOnline.Text;
                        var optionsSolicitation = optionService.CreateNewPrefer(online, option, id);
                        optionService.GenericService.Update(optionsSolicitation);
                        var adminSolicitation = CreateNewAdmin(optionsSolicitation.ID, id);
                        adminService.GenericService.Add(adminSolicitation);
                    }
                }
                optionService.GenericService.Save();
                adminService.GenericService.Save();
                Response.Write("<script defer='defer'>alert('投票已成功！');</script>");
                Response.Write("<script>window.location.href='NY_OnlineDetail.aspx?id=" + id.ToString() + "&TopNavId=" + TopNavId + "&CurNavId=" + CurNavId + "';</script>");
            }
            else
            {
                Response.Write("<script defer='defer'>alert('您已投过票，不能重复投票！');</script>");
                Response.Write("<script>window.location.href='NY_OnlineDetail.aspx?id=" + id.ToString() + "&TopNavId=" + TopNavId + "&CurNavId=" + CurNavId + "';</script>");
            }
        }
        private OnlineVoteRecords CreateNewAdmin(string OnlineVoteAdminID, string OnlineVoteID)
        {
            OnlineVoteRecords od = new OnlineVoteRecords();
            od.NY_OnlineVoteAdminID = OnlineVoteAdminID;
            od.OnlineVoteID = OnlineVoteID;
            od.UserName = UserInfo.Account;
            return od;
        }
    }
}