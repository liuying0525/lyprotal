using DZAFCPortal.Entity;
using DZAFCPortal.Service;
using DZAFCPortal.Utility;
using Newtonsoft.Json;
using NySoftland.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.SqlServer;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DZAFCPortal.Web.Client.AjaxPage
{
    /// <summary>
    /// IndexHandler 的摘要说明
    /// </summary>
    public class IndexHandler : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {

            var op = context.Request.Params["op"];
            var json = string.Empty;
            switch (op)
            {
                case "Init":
                    json = InitData();
                    break;
                default:
                    break;
            }

            context.Response.ContentType = "text/plain";
            context.Response.Write(json);
        }

        string InitData()
        {
            var result = new Result();
            var navService = new NavigateService();

            #region 职工之家
            var nav_staff_home = navService.GenericService.FirstOrDefault(p => p.Url.Contains("HighlightList.aspx") && p.ParentID != ConstValue.EMPTY_GUID_STR);
            HighlightsService highlightService = new HighlightsService();
            var highlights = highlightService.GenericService
                                             .GetAll()
                                             .OrderByDescending(h => h.ModifyTime)
                                             .Take(5)
                                             .ToList()
                                             .Select(h => new
                                             {
                                                 title = h.AuctionName,
                                                 url = $"/Client/StaffHome/HighlightDetail.aspx?TopNavId={nav_staff_home.ParentID}&CurNavId={nav_staff_home.ID}&id={h.ID}",
                                                 modify_time = h.ModifyTime.ToString("yyyy年MM月dd日")
                                             });

            var highlights_more_url = $"/Client/StaffHome/HighlightList.aspx?TopNavId={nav_staff_home.ParentID}&CurNavId={nav_staff_home.ID}";
            #endregion

            #region 热点新闻
            NewsService newsService = new NewsService();
            var categoryService = new NewsCategoryService();
            var category = categoryService.GenericService.FirstOrDefault(c => c.Code == "GSXW");
            var nav_company_news = navService.GenericService.FirstOrDefault(p => p.Url.Contains("GSXW") && p.ParentID != ConstValue.EMPTY_GUID_STR);

            var news = newsService.GenericService
                                  .GetAll(n => n.IsEnable && n.CategoryID == category.ID)
                                  .OrderByDescending(h => h.ModifyTime)
                                  .Take(3)
                                  .ToList()
                                  .Select(h => new
                                  {
                                      title = h.Title,
                                      image_url = h.IndexImgUrl,
                                      url = $"/Client/Pages/Content.aspx?TopNavId={nav_company_news.ParentID}&CurNavId={nav_company_news.ID}&ContentId={h.ID}",
                                      modify_time = h.ModifyTime.ToString("yyyy年MM月dd日")
                                  });

            var news_more_url = $"/Client/Pages/SecondaryList.aspx?CategoryCode=GSXW&TopNavId={nav_company_news.ParentID}&CurNavId={nav_company_news.ID}";

            #endregion

            #region 日历信息

            //var month_abbr = DateTime.Now.ToString("MMM", CultureInfo.InvariantCulture);
            //var day_num = DateTime.Now.ToString("dd");
            //var date_full = DateTime.Now.ToString("yyyy年MM月dd日");
            //var week = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek);

            //农历日期
            string[] DayName =   {"*","初一","初二","初三","初四","初五",
             "初六","初七","初八","初九","初十",
             "十一","十二","十三","十四","十五",
             "十六","十七","十八","十九","二十",
             "廿一","廿二","廿三","廿四","廿五",
             "廿六","廿七","廿八","廿九","三十"};

            //农历月份
            string[] MonthName = { "*", "正", "二", "三", "四", "五", "六", "七", "八", "九", "十", "十一", "腊" };


            ChineseLunisolarCalendar ChineseCalendar = new ChineseLunisolarCalendar();
            int chinese_year = ChineseCalendar.GetYear(DateTime.Now);
            int chinese_day = ChineseCalendar.GetDayOfMonth(DateTime.Now);
            int chinese_month = ChineseCalendar.GetMonth(DateTime.Now);

            #endregion

            result.Datas = new
            {
                StaffHome = new
                {
                    more_url = highlights_more_url,
                    data = highlights
                },
                News = new
                {
                    more_url = news_more_url,
                    data = news
                },
                DateInfo = new
                {
                    //月份英文缩写
                    month_abbr = DateTime.Now.ToString("MMM", CultureInfo.InvariantCulture),
                    //日期天数
                    day_num = DateTime.Now.ToString("dd"),
                    //日期全称
                    date_full = DateTime.Now.ToString("yyyy年MM月dd日"),
                    //星期
                    week = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(DateTime.Now.DayOfWeek),
                    //农历
                    chinese_date = $"{MonthName[chinese_month]}月{DayName[chinese_day]}",
                }
            };

            result.IsSucess = true;
            return JsonConvert.SerializeObject(result);

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}