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
    public partial class NY_EditUser : System.Web.UI.Page
    {
        DZAFCPortal.Authorization.DAL.UserService userService = new DZAFCPortal.Authorization.DAL.UserService();
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
            txtTelPhone.Text = CurrentUser.MobilePhone;
            txtPhone.Text = CurrentUser.OfficePhone;
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
                Fxm.Utility.Page.MessageBox.Show("功能暂不可用!");

                //var adhelper = new CommonADHelper(AppSettings.LDAPPath, AppSettings.LDAPAccount, AppSettings.LDAPPwd);
                //var entry = adhelper.GetUserByAccountName(UserInfo.Account);

                //if (entry == null)
                //{
                //    Fxm.Utility.Page.JsHelper.CloseWindow(true, "AD中无此用户,请联系管理员！");
                //    return;
                //}
                //entry.Properties["mobile"].Value = string.IsNullOrEmpty(txtTelPhone.Text) ? null : txtTelPhone.Text;
                //entry.Properties["telephoneNumber"].Value = string.IsNullOrEmpty(txtPhone.Text) ? null : txtPhone.Text;


                //var userToUpdate = CurrentUser;
                //userToUpdate.MobilePhone = txtTelPhone.Text.Trim();
                //userToUpdate.OfficePhone = txtPhone.Text.Trim();


                //entry.CommitChanges();

                //userService.GenericService.Update(userToUpdate);
                //userService.GenericService.Save();

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