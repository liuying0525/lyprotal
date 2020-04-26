using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using DZAFCPortal.Web.Admin;
using Fxm.Utility.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DZAFCPortal.Utility;

namespace NYPortal.Web.SMGPages.Links
{
    public partial class EditLink : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadItem();
            }
        }
        #region--------Service 私有变量---------
        IndexScrollService linkSerivce = new IndexScrollService();
        #endregion

        #region--------页面控件响应事件--------
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var item = CreateEditPageLink();

                linkSerivce.GenericService.Update(item);
                linkSerivce.GenericService.Save();

                Fxm.Utility.Page.JsHelper.CloseWindow(true, "保存成功！");
            }
            catch (Exception ex)
            {
                Fxm.Utility.Page.MessageBox.Show("保存过程发生错误，请重试!");
                //记录错误日志
                //记录错误日志
                NySoftland.Core.Log4.LogHelper.Error("编辑链接异常[EditLink]", ex);
            }
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Fxm.Utility.Page.JsHelper.CloseWindow(false);
        }


        #endregion

        #region-------------控件与实体之间赋值操作------------
        /// <summary>
        /// 加载信息
        /// </summary>
        private void LoadItem()
        {
            string type = Request["type"];
            var item = GetItemByID();
            txtName.Text = item.Name;
            txtShortName.Text = item.ShortName;
            imgUpload.ImageUrl = item.ImageUrl;
            imgUpload.Visible = true;
            if (type == "200")
            {
                imgUpload.Width = 194;
                imgUpload.Height = 92;
            }
            txtUrl.Text = item.LinkUlr;
            txtOrderNum.Text = item.OrderNum.ToString();
            if (type == "100")
            {
                txtCode.Text = item.Code;
                Code.Visible = true;
            }
            dropIsEnable.SelectedIndex = item.EnableState ? 0 : 1;
        }
        /// <summary>
        /// 通过页面控件，创建一个实体
        /// </summary>
        /// <returns></returns>
        private IndexScroll CreateEditPageLink()
        {
            var item = GetItemByID();

            item.Name = txtName.Text.Trim();
            item.ShortName = txtShortName.Text.Trim();
            item.ImageUrl = imgUpload.ImageUrl;
            item.LinkUlr = txtUrl.Text.Trim();
            item.OrderNum = int.Parse(txtOrderNum.Text.Trim());
            item.EnableState = bool.Parse(dropIsEnable.SelectedValue);

            return item;
        }
        #endregion

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
            var savePath = "/Uploads/Ny_Images/";
            string fileFullName = savePath + uploadFileName;
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
        }

        public string ImgSize()
        {
            int type = int.Parse(Request["type"]);
            string size = "";
            if (type == 200)
            {
                size = "width='300' height='100'";
            }
            else
            {
                size = "width='100' height='100'";
            }
            return size;
        }

        #region-------私有方法---------
        private IndexScroll GetItemByID()
        {
            var id = Request["ID"];
            var item = linkSerivce.GenericService.GetModel(id);
            if (item == null)
            {
                Response.Write("找不到该ID数据，可能已被删除！");
                Response.End();
            }

            return item;
        }
        #endregion
    }
}