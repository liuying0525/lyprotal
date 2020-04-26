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
    public partial class HighlightAdd : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string id = Request["id"];
                if (!string.IsNullOrEmpty(id))
                {
                    AuctionShow();
                }
                else 
                {
                    string account = DZAFCPortal.Utility.UserInfo.Account;
                    //txtPublishDept.Text = userService.GenericService.GetAll(p => p.Account == account).First().Department;
                    txtPublishDept.Text = "东正金融";
                }
            }
        }
        HighlightsService auctionService = new HighlightsService();
        UMS_MessageService umsSercice = new UMS_MessageService();

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string id = Request["id"];
            try
            {
                var auction = CreateNewPrefer();
                if (!string.IsNullOrEmpty(id))
                {
                    auctionService.GenericService.Update(auction);
                }
                else
                {
                    auctionService.GenericService.Add(auction);
                    UMS_Message um = CreateUMS_Message(auction);
                    umsSercice.AddMessage(um);
                }
                auctionService.GenericService.Save();
                //Fxm.Utility.Page.MessageBox.Show("保存成功!");
                //ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>RefreshParent();</script>");
                Fxm.Utility.Page.JsHelper.CloseWindow(false, "数据保存成功！", "refresh();");
            }
            catch (Exception ex)
            {
                Fxm.Utility.Page.MessageBox.Show("保存过程发生错误，请重试!");

                //记录错误日志
                NySoftland.Core.Log4.LogHelper.Error("活动召集添加异常[PreferentActionAdd.aspx]", ex);
            }
        }

        private DZAFCPortal.Entity.Highlights CreateNewPrefer()
        {
            DZAFCPortal.Entity.Highlights ai = new DZAFCPortal.Entity.Highlights();
            if (!string.IsNullOrEmpty(Request["ID"]))
            {
                ai = auctionService.GenericService.GetModel(Request["ID"]);
            }
            ai.AuctionName = txtName.Text.Trim();
            if (!string.IsNullOrEmpty(hidUpload.Value.Trim()))
            {
                ai.IndexImgUrl = hidUpload.Value.Trim();
            }
            ai.AuctionBeginTime = DateTime.Parse(beginTime.Value);
            ai.AuctionEndTime = DateTime.Parse(endTime.Value);
            ai.Summary = txtSummary.Text.Trim();
            ai.ItemSummary = txtItemSummary.Value;
            ai.PublishDept = txtPublishDept.Text.Trim();
            ai.Creator = DZAFCPortal.Utility.UserInfo.Account;
            return ai;
        }

        NavigateService ns = new NavigateService();
        private UMS_Message CreateUMS_Message(Highlights es)
        {
            Navigator nv = ns.GenericService.First(p => p.Title.Equals("活动风采"));
            UMS_Message um = new UMS_Message();
            um.EngineType = EngineTypeEnum.WeChat.ToString();
            um.To = "@all";//默认为向关注微信的全部成员发送
            um.Subject = es.AuctionName;
            um.Body = es.Summary;
            um.State = MessageStateEnum.Waiting.ToString();
            um.ErrorCount = 0;
            um.CreateTime = DateTime.Now;
            um.Url = "/StaffHome/WxNY_AuctionDetail.aspx?TopNavId=" + nv.ParentID + "&CurNavId=" + nv.ID + "&id=" + es.ID;
            um.EstimateTime = DateTime.Now;
            um.Source = ApplicationEnum.职工之家.ToString();
            um.Images = es.IndexImgUrl;
            return um;
        }

        public void AuctionShow()
        {
            var item = auctionService.GenericService.GetModel(Request["ID"]);
            txtName.Text = item.AuctionName;
            txtSummary.Text = item.Summary;
            txtItemSummary.Value = item.ItemSummary;
            if (!string.IsNullOrEmpty(item.IndexImgUrl))
            {
                imgUpload.ImageUrl = item.IndexImgUrl;
                imgUpload.Visible = true;
            }
            beginTime.Value = item.AuctionBeginTime.ToString("yyyy-MM-dd HH:mm");
            endTime.Value = item.AuctionEndTime.ToString("yyyy-MM-dd HH:mm");
            txtPublishDept.Text = item.PublishDept;
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

            string uploadFileName = Guid.NewGuid() + "_" + fileName;
            var savePath = "/Uploads/EFamilys/";
            string fileFullName = savePath + uploadFileName.Replace(" ", "");
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
        }
    }
}