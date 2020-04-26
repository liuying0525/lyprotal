using DZAFCPortal.Entity;
using DZAFCPortal.Facade;
using DZAFCPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Client.Pages
{
    public partial class SecondaryList : AuthrizedBase
    {
        public string ContentId
        {
            get
            {
                return Request["ContentId"] == null ? string.Empty : Request["ContentId"];
            }
        }

        /// <summary>
        /// 信息发布Code
        /// </summary>
        public string CategoryCode
        {
            get
            {
                return Request["CategoryCode"] == null ? string.Empty : Request["CategoryCode"];
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindContent();
                BindContents();
            }

        }

        #region Service Definition

        private NewsCategoryService categoryService = new NewsCategoryService();

        private NewsService dynamicService = new NewsService();

        private NavigateService navService = new NavigateService();

        private StatusFacade staService = new StatusFacade();

        #endregion

        #region PageEvent Handle

        /// <summary>
        /// 执行分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindContents();
        }
        #endregion

        #region ResourceGeneration & Binding
        /// <summary>
        /// 根据类别ID获取内容数据源
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        List<News> GenerateContentSource(string CategoryCode)
        {
            if (string.IsNullOrEmpty(CategoryCode))
                return new List<News>();

            string category_id = categoryService.GenericService.GetAll(p => p.Code == CategoryCode).First().ID;
            if (string.IsNullOrEmpty(category_id))
                return new List<News>();

            int count = 0;
            System.Linq.Expressions.Expression<Func<News, bool>> predicate = p => p.CategoryID == category_id;
            predicate = predicate.And(p => p.CreateTime <= DateTime.Now);

            //var contentSource = dynamicService.GenericService.GetListByPage(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out count, predicate, p => p.CreateTime, true).ToList();
            var contentSourceCount = dynamicService.GenericService.GetAll(predicate).ToList();
            var contentSource = dynamicService.GenericService.GetAll(predicate).OrderByDescending(p => p.OrderNum).ThenByDescending(p => p.CreateTime).Skip<News>((AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize)
             .Take<News>(AspNetPager1.PageSize).ToList();
            //设置页数
            AspNetPager1.RecordCount = contentSourceCount.Count;

            return contentSource;
        }

        /// <summary>
        /// 绑定内容
        /// </summary>
        void BindContents()
        {
            var source = GenerateContentSource(CategoryCode);

            rptContentTitles.DataSource = source;
            rptContentTitles.DataBind();
        }


        private void BindContent()
        {

            string category_id = categoryService.GenericService.GetAll(p => p.Code == CategoryCode).First().ID;
            System.Linq.Expressions.Expression<Func<News, bool>> predicate = p => p.CategoryID == category_id;
            predicate = predicate.And(p => p.CreateTime <= DateTime.Now);
            var contentSource = dynamicService.GenericService.GetAll(predicate).OrderByDescending(p => p.OrderNum).ThenByDescending(p => p.CreateTime).Take(10).ToList();
            rptContentTitle.DataSource = contentSource.ToList();
            rptContentTitle.DataBind();
        }
        #endregion

        #region Extension Method

        public string GenerateContentUrl(string contentId)
        {
            //var dynamic = dynamicService.GenericService.GetModel(new Guid(contentId));

            //var category = categoryService.GenericService.GetModel(new Guid(CategoryId));
            //if (category.IsContentNoneDetail)
            //    return dynamic.;

            return "Content.aspx?TopNavId=" + TopNavId + "&CurNavId=" + CurNavId + "&ContentId=" + contentId;
        }
        #endregion

        public bool Show(string Createtime)
        {
            return staService.Show(Createtime);
        }
    }
}