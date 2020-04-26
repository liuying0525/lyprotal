using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Client
{
    public partial class CompanyNews : System.Web.UI.Page
    {
        public string CategoryCode
        {
            get
            {
                var code = Request["Category"];
                if (string.IsNullOrEmpty(code))
                {
                    Response.Write("参数异常");
                    Response.End();
                }

                return code;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindContents();
            }
        }

        private NewsCategoryService categoryService = new NewsCategoryService();

        private NewsService contentService = new NewsService();

        /// <summary>
        /// 执行分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            BindContents();
        }
        protected void btnSeach_Click(object sender, EventArgs e)
        {
            BindContents();
        }
        /// <summary>
        /// 根据类别ID获取内容数据源
        /// </summary>
        /// <param name="categoryId"></param>
        /// <returns></returns>
        List<News> GenerateSource()
        {
            if (string.IsNullOrEmpty(CategoryCode))
                return new List<News>();

            var category = categoryService.GenericService.FirstOrDefault(p => p.Code == CategoryCode);
            if (category == null)
                return new List<News>();

            int count = 0;
            System.Linq.Expressions.Expression<Func<News, bool>> predicate = p => p.CategoryID == category.ID;
            if (!string.IsNullOrEmpty(txtSearchKey.Value))
            {
                predicate = predicate.And(p => p.Title.Contains(txtSearchKey.Value.Trim()));
            }

            //var contentSource = contentService.GenericService.GetListByPage(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out count, predicate, p => p.CreateTime, true).ToList();
            var contentSourceCount = contentService.GenericService.GetAll(predicate).ToList();
            var contentSource = contentService.GenericService.GetAll(predicate)
                                                             .OrderByDescending(p => p.OrderNum)
                                                             .ThenByDescending(p => p.CreateTime)
                                                             .Skip((AspNetPager1.CurrentPageIndex - 1) * AspNetPager1.PageSize)
                                                             .Take(AspNetPager1.PageSize)
                                                             .ToList();
            //设置页数
            AspNetPager1.RecordCount = contentSourceCount.Count;

            return contentSource;
        }
        /// <summary>
        /// 绑定内容
        /// </summary>
        void BindContents()
        {
            var source = GenerateSource();

            rptContent.DataSource = source;
            rptContent.DataBind();
        }
        public string GenerateContentUrl(string contentId)
        {
            //var dynamic = contentService.GenericService.GetModel(new Guid(contentId));

            //var category = categoryService.GenericService.GetModel(new Guid(CategoryId));
            //if (category.IsContentNoneDetail)
            //    return dynamic.;

            // return "Content.aspx?TopNavId=" + TopNavId + "&CurNavId=" + CurNavId + "&ContentId=" + contentId;

            return "";
        }



        
    }
}