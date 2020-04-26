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

namespace DZAFCPortal.Web.Admin.Categorys
{
    public partial class EditCategory : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                string id = Request["id"];
                if (!string.IsNullOrEmpty(id))
                {
                    LoadItem();
                }
            }
        }

        #region--------Service 私有变量--------
        NewsCategoryService categorySerivce = new NewsCategoryService();
        #endregion

        #region--------页面控件响应事件--------
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Fxm.Utility.Page.JsHelper.CloseWindow(true);
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUploadImg_Click(object sender, EventArgs e)
        {
            //var result = UploadLinkImg();
            //if (result.IsSucess)
            //{
            //    imgUrl.ImageUrl = result.Datas.ToString();
            //}
            //else AegonCMS.Utility.JsHelper.Alert(this, result.Message);
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnUpNoticeImg_Click(object sender, EventArgs e)
        {
            //var result = UploadNoticeImg();
            //if (result.IsSucess)
            //{
            //    imgNotice.ImageUrl = result.Datas.ToString();
            //}
            //else AegonCMS.Utility.JsHelper.Alert(this, result.Message);
        }

        #endregion

        #region-------页面控件与实体间赋值--------

        /// <summary>
        /// 更新类别实体数据
        /// </summary>
        private NewsCategory CreateEditCategory()
        {
            var item = categorySerivce.GenericService.GetModel(Request["ID"].Trim());

            item.Name = txtName.Text.Trim();
            item.OrderNum = int.Parse(txtOrderNum.Text.Trim());
            item.Code = txtCode.Text.Trim();
            item.IsShowIndexArea = ddlIsShowIndex.SelectedValue == "1";
            if (!string.IsNullOrEmpty(hidUpload.Value.Trim()))
            {
                item.IndexImgUrl = hidUpload.Value.Trim();
            }
            else
            {
                item.IndexImgUrl = "";
            }
            return item;
        }

        /// <summary>
        /// 新增类别实体数据
        /// </summary>
        private NewsCategory CreateNewCategory()
        {
            var item = new NewsCategory();
            item.ID = Guid.NewGuid().ToString();

            item.Name = txtName.Text.Trim();
            item.OrderNum = int.Parse(txtOrderNum.Text.Trim());
            item.Code = txtCode.Text.Trim();
            item.IsShowIndexArea = ddlIsShowIndex.SelectedValue == "1";
            if (!string.IsNullOrEmpty(hidUpload.Value.Trim()))
            {
                item.IndexImgUrl = hidUpload.Value.Trim();
            }
            else
            {
                item.IndexImgUrl = "";
            }
            return item;
        }


        /// <summary>
        /// 页面载入时加载数据
        /// </summary>
        private void LoadItem()
        {
            var item = categorySerivce.GenericService.GetModel(Request["ID"].Trim());


            if (item != null)
            {
                txtName.Text = item.Name;
                txtOrderNum.Text = item.OrderNum.ToString();
                txtCode.Text = item.Code.ToString();
                ddlIsShowIndex.SelectedValue = item.IsShowIndexArea ? "1" : "0";
                if (!string.IsNullOrEmpty(item.IndexImgUrl))
                {
                    imgUpload.ImageUrl = item.IndexImgUrl;
                    imgUpload.Visible = true;
                    hidUpload.Value = item.IndexImgUrl;
                }
            }
            else
            {
                throw new Exception("数据加载错误！该数据可能已被删除！");
            }
        }
        #endregion



        private NewsCategory GetCategory()
        {
            string id = Request["id"];
            if (string.IsNullOrEmpty(id))
            {
                return CreateNewCategory();
            }
            else
            {
                return CreateEditCategory();
            }
        }

        private void Save()
        {
            try
            {
                var category = GetCategory();
                string id = Request["id"];
                if (string.IsNullOrEmpty(id))
                {
                    categorySerivce.GenericService.Add(category);
                }
                else categorySerivce.GenericService.Update(category);

                categorySerivce.GenericService.Save();
            }
            catch (Exception ex)
            {
                Fxm.Utility.Page.MessageBox.Show("保存过程发生错误，请重试!");
                //记录错误日志
                NySoftland.Core.Log4.LogHelper.Error("类别保存出错！", ex);
            }

            Fxm.Utility.Page.JsHelper.CloseWindow(true, "数据保存成功！", "refresh");
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
            string fileFullName = savePath + uploadFileName;
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