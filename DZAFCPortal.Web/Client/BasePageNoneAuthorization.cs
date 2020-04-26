using DZAFCPortal.Authorization.BLL;
using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;
using DZAFCPortal.Utility;
using System;

namespace DZAFCPortal.Web.Client
{
    public class BasePageNoneAuthorization: System.Web.UI.Page
    {
        public User CurrentUser
        {
            get
            {
                var account = UserInfo.Account;
                var user = new UserAuthorizationBLL().GetUserByAccount(account);

                return user;
            }
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            var user = CurrentUser;

            if (user == null)
            {
                var account = UserInfo.Account;

                Response.Write(String.Format("当前登录账号[{0}]未同步到当前系统，请联系管理员添加该账号到当前系统中！", account));
                Response.End();
            }
        }


        /// <summary>
        /// 顶部ID
        /// </summary>
        protected string TopNavId
        {
            get
            {
                var topNavId = Request["TopNavId"];
                return topNavId;
            }
        }

        /// <summary>
        /// 左侧ID
        /// </summary>
        protected string CurNavId
        {
            get
            {
                var curNavId = Request["CurNavId"];
                return curNavId;
            }
        }

        UserService userService = new UserService();
        public string GetDisplayNameByAccount(string account)
        {
            return userService.getUserName(account);
        }
    }
}