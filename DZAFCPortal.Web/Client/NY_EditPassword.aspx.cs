using DZAFCPortal.Authorization.BLL;
using DZAFCPortal.Authorization.Entity;
using DZAFCPortal.Utility;
using DZAFCPortal.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Client
{
    public partial class NY_EditPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrentUser != null)
                {
                    InitPersonalInfo();
                }
            }
        }

        private void InitPersonalInfo()
        {
            labAccount.Text = CurrentUser.Account;
            labName.Text = CurrentUser.DisplayName;

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

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Fxm.Utility.Page.MessageBox.Show("功能咱不可用!");
                //var adhelper = new CommonADHelper(AppSettings.LDAPPath, AppSettings.LDAPAccount, AppSettings.LDAPPwd);

                //string account = CurrentUser.Account;
                //string Password = txtNewPassword.Text.Trim();
                //adhelper.ModifyPassword(account, Password);
                //Fxm.Utility.Page.MessageBox.Show("保存成功");
                //ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>closeWin();</script>");
            }
            catch (Exception ex)
            {
                Fxm.Utility.Page.MessageBox.Show("保存过程发生错误，请重试!");
                //Fxm.Utility.Page.MessageBox.Show("保存异常,异常信息:" + ex.Message.ToString());
            }
        }
    }
}