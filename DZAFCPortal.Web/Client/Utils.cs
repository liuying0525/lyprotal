using DZAFCPortal.Authorization.BLL;
using DZAFCPortal.Authorization.Entity;
using DZAFCPortal.Utility;
using System.Text.RegularExpressions;
using System.Web;

namespace DZAFCPortal.Web.Client
{
    public class Utils
    {
        public static string FormatUrl(object url)
        {
            return DZAFCPortal.Config.Base.ClientBasePath + url;
        }

        

        public static User CurrentUser
        {
            get
            {
                var curAcc = UserInfo.Account;
                var user = new UserAuthorizationBLL().GetUserByAccount(curAcc);

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
            var res = string.Empty;
            var arr = Regex.Split(aburl, "/_layouts/15", RegexOptions.IgnoreCase);
            if (arr.Length >= 2)
            {
                var url = arr[1];

                var module = new DZAFCPortal.Authorization.DAL.ModuleService().GenericService.FirstOrDefault(m => m.Url.Contains(url));

                if (module != null)
                    res = module.Name;
            }
            return res;
        }
    }
}