using DZAFCPortal.Authorization.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DZAFCPortal.Authorization.Entity;

namespace DZAFCPortal.Web.Admin.Authorization.Users
{
    public partial class EidtUser : BasePage
    {
        protected UserService userService = new UserService();

        private string UserID
        {
            get
            {
                if (!string.IsNullOrEmpty(Request["ID"]))
                {
                    return Request["ID"];
                }
                return string.Empty;
            }
        }

        public string search;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                try
                {
                    User user = userService.GenericService.GetModel(UserID);
                    labAccount.Text = user.Account;
                    labName.Text = user.DisplayName;
                    labAddress.Text = user.Address;
                    labEmail.Text = user.Email;
                    labMobile.Text = user.MobilePhone;
                    //添加排序号是否开启
                    txtOrderNum.Text = user.SortNum.ToString();
                    dropIsEnable.SelectedIndex = user.IsShow ? 0 : 1;

      
                

                    ltrUp.Text = new DZAFCPortal.Authorization.BLL.UserAuthorizationBLL().GetOrganizationName(user.Account);

                    //默认账号，不可编辑
                    if (DZAFCPortal.Config.AppSettings.DefaultAccounts.Contains(user.Account))
                    {
                        panel.Enabled = false;
                    }
                }
                catch
                {
                    Response.Write("<hr title='页面出现问题，请联系管理员。' />");
                    Response.End();
                }

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            User userToUpdate = userService.GenericService.GetModel(UserID);
            userToUpdate.DisplayName = labName.Text;
            userToUpdate.Email = labEmail.Text;
            userToUpdate.MobilePhone = labMobile.Text;
            userToUpdate.Address = labAddress.Text;
            //添加排序号是否开启
            userToUpdate.SortNum = int.Parse(txtOrderNum.Text);
            userToUpdate.IsShow = bool.Parse(dropIsEnable.SelectedValue);


            userService.GenericService.Update(userToUpdate);
            userService.GenericService.Save();
            //Fxm.Utility.Page.JsHelper.CloseWindow(true, "数据保存成功！");
            search = Request["search"];
            Fxm.Utility.Page.MessageBox.Show("保存成功!");
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>RefreshParent();</script>");
        }
    }
}