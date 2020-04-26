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
    public partial class SetRole : BasePage
    {
        protected RoleUserService roleUserService = new RoleUserService();
        protected RoleService roleService = new RoleService();
        protected UserService userService = new UserService();


        public string UserID
        {
            get
            {
                return Request["ID"];
            }
        }
        public string search;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    var user = userService.GenericService.GetModel(UserID);
                    lblUserAccount.Text = user.Account;
                    lblUserName.Text = user.DisplayName;

                    //默认账号，不可编辑
                    if (DZAFCPortal.Config.AppSettings.DefaultAccounts.Contains(user.Account))
                    {
                        panel.Enabled = false;
                    }

                    Bind();
                    InitChecked(UserID);
                }
                catch
                {
                    Response.Write("<hr title='页面出现问题，请联系管理员。' />");
                    Response.End();
                }

            }

        }

        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        private void Bind()
        {
            cblRoles.DataSource = roleService.GenericService.GetAll(p => p.Code != "cjgly").OrderBy(r => r.OrderNum).ToList();
            cblRoles.DataTextField = "Name";
            cblRoles.DataValueField = "ID";
            cblRoles.DataBind();
        }

        /// <summary>
        /// 获取当前用户拥有的角色
        /// </summary>
        /// <param name="userID"></param>
        private void InitChecked(string userID)
        {
            List<string> roleUserIDs = roleUserService.GenericService.GetAll(r => r.UserID == userID).Select(p => p.RoleID).ToList();
            if (roleUserIDs.Count > 0)
            {
                foreach (ListItem item in cblRoles.Items)
                {
                    if (roleUserIDs.Contains(item.Value))
                    {
                        item.Selected = true;
                    }
                }
            }
        }

        /// <summary>
        /// 角色分派事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_Click(object sender, EventArgs e)
        {
            //删除全部关联
            List<RoleUser> roleUserRelationshipList = roleUserService.GenericService.GetAll(r => r.UserID == UserID).ToList();
            foreach (var relationship in roleUserRelationshipList)
            {
                roleUserService.GenericService.Delete(relationship);
            }
            roleUserService.GenericService.Save();

            //重新关联
            List<string> selectedStr = new List<string>();
            for (int i = 0; i < cblRoles.Items.Count; i++)
            {
                if (cblRoles.Items[i].Selected)
                {
                    selectedStr.Add(cblRoles.Items[i].Value);
                }
            }

            foreach (var roleid in selectedStr)
            {
                RoleUser relationshipToInsert = new RoleUser
                {
                    ID = Guid.NewGuid().ToString(),
                    OrganizationID = Guid.NewGuid().ToString(),
                    ModifyTime = DateTime.Now,
                    CreateTime = DateTime.Now,
                    IsDefault = true,
                    RoleID = roleid,
                    UserID = UserID
                };
                roleUserService.GenericService.Add(relationshipToInsert);
            }

            roleUserService.GenericService.Save();

            //Fxm.Utility.Page.JsHelper.CloseWindow(true, "数据保存成功！");
            search = Request["search"];
            Fxm.Utility.Page.MessageBox.Show("保存成功!");
            ClientScript.RegisterStartupScript(ClientScript.GetType(), "myscript", "<script>RefreshParent();</script>");
        }

    }
}