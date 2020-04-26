using DZAFCPortal.Config;
using DZAFCPortal.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DZAFCPortal.Service
{
    public class CommonLinkService : BizGenericService<CommonLink>
    {
        

        /// <summary>
        /// 将电脑端的链接 Url 重写
        /// 当前重写规则为：如果以 /开头，则表示为当前系统，会判断是否为左侧菜单(Cms.NavCategory)中存在的页面
        /// 如果存在，则将URL重写为  /BaseLayouts
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string PCFormatUrl(string url)
        {
            //var navCategoryService = new Cms.NavCategoryService();

            //var formatUrl = string.Empty;

            ////表示为当前系统的链接
            //if (IsUrlCurrentSys(url))
            //{
            //    //判断链接是否为已配置的左侧菜单
            //    var navCategory = navCategoryService.GetAll(p => p.Url == url).FirstOrDefault();
            //    //表示左侧中存在
            //    if (navCategory != null)
            //    {
            //        formatUrl = NY_HomeLinkService.SiteBasePath + "/BaseLayouts.aspx?leftNavCodeParm=" + navCategory.Code;
            //        var channel = navCategory.NavChannel;

            //        //判断是否有参数
            //        if (formatUrl.IndexOf('?') > 0)
            //        {
            //            formatUrl += String.Format("&channelID={0}&channelCode={1}", channel.ID, channel.Code);
            //        }
            //        else
            //        {
            //            formatUrl += String.Format("?channelID={0}&channelCode={1}", channel.ID, channel.Code);
            //        }
            //    }
            //    else
            //    {
            //        if (!String.IsNullOrEmpty(SiteBasePath) && url.IndexOf(SiteBasePath) != 0)
            //        {
            //            formatUrl = SiteBasePath + url;
            //        }
            //        else formatUrl = url;
            //    }
            //}
            //else
            //{
            //    formatUrl = url;
            //}

            return url;
        }

        public static string PadFormatUrl(string url)
        {
            return PCFormatUrl(url);
        }

        public static string PhoneFormatUrl(string url)
        {
            if (IsUrlCurrentSys(url))
            {
                var formatUrl = url;

                if (!String.IsNullOrEmpty(Base.ClientBasePath) && url.IndexOf(Base.ClientBasePath) != 0)
                {
                    formatUrl = Base.ClientBasePath + url;
                }
                else formatUrl = url;

                return formatUrl;
            }
            else return url;
        }

        /// <summary>
        /// 判断当前链接是否为 本系统的
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsUrlCurrentSys(string url)
        {
            var rs = false;

            if (!String.IsNullOrEmpty(url) && url.StartsWith("/"))
            {
                rs = true;
            }

            return rs;
        }
    }
}
