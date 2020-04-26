
using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Roles
{
    public partial class EditRole : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                LoadItem();
            }
        }

        #region--------Service 私有变量---------
        DZAFCPortal.Authorization.DAL.RoleService roleSerivce = new DZAFCPortal.Authorization.DAL.RoleService();
        DZAFCPortal.Authorization.DAL.RoleUserService userRoleService = new DZAFCPortal.Authorization.DAL.RoleUserService();

        #endregion

        #region--------页面控件响应事件--------
        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var role = CreateRole();

                if (roleSerivce.IsRoleExsist(role.Name, role.ID, true))
                {
                    Fxm.Utility.Page.MessageBox.Show("该名称的角色已经存在，请修改");
                    return;
                }

                roleSerivce.GenericService.Update(role);
                roleSerivce.GenericService.Save();
            }
            catch (Exception ex)
            {
                Fxm.Utility.Page.MessageBox.Show("保存过程发生错误，请重试!");
                //记录错误日志
                NySoftland.Core.Log4.LogHelper.Error("编辑角色异常[EditRole.aspx]", ex);
            }

            Fxm.Utility.Page.JsHelper.CloseWindow(true, "数据保存成功！");
        }


        #endregion


        #region-------私有方法---------
        private Role CreateRole()
        {
            var item = roleSerivce.GenericService.GetModel(Request["ID"]);

            item.Name = txtName.Text.Trim();
            item.IsEnable = true;
            item.IsDelete = true;
            item.OrderNum = int.Parse(txtSortNum.Text);
            item.Description = txtRemark.Text.Trim();
            item.RoleTypes = int.Parse(dropRoleTypes.SelectedValue);
            SaveRole(item.ID);
            return item;
        }


        /// <summary>
        /// 页面载入时加载数据
        /// </summary>
        private void LoadItem()
        {
            var item = roleSerivce.GenericService.GetModel(Request["ID"].Trim());

            if (item != null)
            {
                txtName.Text = item.Name;

                txtRemark.Text = item.Description;

                txtCode.Text = item.Code;

                txtSortNum.Text = item.OrderNum.ToString();

                dropRoleTypes.SelectedValue = item.RoleTypes.ToString();

                //加载已有用户
                var layoutUsers = userRoleService.GenericService.GetAll(p => p.RoleID == item.ID).Select(p => p.UserID).ToList();
                if (layoutUsers.Count() > 0)
                {
                    MutiUserChooseControl.CheckedUserIds = String.Join(",", layoutUsers);
                }
                else MutiUserChooseControl.CheckedUserIds = String.Empty;

                //判断默认账号不允许编辑
                if (DZAFCPortal.Config.AppSettings.DefaultRoles.Contains(item.Code))
                {
                    panel.Enabled = false;
                }
            }
            else
            {
                throw new Exception("数据加载错误！该数据可能已被删除！");
            }
        }
        #endregion


        public void SaveRole(string RoleID)
        {
            List<RoleUser> roleUserRelationshipList = userRoleService.GenericService.GetAll(r => r.RoleID == RoleID).ToList();
            foreach (var relationship in roleUserRelationshipList)
            {
                userRoleService.GenericService.Delete(relationship);
            }
            userRoleService.GenericService.Save();
            var layoutUserIds = MutiUserChooseControl.CheckedUserIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            //重新关联
            List<string> selectedStr = new List<string>();
            foreach (var id in layoutUserIds.Distinct())
            {
                selectedStr.Add(id);
            }

            foreach (var userid in selectedStr)
            {
                RoleUser relationshipToInsert = new RoleUser
                {
                    ID = Guid.NewGuid().ToString(),
                    OrganizationID = Guid.NewGuid().ToString(),
                    ModifyTime = DateTime.Now,
                    CreateTime = DateTime.Now,
                    IsDefault = true,
                    RoleID = RoleID,
                    UserID = userid
                };
                userRoleService.GenericService.Add(relationshipToInsert);
            }

            userRoleService.GenericService.Save();
        }
    }
}