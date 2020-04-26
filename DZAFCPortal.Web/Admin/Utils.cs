using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DZAFCPortal.Web.Admin
{
    public class Utils
    {
        public static string FormatUrl(object url)
        {
            return DZAFCPortal.Config.Base.ClientBasePath + url;
        }


        public static DZAFCPortal.Authorization.Entity.User CurrentUser
        {
            get
            {
                
                var user = new DZAFCPortal.Authorization.BLL.UserAuthorizationBLL().GetUserByAccount(DZAFCPortal.Utility.UserInfo.Account);

                if (user == null)
                {
                    HttpContext.Current.Response.Write("当前登录用户未同步到服务器中，请联系管理员!");
                    HttpContext.Current.Response.End();
                }

                return user;
            }
        }

        /// <summary>
        /// 生成后台顶部站点地图
        /// </summary>
        /// <param name="aburl"></param>
        /// <returns></returns>
        public static string GenerateTitle(string aburl)
        {
            //var abpath = Request.Url.AbsolutePath;
            var res = string.Empty;
            var arr = Regex.Split(aburl, "/_layouts/15/NYAdmin", RegexOptions.IgnoreCase);
            var url = arr.Length >= 2 ? arr[1] : arr[0];

            var temp = "{0} &gt; {1}";
            var module = new DZAFCPortal.Authorization.DAL.ModuleService().GenericService.FirstOrDefault(m => m.Url.Contains(url));

            if (module != null)
            {
                var mg = new DZAFCPortal.Authorization.DAL.ModuleGroupService().GenericService.GetModel(module.ModuleGroup_ID);
                res = string.Format(temp, mg.Name, module.Name);
            }

            return res;
        }
    }
}