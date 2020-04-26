using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using DZAFCPortal.Facade;
using DZAFCPortal.Utility;

namespace DZAFCPortal.Web.Client
{
    public partial class Home : System.Web.UI.Page
    {
        #region Service Definition

        private NewsCategoryService categoryService = new NewsCategoryService();

        private NewsService dynamicService = new NewsService();

        private NavigateService navService = new NavigateService();

        private StatusFacade staService = new StatusFacade();

        private IndexScrollService linkService = new IndexScrollService();


        HighlightsService auctionSerivce = new HighlightsService();

        ActivitiesService eeSerivce = new ActivitiesService();

        OnlineVoteService onlineService = new OnlineVoteService();
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    //公司动态数据类别绑定
                    GetCategoryAndBind();
                    //首页滚动图片绑定
                    GetPictureAndBind();
                    //职工之家数据绑定
                    WorkHomeBind();
                    //职场氧吧
                    aZCYB.HRef = HomeURL(aZCYB.Name);
                    //安全生产
                    aAQGZ.HRef = HomeURL(aAQGZ.Name);
                    //党建工作
                    aDZBGZ.HRef = HomeURL(aDZBGZ.Name);
                    //公司动态More
                    aGSXW.HRef = URLMore("公司动态");
                    //职工直接More
                    aZGZJ.HRef = URLMore("职工之家");
                    ////报表中心
                    //aBBZX.HRef = HomeURL(aBBZX.Name);
                    ////文档中心
                    //aWDZX.HRef = HomeURL(aWDZX.Name);
                }
                catch (Exception ex)
                {

                }
            }
        }

        /// <summary>
        /// 拼写公司动态信息跳转地址
        /// </summary>
        /// <param name="contentId">信息ID</param>
        /// <returns></returns>
        public string GenerateContentUrl(string contentId)
        {
            var dynamic = dynamicService.GenericService.GetModel(contentId);
            var category = categoryService.GenericService.GetModel(dynamic.CategoryID);
            var navigate = navService.GenericService.GetAll(p => p.Url.Contains(category.Code)).FirstOrDefault();
            return "Pages/Content.aspx?TopNavId=" + navigate.ParentID + "&CurNavId=" + navigate.ID + "&ContentId=" + contentId;
        }

        /// <summary>
        /// 拼写职工之家信息跳转地址
        /// </summary>
        /// <param name="contentId">信息ID</param>
        /// <param name="url">url</param>
        /// <returns></returns>
        public string GenerateHomeUrl(string contentId, string url)
        {
            var navigate = navService.GenericService.FirstOrDefault(p => p.Url.Contains(url) && p.ParentID != ConstValue.EMPTY_GUID_STR);
            return DZAFCPortal.Config.Base.ClientBasePath + navigate.Url.Replace(".aspx", "Detail.aspx") + "?TopNavId=" + navigate.ParentID + "&CurNavId=" + navigate.ID + "&id=" + contentId;
        }

        /// <summary>
        /// 判断是否显示new图标
        /// </summary>
        /// <param name="Createtime">创建时间</param>
        /// <returns></returns>
        public bool Show(string Createtime)
        {
            return staService.Show(Createtime);
        }

        /// <summary>
        /// 公司动态数据绑定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void repResults_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                Repeater repSubject = e.Item.FindControl("rptContent") as Repeater;
                if (repSubject != null)
                {
                    var category = (NewsCategory)e.Item.DataItem;
                    System.Linq.Expressions.Expression<Func<News, bool>> predicate = p => p.CategoryID == category.ID;
                    predicate = predicate.And(p => p.CreateTime <= DateTime.Now);
                    var contentSource = dynamicService.GenericService.GetAll(predicate).OrderByDescending(p => p.OrderNum).ThenByDescending(p => p.CreateTime).Take(5).ToList();
                    repSubject.DataSource = contentSource;
                    repSubject.DataBind();
                }
            }
        }

        /// <summary>
        /// 公司动态数据类别绑定
        /// </summary>
        private void GetCategoryAndBind()
        {
            List<NewsCategory> category = categoryService.GenericService.GetAll(p => p.IsShowIndexArea).OrderBy(p => p.OrderNum).Take(4).ToList();
            repResults.DataSource = category.ToList();
            repResults.DataBind();
        }

        /// <summary>
        /// 首页滚动图片绑定
        /// </summary>
        private void GetPictureAndBind()
        {
            var linkregister = linkService.GenericService.GetAll(p => p.EnableState == true && p.PageLinkType == 200).OrderBy(p => p.OrderNum).Take(6).ToList();
            repHome.DataSource = linkregister;
            repHome.DataBind();
        }

        /// <summary>
        /// 首页职工之家数据绑定
        /// </summary>
        private void WorkHomeBind()
        {
            var auction = auctionSerivce.GetHighlights();
            var events = eeSerivce.GetHomeEvent();
            var online = onlineService.GetHomeOnline();
            auction.AddRange(events);
            auction.AddRange(online);
            rptHome.DataSource = auction.OrderByDescending(p => p.CreateTime).Take(5);
            rptHome.DataBind();
        }

        /// <summary>
        /// 首页公司动态第一条数据样式修改
        /// </summary>
        /// <param name="num">当前条数</param>
        /// <returns></returns>

        public string GetStyle(string num)
        {
            string Class = "ny_container_nynews_list_title_li";
            if (num == "1")
            {
                Class = "ny_container_nynews_list_title_li active";
            }
            return Class;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="num">当前条数</param>
        /// <returns></returns>
        public string GetStyle2(string num)
        {
            string Style = "display:none";
            if (num == "1")
            {
                Style = "";
            }
            return Style;
        }

        /// <summary>
        /// 职场氧吧，安全生产，党建工作地址拼接
        /// </summary>
        /// <param name="Code">编码</param>
        /// <returns></returns>
        public string HomeURL(string Code)
        {
            string url = "";
            try
            {
                var navigate = navService.GenericService.GetAll(p => p.Url.Contains(Code)).First();
                if (navigate != null)
                {
                    var baseUrl = DZAFCPortal.Config.Base.ClientBasePath + navigate.Url;
                    url = baseUrl + "&TopNavId=" + navigate.ParentID + "&CurNavId=" + navigate.ID;
                }
                return url;
            }
            catch
            {
                return url;
            }
        }

        /// <summary>
        /// 公司动态或职工直接More
        /// </summary>
        /// <returns></returns>
        public string URLMore(string Title)
        {
            string url = "";
            try
            {
                var navigate = navService.GenericService.GetAll(p => p.Title == Title).OrderBy(p => p.OrderNum).First();
                var navigate2 = navService.GenericService.GetAll(p => p.ParentID == navigate.ID).OrderBy(p => p.OrderNum).First();
                if (navigate2 != null)
                {
                    var baseUrl = DZAFCPortal.Config.Base.ClientBasePath + navigate2.Url;
                    if (baseUrl.Contains('?'))
                    {
                        url = baseUrl + "&TopNavId=" + navigate2.ParentID + "&CurNavId=" + navigate2.ID;
                    }
                    else
                    {
                        url = baseUrl + "?TopNavId=" + navigate2.ParentID + "&CurNavId=" + navigate2.ID;
                    }
                }
                return url;
            }
            catch
            {
                return url;
            }
        }

        /// <summary>
        /// 首页图片跳转地址处理
        /// </summary>
        /// <param name="Url">url地址</param>
        /// <returns></returns>
        public string ahref(string Url)
        {
            string backUrl = "";
            if (!string.IsNullOrEmpty(Url) && Url != "#")
            {
                backUrl = "href = '" + Url + "'";
            }
            else
            {
                backUrl = "style='text-decoration:none;cursor:default;'";
            }
            return backUrl;
        }


    }
}