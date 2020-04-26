
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;

namespace DZAFCPortal.Web.Admin.Authorization.Users
{
    public partial class AddUser : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            UserService userService = new UserService();
            User user = new User
            {
                Account = txtAccount.Text,
                ID = Guid.NewGuid().ToString(),
                FirstName = "xl",
                LastName = "zhan",
                DisplayName = txtDisplayName.Text,
               
                Address = txtAddress.Text,
                Email = txtEmail.Text,
                MobilePhone = txtMobile.Text,
                Gender = 1,
             
                Status = 1,
             
                CreateTime = DateTime.Now,
                ModifyTime = DateTime.Now
            };

            if (userService.IsUserExsist(user.Account, Guid.Empty.ToString()))
            {
                Fxm.Utility.Page.MessageBox.Show("该名称的账号已经存在，请修改");
                return;
            }

            userService.GenericService.Add(user);
            userService.GenericService.Save();
            Fxm.Utility.Page.JsHelper.CloseWindow(true,"数据保存成功！");
        }
    }
}