using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DZAFCPortal.Authorization.BLL;
using System.IO;
using DZAFCPortal.Utility;

namespace DZAFCPortal.Web.Admin.News
{
    public partial class NewsEdit : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //指定附件类型
            UploadAttach.AttachType = DZAFCPortal.Config.Enums.AttachType.信息发布;

            if (!Page.IsPostBack)
            {
                if (!String.IsNullOrEmpty(Request["ID"]))
                {
                    BinddropNav();
                    LoadItem();
                    //dropType.Visible = false;
                    //checkboxType.Visible = false;
                    trType.Visible = false;
                }
                else
                {
                    BinddropNav();
                    BindRelatedNav();
                    //dropType.Visible = true;
                    checkboxType.Visible = true;
                    //txtPublisher.Text = NySoftland.Moss.Helper.GetCurrentDisplayName();
                    labPublisher.Text = DZAFCPortal.Facade.userDisplayName.UserDisplayName();
                    txtPulishTime.Text = DateTime.Now.ToString(DZAFCPortal.Config.Base.NewsDataFormate);
                    string codeid = Request["CategoryCode"];
                    dropType.SelectedValue = dropType.Items.FindByValue(codeid).Value;
                    labType.Text = dropType.Items[dropType.SelectedIndex].Text;
                }
            }
        }

        NewsService dynamicService = new NewsService();
        NewsCategoryService categoryService = new NewsCategoryService();
        private NavigateService navService = new NavigateService();
        UMS_MessageService umsSercice = new UMS_MessageService();
        #region---------控件与实体之间转换---------
        private DZAFCPortal.Entity.News GetNewItem(string checkValue)
        {

            var dynamic = new DZAFCPortal.Entity.News();
            dynamic.ID = Guid.NewGuid().ToString();

            dynamic.Title = txtTitle.Text.Trim();
            dynamic.CategoryID = checkValue;
            dynamic.Publisher = labPublisher.Text.Trim();
            dynamic.Content = Server.HtmlDecode(editorContent.InnerHtml);
            dynamic.CreateTime = Fxm.Utility.StringConvert.ToDate(txtPulishTime.Text).Value;
            //dynamic.Summary = txtSummary.Text.Trim();
            dynamic.OrderNum = int.Parse(txtOrderNum.Text.Trim());
            if (!string.IsNullOrEmpty(hidUpload.Value.Trim()))
            {
                dynamic.IndexImgUrl = hidUpload.Value.Trim();
            }
            return dynamic;

        }

        private DZAFCPortal.Entity.News GetEditItem(string checkValue)
        {
            var id = Request["ID"];

            var dynamic = dynamicService.GenericService.GetModel(id);
            dynamic.Title = txtTitle.Text.Trim();
            dynamic.Publisher = labPublisher.Text.Trim();
            dynamic.Content = Server.HtmlDecode(editorContent.InnerHtml);
            dynamic.CreateTime = Fxm.Utility.StringConvert.ToDate(txtPulishTime.Text).Value;
            //dynamic.Summary = txtSummary.Text.Trim();
            //dynamic.CategoryID = new Guid(dropType.SelectedValue);
            dynamic.CategoryID = checkValue;
            dynamic.OrderNum = int.Parse(txtOrderNum.Text.Trim());
            if (!string.IsNullOrEmpty(hidUpload.Value.Trim()))
            {
                dynamic.IndexImgUrl = hidUpload.Value.Trim();
            }
            return dynamic;
        }

        private void LoadItem()
        {
            var id = Request["ID"];
            var dynamic = dynamicService.GenericService.GetModel(id);

            if (dynamic == null)
            {
                Response.Write("未能获取数据，该数据可能已被删除！");
                Response.End();
            }

            if (!string.IsNullOrEmpty(dynamic.IndexImgUrl))
            {
                imgUpload.ImageUrl = dynamic.IndexImgUrl;
                imgUpload.Visible = true;
            }

            txtTitle.Text = dynamic.Title;
            labPublisher.Text = dynamic.Publisher;
            editorContent.InnerText = dynamic.Content;
            txtPulishTime.Text = dynamic.CreateTime.ToString(DZAFCPortal.Config.Base.NewsDataFormate);
            //txtSummary.Text = dynamic.Summary;
            dropType.SelectedValue = dropType.Items.FindByValue(dynamic.CategoryID).Value;
            txtOrderNum.Text = dynamic.OrderNum.ToString();
            //checkboxType.SelectedValue = checkboxType.Items.FindByValue(dynamic.CategoryID).Value;
            labType.Text = dropType.Items[dropType.SelectedIndex].Text;
            //加载附件
            UploadAttach.GetAttachAndBind(id);
        }
        #endregion

        private void Save()
        {
            try
            {
                if (!String.IsNullOrEmpty(Request["ID"]))
                {
                    var item = GetEditItem(dropType.SelectedValue);
                    dynamicService.GenericService.Update(item);

                    //保存附件
                    UploadAttach.SaveAttach(item.ID);

                }
                else
                {
                    var item2 = GetNewItem(dropType.SelectedValue);
                    dynamicService.GenericService.Add(item2);

                    UMS_Message um = CreateUMS_Message(item2);
                    if (um != null)
                    {
                        umsSercice.AddMessage(um);
                    }
                    //保存附件
                    UploadAttach.SaveAttach(item2.ID);
                    for (int i = 0; i < checkboxType.Items.Count; i++)
                    {
                        if (checkboxType.Items[i].Selected)
                        {
                            var item = GetNewItem(checkboxType.Items[i].Value);
                            dynamicService.GenericService.Add(item);

                            //保存附件
                            UploadAttach.SaveAttach(item.ID);
                        }
                    }
                }
                dynamicService.GenericService.Save();
                Fxm.Utility.Page.JsHelper.CloseWindow(false, "数据保存成功！", "refresh();");
            }
            catch (Exception ex)
            {
                Fxm.Utility.Page.MessageBox.Show("发生未处理异常，请重试！");

                NySoftland.Core.Log4.LogHelper.Error("保存行内动态异常！", ex);
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
        NavigateService ns = new NavigateService();
        NewsCategoryService nc = new NewsCategoryService();
        private UMS_Message CreateUMS_Message(DZAFCPortal.Entity.News es)
        {
            NewsCategory ny = nc.GenericService.First(p => p.ID == es.CategoryID);

            Navigator nv = ns.GenericService.First(p => p.Title.Equals(ny.Name));
            UMS_Message um = new UMS_Message();
            //if (!ny.Name.Equals("培训信息") && !ny.Name.Equals("HR通知") && !ny.Name.Equals("职场氧吧"))
            //{
            um.EngineType = EngineTypeEnum.WeChat.ToString();
            um.To = "@all";//默认为向关注微信的全部成员发送
            um.Subject = es.Title;
            //um.Body = es.Content;
            um.State = MessageStateEnum.Waiting.ToString();
            um.ErrorCount = 0;
            um.CreateTime = DateTime.Now;
            um.Url = "/Pages/Content.aspx?TopNavId=" + nv.ParentID + "&CurNavId=" + nv.ID + "&ContentId=" + es.ID;
            DateTime dt = DateTime.Now;
            if (txtPulishTime.Text != null)
            {
                dt = DateTime.Parse(txtPulishTime.Text);
            }
            um.EstimateTime = dt;
            um.Source = ApplicationEnum.公司动态.ToString();
            if (!string.IsNullOrEmpty(es.IndexImgUrl))
            {
                um.Images = es.IndexImgUrl;
            }
            else
            {
                um.Images = ny.IndexImgUrl;
            }
            //}
            //else
            //{
            //    um = null;
            //}
            return um;
        }

        private void BindRelatedNav()
        {
            //var navs = categoryService.GenericService.GetAll().ToList();
            var navs = GetAuthorizationCategory();
            var source = new Dictionary<string, string>();

            navs.ForEach(n =>
            {
                source.Add(n.ID, n.Name);
            });
            string CategoryCode = Request["CategoryCode"];
            source.Remove(CategoryCode);
            checkboxType.DataSource = source;
            checkboxType.DataTextField = "value";
            checkboxType.DataValueField = "key";

            checkboxType.DataBind();
        }

        private void BinddropNav()
        {
            //var navs = categoryService.GenericService.GetAll().ToList();
            var navs = GetAuthorizationCategory();
            var source = new Dictionary<string, string>();

            navs.ForEach(n =>
            {
                source.Add(n.ID, n.Name);
            });

            dropType.DataSource = source;
            dropType.DataTextField = "value";
            dropType.DataValueField = "key";

            dropType.DataBind();
        }

        private List<NewsCategory> GetAuthorizationCategory()
        {
            var curAccount = Utils.CurrentUser.Account;

            var moduleNames = new UserAuthorizationBLL().GetUserModules(curAccount, "NyAdmin").Select(u => u.Name).Distinct();

            return categoryService.GenericService.GetAll(c => moduleNames.Contains(c.Name)).ToList();
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