
using DZAFCPortal.Authorization.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DZAFCPortal.Web.Admin.Roles
{
    public partial class RoleList : Page
    {
        System.Linq.Expressions.Expression<Func<Role, bool>> predicate = p => true;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GetDataAndBind(predicate);
                LiteralSiteMap.Text = Utils.GenerateTitle(Request.Url.AbsolutePath);
                //控制权限
                DZAFCPortal.Authorization.Web.RepeaterHelper.RepeaterAuthorization(rptRole);
            }
        }

        #region--------Service 私有变量---------
        DZAFCPortal.Authorization.DAL.RoleService roleSerivce = new DZAFCPortal.Authorization.DAL.RoleService();
        DZAFCPortal.Authorization.DAL.UserService userSerivce = new DZAFCPortal.Authorization.DAL.UserService();
        DZAFCPortal.Authorization.DAL.RoleUserService userRoleService = new DZAFCPortal.Authorization.DAL.RoleUserService();
        #endregion

        #region--------页面控件的响应事件-----------
        /* 已替换为使用客户端脚本来处理
         * update by 唐万祯 at 2014/07/11
         */
        ///// <summary>
        ///// 新增
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void btnAddNew_Click(object sender, EventArgs e)
        //{
        //    CNOOCPortal.Utility.JsHelper.OepnWindow(this, "AddRole.aspx");
        //}

        /// <summary>
        /// 执行列表控件执行Command
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        protected void rptRole_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            var cmdName = e.CommandName.Trim();

            switch (cmdName)
            {
                //case "Edit": ShowEditPage(e.CommandArgument.ToString()); break;
                case "Delete": Delete(e.CommandArgument.ToString()); break;
            }
        }
        #endregion

        #region------------私有方法----------
        /* 已替换为使用客户端脚本来处理
         * update by 唐万祯 at 2014/07/11
         */
        //private void ShowEditPage(string id)
        //{
        //    var url = "EditRole.aspx?ID=" + id;

        //    //Response.Redirect(url);
        //    CNOOCPortal.Utility.JsHelper.OepnWindow(this, url);
        //}

        private void Delete(string id)
        {
            var role = roleSerivce.GenericService.GetModel(id);

            if (DZAFCPortal.Config.AppSettings.DefaultRoles.Contains(role.Name))
            {
                Fxm.Utility.Page.MessageBox.Show(String.Format("[{0}]是系统默认角色，不允许删除", role.Name));
                return;
            }

            //删除角色
            roleSerivce.GenericService.Delete(role);
            roleSerivce.GenericService.Save();

            //删除角色和用户之间的绑定关系
            var userRoles = userRoleService.GenericService.GetAll(p => p.RoleID == role.ID);
            if (userRoles != null)
            {
                foreach (var item in userRoles)
                {
                    userRoleService.GenericService.Delete(item);
                }
                userRoleService.GenericService.Save();
            }

            //删除后重新绑定数据
            GetDataAndBind(predicate);
        }

        /// <summary>
        /// 获取数据，并绑定到列表中
        /// </summary>
        private void GetDataAndBind(System.Linq.Expressions.Expression<Func<Role, bool>> predicate)
        {
            var roles = roleSerivce.GenericService.GetAll(predicate).OrderBy(p => p.OrderNum).ToList();

            rptRole.DataSource = roles.ToList();
            rptRole.DataBind();
        }
        #endregion


        /// <summary>
        /// 根据角色获取用户
        /// </summary>
        /// <remarks>create by zhanxl 2014年11月4日14:46:14</remarks>
        /// <param name="roleID"></param>
        /// <returns></returns>
        public string GetUsersByRole(object roleID)
        {
            string roleGuid = roleID.ToString() ;
            var userIds = userRoleService.GenericService.GetAll(p => p.RoleID == roleGuid).Select(p => p.UserID).ToList();
            var nameRs = String.Empty;

            if (userIds != null)
            {
                var userNames = userSerivce.GenericService.GetAll(p => userIds.Contains(p.ID)).OrderBy(p => p.DisplayName).Select(p => p.DisplayName);
                foreach (var name in userNames)
                {
                    nameRs += "<span class='userInfo'><span>" + name + "</span></span>";
                }
            }

            return nameRs;
        }

        protected void dropRoleTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int dropType = int.Parse(dropRoleTypes.SelectedValue);
            if (dropType > -1)
            {
                predicate = p => p.RoleTypes == dropType;
            }
            GetDataAndBind(predicate);
        }
    }
}