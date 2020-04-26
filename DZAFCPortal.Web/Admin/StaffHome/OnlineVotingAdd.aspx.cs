using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using DZAFCPortal.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.StaffHome
{
    public partial class OnlineVotingAdd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //指定附件类型
            UploadAttach.AttachType = DZAFCPortal.Config.Enums.AttachType.在线评选;
            if (!Page.IsPostBack)
            {
                dropdownload();
                string id = Request["id"];
                if (!string.IsNullOrEmpty(id))
                {
                    EFamilyShow(id);
                }
                else
                {
                    string account = DZAFCPortal.Utility.UserInfo.Account;
                    //txtPublishDept.Text = userService.GenericService.GetAll(p => p.Account == account).First().Department;
                    txtPublishDept.Text = "东正金融";
                }
            }
            //else
            //{
            //    string id = Request["id"];
            //    if (!string.IsNullOrEmpty(id))
            //    {
            //        EFamilyShow(id);
            //    }
            //}
        }

        #region--------Service 私有变量---------
        OnlineVoteService onlineService = new OnlineVoteService();
        OnlineVoteOptionsService optionService = new OnlineVoteOptionsService();
        DZAFCPortal.Authorization.DAL.UserService userService = new DZAFCPortal.Authorization.DAL.UserService();
        UMS_MessageService umsSercice = new UMS_MessageService();
        #endregion
        private void dropdownload()
        {
            int max = 10;
            ListItem item;
            for (int i = 0; i < max; i++)
            {
                if (i == 0)
                {
                    item = new ListItem("请选择", (i + 1).ToString());
                }
                else
                {
                    item = new ListItem((i + 1).ToString(), (i + 1).ToString());
                }
                this.dropOnline.Items.Add(item);
            }

            for (int i = 0; i < max; i++)
            {

                item = new ListItem((i + 1).ToString(), (i + 1).ToString());
                this.dropNum.Items.Add(item);
            }
        }


        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowVote();
        }

        private void dropdownList(List<OnlineVoteOptions> oo, bool update)
        {
            for (int i = 0; i < this.dropOnline.SelectedIndex + 1; i++)
            {
                HtmlTableRow TabRow = new HtmlTableRow();
                HtmlTableCell cell = new HtmlTableCell();
                HtmlTableCell cell2 = new HtmlTableCell();
                //TextBox txtVote = new TextBox();
                //HtmlTextArea txtVote = new HtmlTextArea();
                TextBox txtVote = new TextBox();
                txtVote.ID = "txtVote" + i;
                //txtVote.Attributes.Add("class", "form-control");
                txtVote.Style.Add("width", "100%");
                txtVote.Style.Add("min-height", "50px");
                txtVote.Attributes.Add("t", "option");
                if (oo != null)
                {
                    if (i < oo.Count)
                        txtVote.Text = oo[i].Option;
                    if (update)
                    {
                        //txtVote.Disabled = false;
                        txtVote.Enabled = false;
                    }
                }
                cell.Controls.Add(txtVote);
                TabRow.Controls.Add(cell2);
                TabRow.Controls.Add(cell);
                tableOnline.Controls.Add(TabRow);
            }
            //ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script> textareaGetEditor();</script>");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string id = Request["id"];
            try
            {
                var eventSolicitation = CreateNewPrefer();
                if (!string.IsNullOrEmpty(id))
                {
                    onlineService.GenericService.Update(eventSolicitation);
                    onlineService.GenericService.Save();
                    var ee = onlineService.GenericService.GetModel(id);
                    if (DateTime.Now < ee.BeginTime)
                    {
                        DeleteOteOptions(id);
                        VoteAdd(eventSolicitation);
                    }
                    //保存附件
                    UploadAttach.SaveAttach(eventSolicitation.ID);
                }
                else
                {
                    onlineService.GenericService.Add(eventSolicitation);
                    onlineService.GenericService.Save();
                    VoteAdd(eventSolicitation);
                    //保存附件
                    UploadAttach.SaveAttach(eventSolicitation.ID);

                    UMS_Message um = CreateUMS_Message(eventSolicitation);
                    umsSercice.AddMessage(um);
                }
                //Fxm.Utility.Page.MessageBox.Show("保存成功!");
                //ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>RefreshParent();</script>");
                Fxm.Utility.Page.JsHelper.CloseWindow(false, "数据保存成功！", "refresh();");
            }
            catch (Exception ex)
            {
                Fxm.Utility.Page.MessageBox.Show("保存过程发生错误，请重试!");

                //记录错误日志
                NySoftland.Core.Log4.LogHelper.Error("在线评选添加异常[PreferentActionAdd.aspx]", ex);
            }
        }

        NavigateService ns = new NavigateService();
        private UMS_Message CreateUMS_Message(OnlineVote es)
        {
            Navigator nv = ns.GenericService.First(p => p.Title.Equals("在线评选"));
            UMS_Message um = new UMS_Message();
            um.EngineType = EngineTypeEnum.WeChat.ToString();
            um.To = "@all";//默认为向关注微信的全部成员发送
            um.Subject = es.Title;
            um.Body = es.Summary;
            um.State = MessageStateEnum.Waiting.ToString();
            um.ErrorCount = 0;
            um.CreateTime = DateTime.Now;
            um.Url = "/StaffHome/WxNY_OnlineDetail.aspx?TopNavId=" + nv.ParentID + "&CurNavId=" + nv.ID + "&id=" + es.ID;
            um.EstimateTime = DateTime.Now;
            um.Source = ApplicationEnum.职工之家.ToString();
            um.Images = es.ImageURL;
            return um;
        }
        private void VoteAdd(OnlineVote ov)
        {

            var voteEvent = new OnlineVoteOptions();
            for (int i = 0; i < this.dropOnline.SelectedIndex + 1; i++)
            {
                //TextBox vote = (TextBox)this.Page.Master.FindControl("ContentPlaceHolder1").FindControl("tableOnline").FindControl("");
                string[] vote = Request.Form.GetValues("ctl00$ContentPlaceHolder1$txtVote" + i);
                voteEvent = CreateNewVote(ov.ID, vote[0].ToString(), i);
                optionService.GenericService.Add(voteEvent);
                //if (!string.IsNullOrEmpty(Request["id"]))
                //{
                //    //optionService.GenericService.Update(voteEvent);
                //    optionService.GenericService.Add(voteEvent);
                //}
                //else
                //{ 
                //    optionService.GenericService.Add(voteEvent);
                //}
            }
            optionService.GenericService.Save();
        }



        private DZAFCPortal.Entity.OnlineVote CreateNewPrefer()
        {
            DZAFCPortal.Entity.OnlineVote es = new DZAFCPortal.Entity.OnlineVote();
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                es = onlineService.GenericService.GetModel(Request["ID"]);
            }
            else
            {
                es.ID = Guid.NewGuid().ToString();
            }
            es.Title = txtName.Text.Trim();
            es.Summary = txtSummary.Text.Trim();
            if (!string.IsNullOrEmpty(hidUpload.Value.Trim()))
            {
                es.ImageURL = hidUpload.Value.Trim();
            }
            es.BeginTime = DateTime.Parse(beginTime.Value.Trim());
            es.EndTime = DateTime.Parse(endTime.Value.Trim());
            es.PublishDept = txtPublishDept.Text.Trim();
            es.OnlineDescription = txtActDescription.Value.Trim();
            es.VoteNum = int.Parse(dropNum.SelectedValue.ToString());
            es.VoteType = dropVoteType.SelectedValue;
            es.Creator = DZAFCPortal.Utility.UserInfo.Account;
            return es;
        }

        private OnlineVoteOptions CreateNewVote(string id, string vote, int i)
        {
            OnlineVoteOptions oo = new OnlineVoteOptions();
            //if (!string.IsNullOrEmpty(Request["id"]))
            //{
            //    oo = optionService.GenericService.GetAll(p => p.OnlineVoteID == id).ToList()[i];
            //}
            //else 
            //{
            //    oo.OnlineVoteID = id;
            //}
            oo.NY_OnlineVoteID = id;
            oo.Option = vote.Trim();
            oo.CreateTime = DateTime.Now.AddSeconds(i * 0.1);
            return oo;
        }

        #region 页面赋值
        public void EFamilyShow(string id)
        {
            bool update = false;
            var ee = onlineService.GenericService.GetModel(id);
            var option = optionService.GenericService.GetAll().Where(p => p.NY_OnlineVoteID == id).OrderBy(p => p.CreateTime).ToList();
            txtName.Text = ee.Title;
            txtSummary.Text = ee.Summary;
            if (!string.IsNullOrEmpty(ee.ImageURL))
            {
                imgUpload.ImageUrl = ee.ImageURL;
                imgUpload.Visible = true;
            }
            beginTime.Value = (ee.BeginTime.ToString("yyyy-MM-dd HH:mm")).ToString();
            endTime.Value = (ee.EndTime.ToString("yyyy-MM-dd HH:mm")).ToString();
            txtPublishDept.Text = ee.PublishDept;
            txtActDescription.Value = ee.OnlineDescription;
            if (DateTime.Now > ee.BeginTime)
            {
                update = true;
            }
            if (update)
            {
                dropOnline.Enabled = false;
            }
            dropOnline.SelectedIndex = option.Count - 1;
            dropNum.SelectedIndex = ee.VoteNum - 1;
            dropVoteType.SelectedValue = ee.VoteType;
            dropdownList(option, update);
            //加载附件
            UploadAttach.GetAttachAndBind(id);
        }
        #endregion

        private void DeleteOteOptions(string id)
        {
            optionService.GenericService.Delete(p => p.NY_OnlineVoteID == id);
        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //byte[] uploadFileBytes =  //Convert.FromBase64String(Request[fileIndexImgUrl.PostedFile.FileName]);
            var file = fileIndexImgUrl.PostedFile;
            if (file.ContentLength > 100000)
            {
                Fxm.Utility.Page.MessageBox.Show("图片尺寸太大！请将图片限制在100K以下！");
                return;
            }

            var datas = new byte[file.ContentLength];
            var stream = file.InputStream;
            stream.Read(datas, 0, file.ContentLength);

            string uploadFileName = Guid.NewGuid() + "_" + System.IO.Path.GetFileName(fileIndexImgUrl.PostedFile.FileName);
            var savePath = "/Uploads/EFamilys/";
            string fileFullName = savePath + uploadFileName.Replace(" ", "");
            //string[] Extensions = { ".jpg" };
            //Result rt = Fxm.Utility.FileHelper.UploadToWeb(Extensions, fileIndexImgUrl.PostedFile, savePath, uploadFileName);
            
            var result = FileHelper.UploadDocument(datas, fileFullName, DZAFCPortal.Config.AppSettings.SharePointSiteUrl, DZAFCPortal.Config.Base.ImageExtensions);
            if (result.IsSucess)
            {
                imgUpload.ImageUrl = fileFullName;
                imgUpload.Visible = true;
                hidUpload.Value = fileFullName;
                Fxm.Utility.Page.MessageBox.Show(result.Message);
            }
            else
            {
                Fxm.Utility.Page.MessageBox.Show(result.Message);
            }
            ShowVote();
        }


        public void ShowVote()
        {
            string id = Request["id"];

            if (!string.IsNullOrEmpty(id))
            {
                var option = optionService.GenericService.GetAll().Where(p => p.NY_OnlineVoteID == id).OrderBy(p => p.CreateTime).ToList();
                dropdownList(option, false);
            }
            else
            {
                dropdownList(null, false);
            }
        }
    }
}