using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace DZAFCPortal.Config
{
    public class Base
    {
        public static string ClientBasePath
        {
            get
            {
                return "";
                //if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("/_layouts/"))
                //    return "/_layouts/15/NyClient";
                //else return "";
            }
        }

        public static string AdminBasePath
        {
            get
            {
                return "";
                //if (HttpContext.Current.Request.Url.AbsolutePath.ToLower().Contains("/_layouts/"))
                //    return "/_layouts/15/NyAdmin";
                //else return "";
            }
        }

        /// <summary>
        /// 资讯类时间的格式
        /// </summary>
        public static string NewsDataFormate
        {
            get
            {
                return "yyyy-MM-dd HH:mm:ss";
            }
        }

        public static string[] ImageExtensions
        {
            get
            {
                return new string[] { ".jpg", ".jpeg", ".png", ".bmp", ".gif" };
            }
        }

        public static string NONE_AUTHORITY
        {
            get
            {
                return "当前用户无访问该信息权限！";
            }
        }
    }
}
