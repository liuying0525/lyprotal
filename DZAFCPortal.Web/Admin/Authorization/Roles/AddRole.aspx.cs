
using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Roles
{
    public partial class AddRole : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

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
                var role = CreateNewRole();

                if (roleSerivce.IsRoleExsist(role.Name, role.ID, false))
                {
                    Fxm.Utility.Page.MessageBox.Show("该名称的角色已经存在，请修改");
                    return;
                }

                if (roleSerivce.GenericService.FirstOrDefault(r => r.Code == txtCode.Text.Trim()) != null)
                {
                    Fxm.Utility.Page.MessageBox.Show("该名称的编号已经存在，请修改");
                    return;
                }

                roleSerivce.GenericService.Add(role);
                roleSerivce.GenericService.Save();
                SaveRole(role.ID);
            }
            catch (Exception ex)
            {
                Fxm.Utility.Page.MessageBox.Show("保存过程发生错误，请重试!");

                //记录错误日志
                NySoftland.Core.Log4.LogHelper.Error("新增角色异常[AddRole.aspx]", ex);
            }

            Fxm.Utility.Page.JsHelper.CloseWindow(true, "数据保存成功！");
        }
        #endregion


        #region-------私有方法---------
        private Role CreateNewRole()
        {
            var name = txtName.Text.Trim();
            //var firstLetter = Fxm.Utility.PinYin.GetFirstLetter(name);
            var firstLetter = txtCode.Text.Trim();
            var recordEqual = roleSerivce.GenericService.GetAll(r => r.Code == firstLetter);
            var recordLike = roleSerivce.GenericService.GetAll(r => r.Code.Contains(firstLetter));

            var item = new Role();

            item.ID = Guid.NewGuid().ToString();
            item.Name = name;
            if (recordEqual == null || recordEqual.Count() == 0)
            {
                item.Code = firstLetter;
            }
            else if (recordLike.Count() == 1)
            {
                item.Code = firstLetter + "001";
            }
            else
            {
                var code = recordLike.OrderByDescending(r => r.Code).FirstOrDefault().Code;
                var number = int.Parse(code.Substring(1)) + 1;
                item.Code = number.ToString().PadLeft(4, '0');
            }
            //item.Code = "code";
            item.IsDelete = true;
            item.IsEnable = true;
            item.OrderNum = int.Parse(txtSortNum.Text);
            item.ApplicationID = new DZAFCPortal.Authorization.BLL.ApplicationBLL().GetApplicationsByCode("NyAdmin").ID;
            item.Description = txtRemark.Text.Trim();
            item.RoleTypes = int.Parse(dropRoleTypes.SelectedValue);
           
            return item;
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