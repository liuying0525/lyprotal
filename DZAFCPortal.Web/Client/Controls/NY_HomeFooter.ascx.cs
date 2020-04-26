using DZAFCPortal.Authorization.BLL;
using DZAFCPortal.Authorization.Entity;
using DZAFCPortal.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Client.Controls
{
    public partial class NY_HomeFooter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Info();
            }
        }
        public User CurrentUser
        {
            get
            {
                var user = new UserAuthorizationBLL().GetUserByAccount(UserInfo.Account);

                if (user == null)
                {
                    Response.Write("当前登录用户未同步到服务器中，请联系管理员!");
                    Response.End();
                }

                return user;
            }
        }


        private void Info()
        {
            try
            {
                if (new DZAFCPortal.Authorization.DAL.RoleUserService().GenericService.GetAll(p => p.UserID == CurrentUser.ID).Count() > 0)
                {
                    hylAdmin.NavigateUrl = DZAFCPortal.Config.Base.AdminBasePath + "/Main.aspx";
                    hylAdmin.Visible = true;
                  
                }
                else
                {
                    hylAdmin.Visible = false;
                  
                }
            }
            catch
            {
                hylAdmin.Visible = false;
            }
        }
    }
}