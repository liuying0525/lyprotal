
using DZAFCPortal.Authorization.DAL;
using DZAFCPortal.Authorization.Entity;
using NySoftland.Core.Repositorys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Authorization.Users
{
    public partial class UserList : BasePage
    {
        UserService userService = new UserService();
        RoleUserService roleUserService = new RoleUserService();
        RoleService roleService = new RoleService();
        private string Department;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string search = Request["search"];
                if (!string.IsNullOrEmpty(search))
                {
                    txtSearch.Text = search;
                }
                Department = Request["Department"];
                Bind();
                LiteralSiteMap.Text = Utils.GenerateTitle(Request.Url.AbsolutePath);
                DZAFCPortal.Authorization.Web.RepeaterHelper.RepeaterAuthorization(rptUser);
            }
        }
        /// <summary>
        /// 分页事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void AspNetPager1_PageChanged(object sender, EventArgs e)
        {
            Bind();
        }

        /// <summary>
        /// Repeater绑定
        /// </summary>
        void Bind()
        {
            rptUser.DataSource = GenerateBindingResource();
            rptUser.DataBind();
        }

        /// <summary>
        /// 获取数据源
        /// </summary>
        /// <returns></returns>
        List<User> GenerateBindingResource()
        {
            string type = Request["type"];
            string search = txtSearch.Text.Trim();
            System.Linq.Expressions.Expression<Func<User, bool>> pre = p => true;
            if (!String.IsNullOrEmpty(search))
            {
                pre = p => p.Account.Contains(search) || p.DisplayName.Contains(search) || p.OrganizationName.Contains(search);
            }
            if (!string.IsNullOrEmpty(Department) && String.IsNullOrEmpty(search))
            {
                if (String.IsNullOrEmpty(type))
                {
                    txtSearch.Text = Department;
                }
                pre = pre.And(p => p.OrganizationName == txtSearch.Text);
            }
            int count = 0;
            //var userResource = userService.GenericService.GetListByPage(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out count, pre, p => p.DisplayName, false).ToList();
            var userResource = userService.GenericService.GetListByPage(AspNetPager1.PageSize, AspNetPager1.CurrentPageIndex, out count, pre, new OrderByMultiCondition<User, int>(c => c.SortNum, false),new OrderByMultiCondition<User, String>(p => p.DisplayName, false)).ToList();
            AspNetPager1.RecordCount = count;
           
            return userResource;
        }


        private void Delete(string id)
        {
            var user = userService.GenericService.GetModel(new Guid(id));

            if (DZAFCPortal.Config.AppSettings.DefaultAccounts.Contains(user.Account))
            {
                Fxm.Utility.Page.MessageBox.Show(String.Format("[{0}]是系统默认账号，不允许删除", user.Account));
                return;
            }

            userService.GenericService.Delete(user);
            userService.GenericService.Save();

            List<RoleUser> lstRoleUser = roleUserService.GenericService.GetAll(r => r.UserID == user.ID).ToList();
            if (lstRoleUser.Count > 0)
            {
                foreach (var roleuser in lstRoleUser)
                {
                    roleUserService.GenericService.Delete(roleuser);
                }

                roleUserService.GenericService.Save();
            }

            Bind();
        }

        protected void rptUser_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var cmdName = e.CommandName.Trim();

            switch (cmdName)
            {
                //case "Edit": ShowCahe(); break;
                //case "SetRole": ShowCahe(); break;
            }
        }

        public string GetRoleByUser(object userID)
        {
            //return "";

            var userId = userID.ToString();
            var roleIds = roleUserService.GenericService.GetAll(p => p.UserID == userId).Select(p => p.RoleID).ToList();
            var nameRs = String.Empty;

            if (roleIds != null)
            {
                var roleNames = roleService.GenericService.GetAll(p => roleIds.Contains(p.ID)).Select(p => p.Name);
                foreach (var name in roleNames)
                {
                    nameRs += "<span class='roleInfo'><span>" + name + "</span></span>";
                }
            }

            return nameRs;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //Cache["textbox"] = txtSearch.Text.Trim();
            Bind();
        }



        #region 同步按钮(已隐藏)
        /// <summary>
        /// 同步按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnSync_Click(object sender, EventArgs e)
        //{
        //    bool isSyncOver = false;
        //    SyncHelper.Instance.ExcuteSync(out isSyncOver);
        //    if (isSyncOver)
        //        lblSyncResult.Text = "同步完成";
        //    else
        //        lblSyncResult.Text = "同步中断,请联系管理员.";

        //}
        #endregion
    }
}
