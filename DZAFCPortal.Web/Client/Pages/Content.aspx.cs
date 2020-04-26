using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using DZAFCPortal.Service.CMS;
using DZAFCPortal.Authorization.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using DZAFCPortal.Utility;

namespace DZAFCPortal.Web.Client.Pages
{
    public partial class Content : AuthrizedBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //统计访问量
                ViewNumAdd();
                //统计访问信息
                InfoAccessAdd();

                BindFields();
            }
        }

        #region Service Definition
        private NewsService dynamicService = new NewsService();
        private NewsCategoryService categoryService = new NewsCategoryService();
        //private Content_AttachService attachService = new Content_AttachService();
        private NavigateService navService = new NavigateService();
        InforAccessService infoAccessService = new InforAccessService();
        UserService userService = new UserService();
        #endregion

        #region PageEvent Handle
        protected void rptAttachList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var index = e.Item.ItemIndex + 1;
                (e.Item.FindControl("lblAttachIndex") as Label).Text = "附件" + index.ToString() + ":";

            }

        }
        #endregion

        #region ResourceGeneration & Binding

        /// <summary>
        /// 获取附件
        /// </summary>
        /// <param name="contentId"></param>
        /// <returns></returns>
        List<Attach> GenerateAttachByContentId(string contentId)
        {
            return new AttachService().GenericService.GetAll(a => a.ContentID == contentId).ToList();
        }

        /// <summary>
        /// 绑定附件
        /// </summary>
        void BindAttach(string ContentId)
        {
            var attachs = GenerateAttachByContentId(ContentId);
            rptAttachList.DataSource = attachs;
            rptAttachList.DataBind();

            if (attachs.Count == 0)
            {
                rptAttachList.Visible = false;
                fileUl.Visible = false;
            }
        }

        /// <summary>
        /// 绑定站点导航字段
        /// </summary>
        void BindFields()
        {
            string ContentId = Request["ContentId"] == null ? string.Empty : Request["ContentId"];
            var model = dynamicService.GenericService.GetModel(ContentId);

            //if (model.CheckedState != 99)
            //{
            //    Response.Write("<h2 style='text-align:center'>您当前查看的对象没有被审批,请联系管理员进行审批~</h2>");
            //    Response.End();
            //}

            literalTitle.Text = model.Title;
            literalPublisher.Text = model.Publisher;
            literalPublishTime.Text = model.CreateTime.ToString("yyyy-MM-dd HH:mm");

            //正文
            lblContent.Text = model.Content;

            //访问量
            literalNum.Text = model.ViewNum.ToString();
            //附件
            BindAttach(ContentId);
        }

        #endregion


        /// <summary>
        /// 统计访问量
        /// </summary>
        private void ViewNumAdd()
        {
            string ContentId = Request["ContentId"] == null ? string.Empty : Request["ContentId"];
            var model = dynamicService.GenericService.GetModel(ContentId);
            model.ViewNum = model.ViewNum + 1;
            dynamicService.GenericService.Update(model);
            dynamicService.GenericService.Save();
        }

        [WebMethod]
        private void InfoAccessAdd()
        {
            string ContentId = Request["ContentId"] == null ? string.Empty : Request["ContentId"];
            string AttachName = Request["attachName"] == null ? string.Empty : Request["attachName"];
            var model = dynamicService.GenericService.GetModel(ContentId);
            InforAccess info = new InforAccess();
            info.Title = model.Title;
            info.AccessName = Utils.CurrentUser.DisplayName;
            string account = UserInfo.Account;
            info.AccessDepartment = userService.GenericService.GetAll(p => p.Account == account).FirstOrDefault().OrganizationName;
            info.Type = 1;
            info.CategoryID = model.CategoryID;
            info.AttachName = "";
            infoAccessService.GenericService.Add(info);
            infoAccessService.GenericService.Save();
        }

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        #region Extension Method
        #endregion


        public string RemoveGuid(string name)
        {
            int num = name.IndexOf('_');
            string name2 = name.Substring(0, num + 1);
            name = name.Replace(name2, "");
            return name;
        }

        public string downUrl(string url)
        {
            return "/_layouts/15/download.aspx?SourceUrl=" + Server.UrlEncode(url);
        }
    }
}