using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;

namespace DZAFCPortal.Utility
{
    public class UserInfo : Page
    {
        public static string Account
        {
            get
            {
                var cookie = HttpContext.Current.Request.Cookies[ConstValue.COOKIE_KEY_CURRENT_LOGIN_USER];

                if (cookie == null)
                {
                    var cur_request = HttpContext.Current.Request.Url.PathAndQuery;
                    var encode_url = HttpUtility.UrlEncode(cur_request);

                    HttpContext.Current.Response.Redirect("/login.html?redirect_url=" + encode_url, true);
                    return null;
                }
                else
                    return cookie.Value;
                //return "spadmin";
            }
        }
    }
}
