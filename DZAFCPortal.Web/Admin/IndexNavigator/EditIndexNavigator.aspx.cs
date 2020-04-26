using DZAFCPortal.Entity;
using DZAFCPortal.Service;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace NYPortal.Web.Admin.IndexNavigator
{
    public partial class EditIndexNavigator : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //BindDropdownList();
                //trURL.Visible = false;
                string id = Request["id"];
                if (!string.IsNullOrEmpty(id))
                {
                    LoadItem();
                }


            }
        }

        #region--------Service 私有变量---------
        NY_IndexNavigatorService navService = new NY_IndexNavigatorService();
        #endregion

        #region--------页面控件响应事件--------
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Save();
            }
            catch (Exception ex)
            {
                Fxm.Utility.Page.MessageBox.Show("保存过程发生错误，请重试!");
                //记录错误日志
                NySoftland.Core.Log4.LogHelper.Error("首页滚动图保存出错！", ex);
            }

            Fxm.Utility.Page.JsHelper.CloseWindow(true, "数据保存成功！", "refresh");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Fxm.Utility.Page.JsHelper.CloseWindow(true);
        }

        #endregion

        #region-------私有方法---------
        private NY_IndexNavigation CreateEditNavigator()
        {
            var item = navService.GenericService.GetModel(Request["ID"]);

            item.Title = txtTitle.Text.Trim();
         
            item.OrderNum = int.Parse(txtOrderNum.Text.Trim());
       
          
  

            return item;
        }

        private NY_IndexNavigation CreateNewNavigator()
        {
            var item = new NY_IndexNavigation();
            item.ID = Guid.NewGuid().ToString();

            item.Title = txtTitle.Text.Trim();

            return item;
        }

        /// <summary>
        /// 页面载入时加载数据
        /// </summary>
        private void LoadItem()
        {
            var item = navService.GenericService.GetModel(Request["ID"].Trim());

            if (item != null)
            {
                txtTitle.Text = item.Title;
            }
            else
            {
                throw new Exception("数据加载错误！该数据可能已被删除！");
            }
        }

        private void Save()
        {
            string id = Request["id"];
            if (string.IsNullOrEmpty(id))
            {
                var item = CreateNewNavigator();
                navService.GenericService.Add(item);
                navService.GenericService.Save();
            }
            else 
            {
                var item = CreateEditNavigator();
                navService.GenericService.Update(item);
                navService.GenericService.Save();
            }
        }

    

        /// <summary>
        /// 获取导航位置
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GenerateFieldsTypes()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            foreach (var value in Enum.GetValues(typeof(NavEnum.IndexNavPosition)))
            {
                var v = (NavEnum.IndexNavPosition)value;
                dic.Add((int)v, v.ToString());
            }
            return dic;
        }

        #endregion

        protected void ddlURL_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }


        protected void btnUpload_Click(object sender, EventArgs e)
        {
            //byte[] uploadFileBytes =  //Convert.FromBase64String(Request[fileIndexImgUrl.PostedFile.FileName]);
            //var file = fileIndexImgUrl.PostedFile;
            //var datas = new byte[file.ContentLength];
            //var stream = file.InputStream;
            //stream.Read(datas, 0, file.ContentLength);

            //string uploadFileName = Guid.NewGuid() + "_" + System.IO.Path.GetFileName(fileIndexImgUrl.PostedFile.FileName);
            //var savePath = "/Uploads/Ny_Images/";
            //string fileFullName = savePath + uploadFileName;
            ////string[] Extensions = { ".jpg" };
            ////Result rt = Fxm.Utility.FileHelper.UploadToWeb(Extensions, fileIndexImgUrl.PostedFile, savePath, uploadFileName);
            //MossResult result = FileHelper.UploadDocument(datas, fileFullName, DZAFCPortal.Config.AppSettings.SharePointSiteUrl, DZAFCPortal.Config.Base.ImageExtensions);
            //if (result.IsSucess)
            //{
            //    imgUpload.ImageUrl = fileFullName;
            //    imgUpload.Visible = true;
            //    hidUpload.Value = fileFullName;
            //    Fxm.Utility.Page.MessageBox.Show(result.Message);
            //}
            //else
            //{
            //    Fxm.Utility.Page.MessageBox.Show(result.Message);
            //}
        }
    }
}