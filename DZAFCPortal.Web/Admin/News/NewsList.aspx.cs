using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.News
{
    public partial class NewsList : BasePage
    {
        public string codeId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                LiteralSiteMap.Text = Utils.GenerateTitle(Request.Url.AbsoluteUri);
                //BindRelatedNav();
                GetDataAndBind("");
                DZAFCPortal.Authorization.Web.RepeaterHelper.RepeaterAuthorization(rptSlider);
            }
        }

        #region--------Service 私有变量---------

        NewsService dynamicService = new NewsService();
        NewsCategoryService categoryService = new NewsCategoryService();

        private NavigateService navService = new NavigateService();
        #endregion

        #region--------页面控件的响应事件-----------


        /// <summary>
        /// 执行列表控件执行Command
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptSlider_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var cmdName = e.CommandName.Trim();

            switch (cmdName)
            {
                //case "Edit": ShowEditPage(e.CommandArgument.ToString()); break;
                case "Delete": Delete(e.CommandArgument.ToString()); break;
            }
        }
        #endregion

        #region------------私有方法----------
        private void Delete(string id)
        {
            dynamicService.GenericService.Delete(id);
            dynamicService.GenericService.Save();

            //删除后重新绑定数据
            GetDataAndBind("");
        }

        /// <summary>
        /// 获取数据，并绑定到列表中
        /// </summary>
        private void GetDataAndBind(string Name)
        {
            System.Linq.Expressions.Expression<Func<DZAFCPortal.Entity.News, bool>> predicate = p => true;
            string code = Request["CategoryCode"];
            if (!string.IsNullOrEmpty(code))
            {
                var cate = categoryService.GenericService.GetAll(p => p.Code == code).ToList().First();
                predicate = predicate.And(p => p.CategoryID == cate.ID);
                codeId = cate.ID;
            }
            if (!string.IsNullOrEmpty(Name))
            {
                predicate = predicate.And(p => p.Title.Contains(Name));
            }
            //if (new Guid(dropType.SelectedValue) != Guid.Empty)
            //{
            //    predicate = predicate.And(p => p.CategoryID == new Guid(dropType.SelectedValue));
            //}
            var navs = dynamicService.GenericService.GetAll(predicate).OrderByDescending(p => p.OrderNum).ThenByDescending(p => p.CreateTime).ToList();
            rptSlider.DataSource = navs;
            rptSlider.DataBind();
        }

        #endregion

        public string Type(string id)
        {
            var navs = categoryService.GenericService.GetAll(n => n.ID == id).First();
            return navs.Name;
        }


        private void BindRelatedNav()
        {
            //var navs = categoryService.GenericService.GetAll().ToList();
            //var source = new Dictionary<Guid, string>();
            //source.Add(Guid.Empty, "=所有=");
            //navs.ForEach(n =>
            //{
            //    source.Add(n.ID, n.Name);
            //});

            //dropType.DataSource = source;
            //dropType.DataTextField = "value";
            //dropType.DataValueField = "key";

            //dropType.DataBind();
        }

        protected void btnSerach_Click(object sender, EventArgs e)
        {
            GetDataAndBind(txtName.Text.Trim());
        }

        public string GenerateContentUrl(string contentId)
        {
            var dynamic = dynamicService.GenericService.GetModel(contentId);
            var category = categoryService.GenericService.GetModel(dynamic.CategoryID);
            var navigate = navService.GenericService.GetAll(p => p.Url.Contains(category.Code)).First();
            return DZAFCPortal.Config.Base.ClientBasePath + "/Pages/Content.aspx?TopNavId=" + navigate.ParentID + "&CurNavId=" + navigate.ID + "&ContentId=" + contentId;
        }
    }
}