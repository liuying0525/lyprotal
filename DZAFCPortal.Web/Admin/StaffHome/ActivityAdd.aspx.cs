using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using DZAFCPortal.Utility;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.StaffHome
{
    public partial class ActivityAdd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string id = Request["id"];
                if (!string.IsNullOrEmpty(id))
                {
                    EFamilyShow();   
                }
                else
                {
                    string account = DZAFCPortal.Utility.UserInfo.Account;
                    //txtPublishDept.Text = userService.GenericService.GetAll(p => p.Account == account).First().Department;
                    txtPublishDept.Text = "东正金融";
                }
            }
        }
        #region--------Service 私有变量---------
        ActivitiesService eeSerivce = new ActivitiesService();
        UMS_MessageService umsSercice = new UMS_MessageService();
        DZAFCPortal.Authorization.DAL.UserService userService = new DZAFCPortal.Authorization.DAL.UserService();
        #endregion

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string id = Request["id"];
            try
            {
                var eventSolicitation = CreateNewPrefer();
                if (!string.IsNullOrEmpty(id))
                {
                    eeSerivce.GenericService.Update(eventSolicitation);
                }
                else
                {
                    eeSerivce.GenericService.Add(eventSolicitation);
                    UMS_Message um = CreateUMS_Message(eventSolicitation);
                    umsSercice.AddMessage(um);
                }
                eeSerivce.GenericService.Save();
               
                Fxm.Utility.Page.JsHelper.CloseWindow(false, "数据保存成功！", "refresh();");
            }
            catch (Exception ex)
            {
                Fxm.Utility.Page.MessageBox.Show("保存过程发生错误，请重试!");

                //记录错误日志
                NySoftland.Core.Log4.LogHelper.Error("活动召集添加异常[PreferentActionAdd.aspx]", ex);
            }
        }

        private DZAFCPortal.Entity.Activities CreateNewPrefer()
        {
            DZAFCPortal.Entity.Activities es = new DZAFCPortal.Entity.Activities();
            if (!string.IsNullOrEmpty(Request["id"]))
            {
                es = eeSerivce.GenericService.GetModel(Request["ID"]);
            }
            es.Name = txtName.Text.Trim();
            es.Type = int.Parse(dropType.SelectedValue);
            es.MaxPersonCount = int.Parse(txtMaxPersonCount.Text.Trim(),0);
            //es.TeamMaxPerson = int.Parse(txtTeamMaxPerson.Text.Trim(), 0);
            //pa.Publisher = txtPublisher.Text.Trim();
            es.ActDescription = txtActDescription.Value.Trim();
            es.Summary = txtSummary.Text.Trim();
            if (!string.IsNullOrEmpty(hidUpload.Value.Trim()))
            {
                es.ImageURL = hidUpload.Value.Trim();
            }
            es.ActBeginTime = DateTime.Parse(beginTime.Value.Trim());
            es.ActEndTime = DateTime.Parse(endTime.Value.Trim());
            es.BookBeginTime = DateTime.Parse(bookbeginTime.Value.Trim());
            es.BookEndTime = DateTime.Parse(bookendTime.Value.Trim());
            //pa.OrderNum = int.Parse(txtOrderNum.Text.Trim());
            es.PublishDept = txtPublishDept.Text.Trim();
            es.ActWay = txtActWay.Value.Trim();
            es.Creator = DZAFCPortal.Utility.UserInfo.Account;
            return es;
        }

        NavigateService ns = new NavigateService();
        private UMS_Message CreateUMS_Message(Activities es)
        {
            Navigator nv = ns.GenericService.First(p => p.Title.Equals("活动召集"));
            UMS_Message um = new UMS_Message();
            um.EngineType = EngineTypeEnum.WeChat.ToString();
            um.To = "@all";//默认为向关注微信的全部成员发送
            um.Subject = es.Name;
            um.Body = es.Summary;
            um.State = MessageStateEnum.Waiting.ToString();
            um.ErrorCount = 0;
            um.CreateTime = DateTime.Now;
            um.Url = "/StaffHome/WxNY_Home_eventDetail.aspx?TopNavId=" + nv.ParentID + "&CurNavId=" + nv.ID + "&id=" + es.ID;
            um.EstimateTime = DateTime.Now;
            um.Source = ApplicationEnum.职工之家.ToString();
            um.Images = es.ImageURL;
            return um;
        }
        #region 页面赋值
        public void EFamilyShow()
        {
            bool update = true;
            var item = eeSerivce.GenericService.GetModel(Request["ID"]);
            txtName.Text = item.Name;
            txtMaxPersonCount.Text = item.MaxPersonCount.ToString();
            txtActDescription.Value = item.ActDescription;
            txtSummary.Text = item.Summary;
            dropType.SelectedIndex = dropType.Items.IndexOf(dropType.Items.FindByValue(item.Type.ToString()));
            if (dropType.SelectedValue == "2")
            {
                trTeamMaxPerson.Visible = true;
            }
            if (!string.IsNullOrEmpty(item.ImageURL))
            {
                imgUpload.ImageUrl = item.ImageURL;
                imgUpload.Visible = true;
            }
            beginTime.Value = item.ActBeginTime.ToString("yyyy-MM-dd HH:mm");
            endTime.Value = item.ActEndTime.ToString("yyyy-MM-dd HH:mm");
            bookbeginTime.Value = item.BookBeginTime.ToString("yyyy-MM-dd HH:mm");
            bookendTime.Value = item.BookEndTime.ToString("yyyy-MM-dd HH:mm");
            //txtOrderNum.Text = pa.OrderNum.ToString();
            if (DateTime.Now > item.BookBeginTime)
            {
                update = true;
            }
            if (update)
            {
                txtTeamMaxPerson.Enabled = false;
                //txtMaxPersonCount.Enabled = false;
            }
            txtPublishDept.Text = item.PublishDept;
            txtActWay.Value = item.ActWay;
        }
        #endregion

        protected void dropType_SelectedIndexChanged(object sender, EventArgs e)
        {
            trTeamMaxPerson.Visible = dropType.SelectedValue == "2";
            txtTeamMaxPerson.Text = "0";
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

            string fileName = Path.GetFileName(fileIndexImgUrl.PostedFile.FileName);

            string uploadFileName = Guid.NewGuid() + "_" + fileName.Replace(" ", "");
            var savePath = "/Uploads/EFamilys/";
            string fileFullName = savePath + uploadFileName;
            string[] Extensions = { ".jpg",".png" };
            var result = Fxm.Utility.FileHelper.UploadToWeb(Extensions, fileIndexImgUrl.PostedFile, savePath, uploadFileName);



            //var result = FileHelper.UploadDocument(datas, fileFullName, DZAFCPortal.Config.AppSettings.SharePointSiteUrl, DZAFCPortal.Config.Base.ImageExtensions);
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
        }
    }
}